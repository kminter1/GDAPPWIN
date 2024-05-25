namespace GDAPPWIN
{
    partial class UserForm
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
            panel2 = new Panel();
            label1 = new Label();
            txtName = new TextBox();
            dataGridViewListUser = new DataGridView();
            btnAddUser = new Button();
            label2 = new Label();
            txtSearchUser = new TextBox();
            txtUsername = new TextBox();
            label_UserID = new Label();
            txtPassword = new TextBox();
            label3 = new Label();
            btnEditUser = new Button();
            label4 = new Label();
            txtRole = new TextBox();
            label5 = new Label();
            lable1 = new Label();
            panel1 = new Panel();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListUser).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.CadetBlue;
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 40);
            panel2.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(315, 9);
            label1.Name = "label1";
            label1.Size = new Size(126, 21);
            label1.TabIndex = 0;
            label1.Text = "เพิ่ม/แก้ไข ผู้ใช้งาน";
            // 
            // txtName
            // 
            txtName.Location = new Point(185, 46);
            txtName.Name = "txtName";
            txtName.Size = new Size(144, 23);
            txtName.TabIndex = 1;
            // 
            // dataGridViewListUser
            // 
            dataGridViewListUser.AllowUserToAddRows = false;
            dataGridViewListUser.AllowUserToDeleteRows = false;
            dataGridViewListUser.AllowUserToResizeRows = false;
            dataGridViewListUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewListUser.Dock = DockStyle.Bottom;
            dataGridViewListUser.Location = new Point(0, 285);
            dataGridViewListUser.Name = "dataGridViewListUser";
            dataGridViewListUser.RowHeadersVisible = false;
            dataGridViewListUser.Size = new Size(1134, 386);
            dataGridViewListUser.TabIndex = 8;
            dataGridViewListUser.CellClick += dataGridViewListUser_CellClick;
            // 
            // btnAddUser
            // 
            btnAddUser.Font = new Font("Segoe UI", 10F);
            btnAddUser.Location = new Point(174, 195);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(71, 38);
            btnAddUser.TabIndex = 0;
            btnAddUser.Text = "เพิ่ม";
            btnAddUser.UseVisualStyleBackColor = true;
            btnAddUser.Click += btnSave_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(104, 49);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 10;
            label2.Text = "ชื่อ";
            // 
            // txtSearchUser
            // 
            txtSearchUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtSearchUser.Location = new Point(903, 256);
            txtSearchUser.Name = "txtSearchUser";
            txtSearchUser.Size = new Size(228, 23);
            txtSearchUser.TabIndex = 7;
            txtSearchUser.TextChanged += txtSearchUser_TextChanged;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(185, 75);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(144, 23);
            txtUsername.TabIndex = 2;
            // 
            // label_UserID
            // 
            label_UserID.AutoSize = true;
            label_UserID.Location = new Point(185, 20);
            label_UserID.Name = "label_UserID";
            label_UserID.Size = new Size(12, 15);
            label_UserID.TabIndex = 10;
            label_UserID.Text = "*";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(185, 104);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(144, 23);
            txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(104, 75);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 10;
            label3.Text = "UserName";
            // 
            // btnEditUser
            // 
            btnEditUser.Font = new Font("Segoe UI", 10F);
            btnEditUser.Location = new Point(251, 195);
            btnEditUser.Name = "btnEditUser";
            btnEditUser.Size = new Size(78, 38);
            btnEditUser.TabIndex = 5;
            btnEditUser.Text = "แก้ไข";
            btnEditUser.UseVisualStyleBackColor = true;
            btnEditUser.Click += btnUpdate_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(104, 104);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 10;
            label4.Text = "Password";
            // 
            // txtRole
            // 
            txtRole.Location = new Point(185, 133);
            txtRole.Name = "txtRole";
            txtRole.Size = new Size(144, 23);
            txtRole.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(104, 133);
            label5.Name = "label5";
            label5.Size = new Size(30, 15);
            label5.TabIndex = 10;
            label5.Text = "Role";
            // 
            // lable1
            // 
            lable1.AutoSize = true;
            lable1.Location = new Point(104, 20);
            lable1.Name = "lable1";
            lable1.Size = new Size(18, 15);
            lable1.TabIndex = 10;
            lable1.Text = "ID";
            // 
            // panel1
            // 
            panel1.Controls.Add(lable1);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(label_UserID);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtSearchUser);
            panel1.Controls.Add(txtRole);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtName);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(dataGridViewListUser);
            panel1.Controls.Add(btnAddUser);
            panel1.Controls.Add(btnEditUser);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 40);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 671);
            panel1.TabIndex = 11;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "UserForm";
            Text = "UserForm";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListUser).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label label1;
        private Label lable1;
        private Label label5;
        private TextBox txtRole;
        private Label label4;
        private Button btnEditUser;
        private Label label3;
        private TextBox txtPassword;
        private Label label_UserID;
        private TextBox txtUsername;
        private TextBox txtSearchUser;
        private Label label2;
        private Button btnAddUser;
        private DataGridView dataGridViewListUser;
        private TextBox txtName;
        private Panel panel1;
    }
}