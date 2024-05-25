namespace GDAPPWIN
{
    partial class CustomerForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            textBox_CID = new TextBox();
            textBox_Cname = new TextBox();
            button_AddCustomer = new Button();
            button_ClearCustomer = new Button();
            dataGridView_Customer = new DataGridView();
            panel1 = new Panel();
            label3 = new Label();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Customer).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(3, 28);
            label1.Name = "label1";
            label1.Size = new Size(69, 21);
            label1.TabIndex = 0;
            label1.Text = "รหัสลูกค้า";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(3, 65);
            label2.Name = "label2";
            label2.Size = new Size(28, 21);
            label2.TabIndex = 1;
            label2.Text = "ชื่อ";
            // 
            // textBox_CID
            // 
            textBox_CID.Location = new Point(109, 28);
            textBox_CID.Name = "textBox_CID";
            textBox_CID.Size = new Size(173, 23);
            textBox_CID.TabIndex = 2;
            // 
            // textBox_Cname
            // 
            textBox_Cname.Location = new Point(109, 65);
            textBox_Cname.Name = "textBox_Cname";
            textBox_Cname.Size = new Size(173, 23);
            textBox_Cname.TabIndex = 3;
            // 
            // button_AddCustomer
            // 
            button_AddCustomer.Font = new Font("Segoe UI", 11F);
            button_AddCustomer.Location = new Point(109, 113);
            button_AddCustomer.Name = "button_AddCustomer";
            button_AddCustomer.Size = new Size(92, 36);
            button_AddCustomer.TabIndex = 4;
            button_AddCustomer.Text = "เพิ่ม";
            button_AddCustomer.UseVisualStyleBackColor = true;
            button_AddCustomer.Click += button_AddCustomer_Click;
            // 
            // button_ClearCustomer
            // 
            button_ClearCustomer.Font = new Font("Segoe UI", 11F);
            button_ClearCustomer.Location = new Point(207, 113);
            button_ClearCustomer.Name = "button_ClearCustomer";
            button_ClearCustomer.Size = new Size(90, 36);
            button_ClearCustomer.TabIndex = 5;
            button_ClearCustomer.Text = "ยกเลิก";
            button_ClearCustomer.UseVisualStyleBackColor = true;
            button_ClearCustomer.Click += button_ClearCustomer_Click;
            // 
            // dataGridView_Customer
            // 
            dataGridView_Customer.AllowUserToAddRows = false;
            dataGridView_Customer.AllowUserToDeleteRows = false;
            dataGridView_Customer.AllowUserToResizeRows = false;
            dataGridView_Customer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView_Customer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dataGridView_Customer.BackgroundColor = SystemColors.Control;
            dataGridView_Customer.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView_Customer.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView_Customer.Location = new Point(416, 38);
            dataGridView_Customer.Name = "dataGridView_Customer";
            dataGridView_Customer.RowHeadersVisible = false;
            dataGridView_Customer.RowTemplate.Height = 23;
            dataGridView_Customer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Customer.Size = new Size(712, 661);
            dataGridView_Customer.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 32);
            panel1.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(528, 0);
            label3.Name = "label3";
            label3.Size = new Size(68, 21);
            label3.TabIndex = 0;
            label3.Text = "เพิ่มลูกค้า";
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(button_ClearCustomer);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(button_AddCustomer);
            panel2.Controls.Add(textBox_CID);
            panel2.Controls.Add(textBox_Cname);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 32);
            panel2.Name = "panel2";
            panel2.Size = new Size(410, 679);
            panel2.TabIndex = 8;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(dataGridView_Customer);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "CustomerForm";
            Text = "CustomerForm";
            Load += CustomerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_Customer).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox_CID;
        private TextBox textBox_Cname;
        private Button button_AddCustomer;
        private Button button_ClearCustomer;
        private DataGridView dataGridView_Customer;
        private Panel panel1;
        private Label label3;
        private Panel panel2;
    }
}