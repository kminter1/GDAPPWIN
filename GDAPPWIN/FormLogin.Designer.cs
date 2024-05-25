namespace GDAPPWIN
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            BtnLogin = new Button();
            BtnCancle = new Button();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            btn_testconn = new Button();
            textBoxDatabase = new TextBox();
            textBoxPWD = new TextBox();
            textBoxUser = new TextBox();
            textBoxPort = new TextBox();
            textBoxIP = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnLogin
            // 
            BtnLogin.Location = new Point(200, 171);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(104, 44);
            BtnLogin.TabIndex = 2;
            BtnLogin.Text = "ตกลง";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // BtnCancle
            // 
            BtnCancle.Location = new Point(336, 171);
            BtnCancle.Name = "BtnCancle";
            BtnCancle.Size = new Size(91, 44);
            BtnCancle.TabIndex = 3;
            BtnCancle.Text = "ยกเลิก";
            BtnCancle.UseVisualStyleBackColor = true;
            BtnCancle.Click += btnCancle_Click;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(272, 53);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(155, 23);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(272, 111);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(155, 23);
            txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(200, 53);
            label1.Name = "label1";
            label1.Size = new Size(64, 19);
            label1.TabIndex = 4;
            label1.Text = "ชื่อผู้ใช้งาน";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(211, 111);
            label2.Name = "label2";
            label2.Size = new Size(53, 19);
            label2.TabIndex = 5;
            label2.Text = "รหัสผ่าน";
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_testconn);
            panel1.Controls.Add(textBoxDatabase);
            panel1.Controls.Add(textBoxPWD);
            panel1.Controls.Add(textBoxUser);
            panel1.Controls.Add(textBoxPort);
            panel1.Controls.Add(textBoxIP);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(54, 315);
            panel1.Name = "panel1";
            panel1.Size = new Size(522, 296);
            panel1.TabIndex = 6;
            // 
            // btn_testconn
            // 
            btn_testconn.Location = new Point(93, 204);
            btn_testconn.Name = "btn_testconn";
            btn_testconn.Size = new Size(75, 23);
            btn_testconn.TabIndex = 9;
            btn_testconn.Text = "Test connection";
            btn_testconn.UseVisualStyleBackColor = true;
            btn_testconn.Click += Btn_testconn_Click;
            // 
            // textBoxDatabase
            // 
            textBoxDatabase.Location = new Point(338, 111);
            textBoxDatabase.Name = "textBoxDatabase";
            textBoxDatabase.Size = new Size(100, 23);
            textBoxDatabase.TabIndex = 8;
            // 
            // textBoxPWD
            // 
            textBoxPWD.Location = new Point(338, 82);
            textBoxPWD.Name = "textBoxPWD";
            textBoxPWD.Size = new Size(100, 23);
            textBoxPWD.TabIndex = 7;
            // 
            // textBoxUser
            // 
            textBoxUser.Location = new Point(338, 53);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(100, 23);
            textBoxUser.TabIndex = 6;
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(110, 82);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(100, 23);
            textBoxPort.TabIndex = 5;
            // 
            // textBoxIP
            // 
            textBoxIP.Location = new Point(110, 53);
            textBoxIP.Name = "textBoxIP";
            textBoxIP.Size = new Size(100, 23);
            textBoxIP.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(263, 119);
            label8.Name = "label8";
            label8.Size = new Size(63, 15);
            label8.TabIndex = 0;
            label8.Text = "database  :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(263, 90);
            label7.Name = "label7";
            label7.Size = new Size(66, 15);
            label7.TabIndex = 0;
            label7.Text = "Password  :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(260, 53);
            label6.Name = "label6";
            label6.Size = new Size(69, 15);
            label6.TabIndex = 0;
            label6.Text = "Username  :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 90);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 0;
            label5.Text = "Port  :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 56);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 0;
            label4.Text = "Server IP :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 25);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 0;
            label3.Text = "testconnection";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 651);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(BtnCancle);
            Controls.Add(BtnLogin);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnLogin;
        private Button BtnCancle;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private TextBox textBoxDatabase;
        private TextBox textBoxPWD;
        private TextBox textBoxUser;
        private TextBox textBoxPort;
        private TextBox textBoxIP;
        private Label label3;
        private Button btn_testconn;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label8;
        private Label label7;
    }
}