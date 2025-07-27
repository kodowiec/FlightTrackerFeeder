using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FTF.Windows.Properties;
using SimConnectSharp;

namespace FTF.Windows
{
    public partial class Form1 : Form
    {
        private SimConnectSharp.AircraftData lastLocationData = new SimConnectSharp.AircraftData();
        private SimConnectSharp.SimConnectSharp scs;
        private ApiClient apiClient;
        private DateTime lastsubmssion;
        private System.Threading.Timer submissionTimer;
        private Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        private List<string> AvailableMsfsVariables = new List<string>();

        private int retryCount = 0;

        PropertyInfo[] simConnectProps = typeof(SimConnectSharp.AircraftData).GetProperties();
        PropertyInfo[] aircraftDataProps = typeof(AircraftData).GetProperties();

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = false;
            if (AppConfig != null)
            {
                if (AppConfig.AppSettings.Settings.AllKeys.Contains("api_url"))
                {
                    tb_server.Text = AppConfig.AppSettings.Settings["api_url"].Value;
                }
                if (AppConfig.AppSettings.Settings.AllKeys.Contains("api_token"))
                {
                    tb_password.Text = AppConfig.AppSettings.Settings["api_token"].Value;
                }
                if (AppConfig.AppSettings.Settings.AllKeys.Contains("api_interval"))
                {
                    numericUpDown1.Value = int.Parse(AppConfig.AppSettings.Settings["api_interval"].Value);
                }
                if (AppConfig.AppSettings.Settings.AllKeys.Contains("msfs_interval"))
                {
                    checkBox1.Checked = true;
                    numericUpDown2.Value = int.Parse(AppConfig.AppSettings.Settings["msfs_interval"].Value);
                }
                if (AppConfig.AppSettings.Settings.AllKeys.Contains("msfs_period"))
                {
                    switch (AppConfig.AppSettings.Settings["msfs_period"].Value)
                    {
                        case "VISUAL_FRAME":
                            radioButton1.Checked = true; radioButton2.Checked = false; radioButton3.Checked = false;
                            break;
                        case "SIM_FRAME":
                            radioButton1.Checked = false; radioButton2.Checked = true; radioButton3.Checked = false;
                            break;
                        case "SECOND":
                            radioButton1.Checked = false; radioButton2.Checked = false; radioButton3.Checked = true;
                            break;
                    }
                }
            }
        }

        private void btn_ApiConnect_Click(object sender, EventArgs e)
        {
            this.apiClient = new ApiClient(tb_server.Text, tb_password.Text);
            this.apiClient.Connect();
            var connect = this.apiClient.Authenticate();
            ldata_ApiResponseOk.Text = connect.Content;
            if (connect.IsSuccessStatusCode)
            {
                btn_SubStart.Enabled = true;
                if (ldata_ApiResponseOk.Text.Trim().Length < 2) ldata_ApiResponseOk.Text = this.apiClient.Connect().Content;
            }
        }

        private void btn_MsfsConnect_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                scs = new SimConnectSharp.SimConnectSharp();
                if (scs.Connect())
                {
                    var calculatedInterval = (int)(numericUpDown2.Value * ((radioButton3.Checked) ? 1000 : 20));
                    if (calculatedInterval == 0)
                    {
                        calculatedInterval = (radioButton3.Checked) ? 1000 : 10;
                    }
                    SimConnectSharp.SimConnectSharp.SC_PERIOD requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.ONCE;
                    if (radioButton1.Checked) requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.VISUAL_FRAME;
                    else if (radioButton2.Checked) requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.SIM_FRAME;
                    else if (radioButton3.Checked) requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.SECOND;
                    if (numericUpDown2.Value < 0) numericUpDown2.Value = 0;
                    scs.SubscribeLocationData(requestPeriod, (uint)numericUpDown2.Value);
                    timer1.Interval = calculatedInterval;
                    timer1.Enabled = true;
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Couldn't connect to MSFS");
                }
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
                scs.UnsubscribeLocationData();
                scs.Disconnect();
                scs = new SimConnectSharp.SimConnectSharp();
                if (scs.Connect())
                {
                    var calculatedInterval = (int)(numericUpDown2.Value * ((radioButton3.Checked) ? 1000 : 20));
                    if (calculatedInterval == 0)
                    {
                        calculatedInterval = (radioButton3.Checked) ? 1000 : 10;
                    }
                    SimConnectSharp.SimConnectSharp.SC_PERIOD requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.ONCE;
                    if (radioButton1.Checked) requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.VISUAL_FRAME;
                    else if (radioButton2.Checked) requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.SIM_FRAME;
                    else if (radioButton3.Checked) requestPeriod = SimConnectSharp.SimConnectSharp.SC_PERIOD.SECOND;
                    if (numericUpDown2.Value < 0) numericUpDown2.Value = 0;
                    scs.SubscribeLocationData(requestPeriod, (uint)numericUpDown2.Value);
                    timer1.Interval = calculatedInterval;
                    timer1.Enabled = true;
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Couldn't connect to MSFS");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SimConnectSharp.AircraftData scslld = scs.LastLocationData;
            if (scslld != null && !scslld.Equals(lastLocationData))
            {
                lastLocationData = scslld;
                richTextBox1.Text = DateTime.Now + " \n" + lastLocationData.ToString(true).TrimEnd();
            } else if (scs == null)
            {
                timer1.Stop();
                timer1.Enabled = false;
                submissionTimer.Dispose();
                MessageBox.Show("SCS not connected; disabling.");
            }
        }

        private void SubmitLocationData(object data)
        {
            DateTime now = DateTime.UtcNow;
            RestSharp.RestResponse submit = null;
            
            if (!cb_FieldMapping.Checked)
            {
                submit = apiClient.Submit(new AircraftData
                {
                    date = now.ToString("O"),
                    callsign = tb_Callsign.Text,
                    latitude = lastLocationData.Latitude,
                    longitude = lastLocationData.Longitude,
                    altitude = lastLocationData.Altitude,
                    squawk = lastLocationData.TransponderCode.ToString("D4"),
                    groundSpeed = lastLocationData.GpsGroundSpeed,
                    track = lastLocationData.MagneticCompass,
                    alert = false,
                    emergency = (lastLocationData.TransponderCode == 7500 || lastLocationData.TransponderCode == 7600 || lastLocationData.TransponderCode == 7700),
                    spi = false,
                    isOnGround = lastLocationData.ContactPointIsOnGround,
                });
            } else
            {
var rows = dataGridView1.Rows.Cast<DataGridViewRow>().ToList();
                AircraftData submitBody = new AircraftData
                {
                    date = now.ToString("O"),
                    callsign = tb_Callsign.Text,

                    alert = false,
                    emergency = false,
                    spi = false,
                };

                foreach (PropertyInfo property in aircraftDataProps)
                {
                    var attribute = property.GetCustomAttribute<ADA>();
                    if (attribute.IsMappable)
                    {
                        var matchingRow = rows.FirstOrDefault(row =>
                            row.Tag is string tag &&
                            tag == property.Name &&
                            row.Cells[0].Value is string cellValue &&
                            cellValue == property.Name);

                        if (matchingRow == null) continue;

                        var simVarName = matchingRow.Cells[1].Value?.ToString();
                        if (string.IsNullOrWhiteSpace(simVarName)) continue;

                        var simVarProp = simConnectProps.FirstOrDefault(p =>
                            p.GetCustomAttributes(typeof(SimConnectVariable), false)
                             .Cast<SimConnectVariable>()
                             .Any(attr => attr.SimVarName == simVarName));

                        if (simVarProp == null) continue;

                        var simVarData = simVarProp.GetValue(lastLocationData);
                        if (property.PropertyType == typeof(string)) property.SetValue(submitBody, ((simVarData.GetType() == typeof(int))? ((int)simVarData).ToString("D4") : simVarData.ToString()));
                        else property.SetValue(submitBody, simVarData);
                    }
                }

                submitBody.emergency = (submitBody.squawk == "7500" || submitBody.squawk == "7600" || submitBody.squawk == "7700");

                submit = apiClient.Submit(submitBody);
            }

      
            if (submit.IsSuccessStatusCode)
            {
                lastsubmssion = now;
                if (ldata_LastSubmitted.InvokeRequired)
                {
                    ldata_LastSubmitted.Invoke((MethodInvoker)(() =>
                    {
                        ldata_LastSubmitted.Text = "D: " + now;
                    }));
                } else
                {
                    ldata_LastSubmitted.Text = "D: " + now;
                }
                retryCount = 0;
            }
            else
            {
                if (retryCount > 10)
                {
                    submissionTimer.Dispose();
                    retryCount = 0;
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)(() => {
                            this.BringToFront();
                            this.TopMost = true;
                            btn_SubStart.Enabled = true;
                            btn_SubStop.Enabled = false;
                            tb_Callsign.Enabled = true;
                            numericUpDown1.Enabled = true;
                            System.Threading.Thread.Sleep(100);
                            this.TopMost = false;
                            MessageBox.Show($"Error submitting data.\nError message: {submit.ErrorMessage}\n{submit.Content}\nException: {submit.ErrorException}\nRequest: {submit.Request}");
                        }));
                    } else
                    {
                        this.BringToFront();
                        this.TopMost = true;
                        btn_SubStart.Enabled = true;
                        btn_SubStop.Enabled = false;
                        tb_Callsign.Enabled = true;
                        numericUpDown1.Enabled = true;
                        System.Threading.Thread.Sleep(100);
                        this.TopMost = false;
                        MessageBox.Show($"Error submitting data.\nError message: {submit.ErrorMessage}\nException: {submit.ErrorException}\nRequest: {submit.Request}");
                    }
                }
                retryCount++;
            }
        }

        private void btn_SubStart_Click(object sender, EventArgs e)
        {
            btn_SubStart.Enabled = false;
            btn_SubStop.Enabled = true;
            tb_Callsign.Enabled = false;
            numericUpDown1.Enabled = false;
            submissionTimer = new System.Threading.Timer(SubmitLocationData, null, 0, (int)numericUpDown1.Value);
        }

        private void btn_SubStop_Click(object sender, EventArgs e)
        {
            submissionTimer.Dispose();
            btn_SubStart.Enabled = true;
            btn_SubStop.Enabled = false;
            tb_Callsign.Enabled = true;
            numericUpDown1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown2.Enabled = true;
            } else
            {
                numericUpDown2.Enabled = false;
                numericUpDown2.Value = 0;
            }
        }

        private void ldata_ApiResponseOk_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ldata_ApiResponseOk.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                RemoveSetting("msfs_interval");
            }
            else
            {
                SaveSetting("msfs_interval", numericUpDown2.Value.ToString());
            }

            SaveSetting("api_url", tb_server.Text);
            SaveSetting("api_token", tb_password.Text);
            SaveSetting("api_interval", numericUpDown1.Value.ToString());
            SaveSetting("msfs_period", ((radioButton1.Checked) ? "VISUAL_FRAME" : ((radioButton2.Checked) ? "SIM_FRAME" : "SECOND")));

            AppConfig.Save(ConfigurationSaveMode.Full);
        }


        private void SaveSetting(string key, string value)
        {
            if (AppConfig.AppSettings.Settings[key] == null)
            {
                AppConfig.AppSettings.Settings.Add(key, value);
            }
            else
            {
                AppConfig.AppSettings.Settings[key].Value = value;
            }
        }

        private void RemoveSetting(string key)
        {
            if (AppConfig.AppSettings.Settings[key] != null) AppConfig.AppSettings.Settings.Remove(key);
        }

        private void btn_fieldMapping_Click(object sender, EventArgs e)
        {
            this.Width = (this.Width > 700) ? 700 : 1040;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (PropertyInfo property in typeof(SimConnectSharp.AircraftData).GetProperties())
            {
                var attribute = property.GetCustomAttribute<SimConnectVariable>();
                AvailableMsfsVariables.Add(attribute.SimVarName);
            }

            using (DataGridViewComboBoxColumn column = dataGridView1.Columns[1] as DataGridViewComboBoxColumn)
            {
                column.DataSource = AvailableMsfsVariables;
            }

            foreach (PropertyInfo property in typeof(AircraftData).GetProperties())
            {
                var attribute = property.GetCustomAttribute<ADA>();
                if (attribute.IsMappable)
                {
                    var id = dataGridView1.Rows.Add();
                    using (DataGridViewRow row = dataGridView1.Rows[id])
                    {
                        row.Tag = property.Name;
                        row.Cells[0].Value = property.Name;
                        (row.Cells[1] as DataGridViewComboBoxCell).Value = attribute.DefSimvar;
                    }
                }
            }

        }

        private void cb_FieldMapping_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = cb_FieldMapping.Checked;
            dataGridView1.Enabled = cb_FieldMapping.Checked;
        }
    }
}
