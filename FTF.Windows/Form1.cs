using System;
using System.Windows.Forms;
using SimConnectSharp;

namespace FTF.Windows
{
    public partial class Form1 : Form
    {
        private LocationData lastLocationData = new LocationData();
        private SimConnectSharp.SimConnectSharp scs;
        private ApiClient apiClient;
        private DateTime lastsubmssion;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_ApiConnect_Click(object sender, EventArgs e)
        {
            this.apiClient = new ApiClient(tb_server.Text, tb_password.Text);
            var connect = this.apiClient.Connect();
            ldata_ApiResponseOk.Text = connect.Content;
            if (connect.IsSuccessStatusCode) btn_SubStart.Enabled = true; 
        }

        private void btn_MsfsConnect_Click(object sender, EventArgs e)
        {
            scs = new SimConnectSharp.SimConnectSharp();
            scs.Connect();
            timer1.Interval = 100;
            timer1.Enabled = true;
            timer1.Start();
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
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var submit = apiClient.Submit(new SubmissionBody {
                callsign = tb_Callsign.Text,
                latitude = lastLocationData.Latitude,
                longitude = lastLocationData.Longitude
            });
            if (submit.IsSuccessStatusCode)
            {
                lastsubmssion = DateTime.Now;
                ldata_LastSubmitted.Text = "D: " + DateTime.Now;
            }
            else
            {
                MessageBox.Show(submit.Content);
                timer2.Stop();
                timer2.Enabled = false;
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
            timer2.Interval = (int)numericUpDown1.Value;
            timer2.Enabled = true;
            timer2.Start();
        }

        private void btn_SubStop_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            timer2.Enabled = false;
            btn_SubStart.Enabled = true;
            btn_SubStop.Enabled = false;
            tb_Callsign.Enabled = true;
            numericUpDown1.Enabled = true;
        }
    }
}
