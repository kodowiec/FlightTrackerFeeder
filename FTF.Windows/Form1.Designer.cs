namespace FTF.Windows
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ldata_ApiResponseOk = new System.Windows.Forms.Label();
            this.btn_ApiConnect = new System.Windows.Forms.Button();
            this.tb_server = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_MsfsConnect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_Callsign = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SubStart = new System.Windows.Forms.Button();
            this.btn_SubStop = new System.Windows.Forms.Button();
            this.ldata_LastSubmitted = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ldata_ApiResponseOk);
            this.groupBox1.Controls.Add(this.btn_ApiConnect);
            this.groupBox1.Controls.Add(this.tb_server);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_password);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(325, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // ldata_ApiResponseOk
            // 
            this.ldata_ApiResponseOk.AutoSize = true;
            this.ldata_ApiResponseOk.Location = new System.Drawing.Point(6, 118);
            this.ldata_ApiResponseOk.Name = "ldata_ApiResponseOk";
            this.ldata_ApiResponseOk.Size = new System.Drawing.Size(76, 13);
            this.ldata_ApiResponseOk.TabIndex = 5;
            this.ldata_ApiResponseOk.Text = "Not authorized";
            // 
            // btn_ApiConnect
            // 
            this.btn_ApiConnect.Location = new System.Drawing.Point(218, 108);
            this.btn_ApiConnect.Name = "btn_ApiConnect";
            this.btn_ApiConnect.Size = new System.Drawing.Size(75, 23);
            this.btn_ApiConnect.TabIndex = 4;
            this.btn_ApiConnect.Text = "Connect";
            this.btn_ApiConnect.UseVisualStyleBackColor = true;
            this.btn_ApiConnect.Click += new System.EventHandler(this.btn_ApiConnect_Click);
            // 
            // tb_server
            // 
            this.tb_server.Location = new System.Drawing.Point(9, 43);
            this.tb_server.Name = "tb_server";
            this.tb_server.Size = new System.Drawing.Size(284, 20);
            this.tb_server.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(9, 82);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(284, 20);
            this.tb_password.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Password";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Controls.Add(this.btn_MsfsConnect);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 259);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MSFS";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Enabled = false;
            this.numericUpDown2.Location = new System.Drawing.Point(170, 167);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(109, 20);
            this.numericUpDown2.TabIndex = 9;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(152, 146);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Additional period interval";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(9, 200);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(62, 17);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.Text = "Second";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(9, 177);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(105, 17);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.Text = "Simulation Frame";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 154);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Visual Frame";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Refresh period";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 23);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(284, 96);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // btn_MsfsConnect
            // 
            this.btn_MsfsConnect.Location = new System.Drawing.Point(184, 227);
            this.btn_MsfsConnect.Name = "btn_MsfsConnect";
            this.btn_MsfsConnect.Size = new System.Drawing.Size(109, 23);
            this.btn_MsfsConnect.TabIndex = 2;
            this.btn_MsfsConnect.Text = "Connect to MSFS";
            this.btn_MsfsConnect.UseVisualStyleBackColor = true;
            this.btn_MsfsConnect.Click += new System.EventHandler(this.btn_MsfsConnect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_Callsign);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btn_SubStart);
            this.groupBox3.Controls.Add(this.btn_SubStop);
            this.groupBox3.Controls.Add(this.ldata_LastSubmitted);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(325, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 92);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Submission";
            // 
            // tb_Callsign
            // 
            this.tb_Callsign.Location = new System.Drawing.Point(104, 63);
            this.tb_Callsign.Name = "tb_Callsign";
            this.tb_Callsign.Size = new System.Drawing.Size(116, 20);
            this.tb_Callsign.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Callsign";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(9, 63);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Interval (ms)";
            // 
            // btn_SubStart
            // 
            this.btn_SubStart.Enabled = false;
            this.btn_SubStart.Location = new System.Drawing.Point(226, 63);
            this.btn_SubStart.Name = "btn_SubStart";
            this.btn_SubStart.Size = new System.Drawing.Size(75, 23);
            this.btn_SubStart.TabIndex = 3;
            this.btn_SubStart.Text = "Start";
            this.btn_SubStart.UseVisualStyleBackColor = true;
            this.btn_SubStart.Click += new System.EventHandler(this.btn_SubStart_Click);
            // 
            // btn_SubStop
            // 
            this.btn_SubStop.Enabled = false;
            this.btn_SubStop.Location = new System.Drawing.Point(226, 34);
            this.btn_SubStop.Name = "btn_SubStop";
            this.btn_SubStop.Size = new System.Drawing.Size(75, 23);
            this.btn_SubStop.TabIndex = 2;
            this.btn_SubStop.Text = "Stop";
            this.btn_SubStop.UseVisualStyleBackColor = true;
            this.btn_SubStop.Click += new System.EventHandler(this.btn_SubStop_Click);
            // 
            // ldata_LastSubmitted
            // 
            this.ldata_LastSubmitted.AutoSize = true;
            this.ldata_LastSubmitted.Location = new System.Drawing.Point(91, 20);
            this.ldata_LastSubmitted.Name = "ldata_LastSubmitted";
            this.ldata_LastSubmitted.Size = new System.Drawing.Size(0, 13);
            this.ldata_LastSubmitted.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Last submitted:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 280);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Flight Tracker Feeder";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ApiConnect;
        private System.Windows.Forms.TextBox tb_server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_MsfsConnect;
        private System.Windows.Forms.Label ldata_ApiResponseOk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_SubStart;
        private System.Windows.Forms.Button btn_SubStop;
        private System.Windows.Forms.Label ldata_LastSubmitted;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox tb_Callsign;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

