﻿using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using FTF.Windows.Properties;
using SimConnectSharp;

namespace FTF.Windows
{
    public partial class Form1 : Form
    {
        private LocationData lastLocationData = new LocationData();
        private SimConnectSharp.SimConnectSharp scs;
        private ApiClient apiClient;
        private DateTime lastsubmssion;
        private System.Threading.Timer submissionTimer;
        private Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

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
            LocationData scslld = scs.LastLocationData;
            if (scslld != null && !scslld.Equals(lastLocationData))
            {
                lastLocationData = scslld;
                richTextBox1.Text = DateTime.Now + 
                    "\n" + 
                    $"Title: {lastLocationData.Title}" +
                    "\n" +
                    $"Latitude: {lastLocationData.Latitude}" +
                    "\n" +
                    $"Longitude: {lastLocationData.Longitude}" +
                    "\n" +
                    $"Altitude: {lastLocationData.Altitude}" +
                    "\n" +
                    $"Kohlsmann: {lastLocationData.Kohlsmann}";
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
            var submit = apiClient.Submit(new SubmissionBody
            {
                callsign = tb_Callsign.Text,
                latitude = lastLocationData.Latitude,
                longitude = lastLocationData.Longitude
            });
            if (submit.IsSuccessStatusCode)
            {
                lastsubmssion = DateTime.Now;
                if (ldata_LastSubmitted.InvokeRequired)
                {
                    ldata_LastSubmitted.Invoke((MethodInvoker)(() =>
                    {
                        ldata_LastSubmitted.Text = "D: " + DateTime.Now;
                    }));
                } else
                {
                    ldata_LastSubmitted.Text = "D: " + DateTime.Now;
                }
            }
            else
            {
                submissionTimer.Dispose();
                MessageBox.Show(submit.Content);
                btn_SubStart.Enabled = true;
                btn_SubStop.Enabled = false;
                tb_Callsign.Enabled = true;
                numericUpDown1.Enabled = true;
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
    }
}
