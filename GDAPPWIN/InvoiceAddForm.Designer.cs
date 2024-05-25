namespace GDAPPWIN
{
    partial class InvoiceAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceAddForm));
            panel1 = new Panel();
            label12 = new Label();
            txt_SearchTemporaryInv = new TextBox();
            label13 = new Label();
            panel2 = new Panel();
            label14 = new Label();
            btnImportData = new Button();
            panel4 = new Panel();
            label2 = new Label();
            txtIssueDate = new DateTimePicker();
            txtDetails = new RichTextBox();
            label6 = new Label();
            label7 = new Label();
            label11 = new Label();
            label10 = new Label();
            label5 = new Label();
            label4 = new Label();
            btnRealReset = new Button();
            btnRealSave = new Button();
            labelDiscountAmount = new Label();
            labelDepositAmount = new Label();
            labelTotalAmount = new Label();
            labelCustomerID = new Label();
            labelCustomerName = new Label();
            label9 = new Label();
            label3 = new Label();
            txtTotalAmount = new TextBox();
            txtExternalBillName = new TextBox();
            dataGridViewTemporaryInvList = new DataGridView();
            panel5 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTemporaryInvList).BeginInit();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.RoyalBlue;
            panel1.Controls.Add(label12);
            panel1.Controls.Add(txt_SearchTemporaryInv);
            panel1.Controls.Add(label13);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 43);
            panel1.TabIndex = 4;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F);
            label12.ForeColor = Color.White;
            label12.Location = new Point(479, 12);
            label12.Name = "label12";
            label12.Size = new Size(173, 21);
            label12.TabIndex = 0;
            label12.Text = "ค้นหาบิลใบส่งของ (ORNC)";
            // 
            // txt_SearchTemporaryInv
            // 
            txt_SearchTemporaryInv.Location = new Point(890, 14);
            txt_SearchTemporaryInv.Name = "txt_SearchTemporaryInv";
            txt_SearchTemporaryInv.Size = new Size(224, 23);
            txt_SearchTemporaryInv.TabIndex = 7;
            txt_SearchTemporaryInv.TextChanged += txt_SearchTemporaryInv_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(795, 17);
            label13.Name = "label13";
            label13.Size = new Size(79, 15);
            label13.TabIndex = 10;
            label13.Text = "*ค้นหาใบส่งของ";
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(label14);
            panel2.Controls.Add(btnImportData);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(txtIssueDate);
            panel2.Controls.Add(txtDetails);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(btnRealReset);
            panel2.Controls.Add(btnRealSave);
            panel2.Controls.Add(labelDiscountAmount);
            panel2.Controls.Add(labelDepositAmount);
            panel2.Controls.Add(labelTotalAmount);
            panel2.Controls.Add(labelCustomerID);
            panel2.Controls.Add(labelCustomerName);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtTotalAmount);
            panel2.Controls.Add(txtExternalBillName);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 263);
            panel2.TabIndex = 6;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F);
            label14.Location = new Point(741, 82);
            label14.Name = "label14";
            label14.Size = new Size(72, 21);
            label14.TabIndex = 13;
            label14.Text = "จำนวนเงิน";
            // 
            // btnImportData
            // 
            btnImportData.Font = new Font("Segoe UI", 10F);
            btnImportData.Location = new Point(491, 199);
            btnImportData.Name = "btnImportData";
            btnImportData.Size = new Size(143, 40);
            btnImportData.TabIndex = 4;
            btnImportData.Text = "นำเข้าข้อมูลใบส่งของ";
            btnImportData.UseVisualStyleBackColor = true;
            btnImportData.Click += btnImportData_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.RoyalBlue;
            panel4.Controls.Add(label2);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1134, 40);
            panel4.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(510, 9);
            label2.Name = "label2";
            label2.Size = new Size(124, 21);
            label2.TabIndex = 0;
            label2.Text = "เปลี่ยนบิล (ORVC)";
            // 
            // txtIssueDate
            // 
            txtIssueDate.Location = new Point(492, 81);
            txtIssueDate.Name = "txtIssueDate";
            txtIssueDate.Size = new Size(174, 23);
            txtIssueDate.TabIndex = 3;
            // 
            // txtDetails
            // 
            txtDetails.Location = new Point(123, 165);
            txtDetails.Name = "txtDetails";
            txtDetails.Size = new Size(303, 74);
            txtDetails.TabIndex = 2;
            txtDetails.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(123, 141);
            label6.Name = "label6";
            label6.Size = new Size(93, 21);
            label6.TabIndex = 0;
            label6.Text = "รายละเอียดบิล";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(414, 83);
            label7.Name = "label7";
            label7.Size = new Size(36, 21);
            label7.TabIndex = 0;
            label7.Text = "วันที่";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = Color.Black;
            label11.Location = new Point(759, 144);
            label11.Name = "label11";
            label11.Size = new Size(54, 21);
            label11.TabIndex = 0;
            label11.Text = "ส่วนลด";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(715, 115);
            label10.Name = "label10";
            label10.Size = new Size(98, 21);
            label10.TabIndex = 0;
            label10.Text = "จำนวนเงินฝาก";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(27, 116);
            label5.Name = "label5";
            label5.Size = new Size(72, 21);
            label5.TabIndex = 0;
            label5.Text = "จำนวนเงิน";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(53, 85);
            label4.Name = "label4";
            label4.Size = new Size(42, 21);
            label4.TabIndex = 0;
            label4.Text = "เลขที่";
            // 
            // btnRealReset
            // 
            btnRealReset.Font = new Font("Segoe UI", 10F);
            btnRealReset.Location = new Point(742, 199);
            btnRealReset.Name = "btnRealReset";
            btnRealReset.Size = new Size(96, 40);
            btnRealReset.TabIndex = 6;
            btnRealReset.Text = "ยกเลิก";
            btnRealReset.UseVisualStyleBackColor = true;
            btnRealReset.Click += btnRealReset_Click;
            // 
            // btnRealSave
            // 
            btnRealSave.Font = new Font("Segoe UI", 10F);
            btnRealSave.Location = new Point(640, 199);
            btnRealSave.Name = "btnRealSave";
            btnRealSave.Size = new Size(96, 40);
            btnRealSave.TabIndex = 5;
            btnRealSave.Text = "ตกลง";
            btnRealSave.UseVisualStyleBackColor = true;
            btnRealSave.Click += btnRealSave_Click;
            // 
            // labelDiscountAmount
            // 
            labelDiscountAmount.AutoSize = true;
            labelDiscountAmount.Font = new Font("Segoe UI", 12F);
            labelDiscountAmount.ForeColor = Color.Black;
            labelDiscountAmount.Location = new Point(837, 144);
            labelDiscountAmount.Name = "labelDiscountAmount";
            labelDiscountAmount.Size = new Size(17, 21);
            labelDiscountAmount.TabIndex = 0;
            labelDiscountAmount.Text = "*";
            // 
            // labelDepositAmount
            // 
            labelDepositAmount.AutoSize = true;
            labelDepositAmount.Font = new Font("Segoe UI", 12F);
            labelDepositAmount.ForeColor = Color.Black;
            labelDepositAmount.Location = new Point(837, 115);
            labelDepositAmount.Name = "labelDepositAmount";
            labelDepositAmount.Size = new Size(17, 21);
            labelDepositAmount.TabIndex = 0;
            labelDepositAmount.Text = "*";
            // 
            // labelTotalAmount
            // 
            labelTotalAmount.AutoSize = true;
            labelTotalAmount.Font = new Font("Segoe UI", 12F);
            labelTotalAmount.ForeColor = Color.Black;
            labelTotalAmount.Location = new Point(837, 84);
            labelTotalAmount.Name = "labelTotalAmount";
            labelTotalAmount.Size = new Size(17, 21);
            labelTotalAmount.TabIndex = 0;
            labelTotalAmount.Text = "*";
            // 
            // labelCustomerID
            // 
            labelCustomerID.AutoSize = true;
            labelCustomerID.Font = new Font("Segoe UI", 12F);
            labelCustomerID.ForeColor = Color.Black;
            labelCustomerID.Location = new Point(463, 43);
            labelCustomerID.Name = "labelCustomerID";
            labelCustomerID.Size = new Size(17, 21);
            labelCustomerID.TabIndex = 0;
            labelCustomerID.Text = "*";
            // 
            // labelCustomerName
            // 
            labelCustomerName.AutoSize = true;
            labelCustomerName.Font = new Font("Segoe UI", 12F);
            labelCustomerName.ForeColor = Color.Black;
            labelCustomerName.Location = new Point(123, 43);
            labelCustomerName.Name = "labelCustomerName";
            labelCustomerName.Size = new Size(17, 21);
            labelCustomerName.TabIndex = 0;
            labelCustomerName.Text = "*";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(71, 43);
            label9.Name = "label9";
            label9.Size = new Size(28, 21);
            label9.TabIndex = 0;
            label9.Text = "ชื่อ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(414, 43);
            label3.Name = "label3";
            label3.Size = new Size(25, 21);
            label3.TabIndex = 0;
            label3.Text = "ID";
            // 
            // txtTotalAmount
            // 
            txtTotalAmount.Location = new Point(123, 114);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.Size = new Size(174, 23);
            txtTotalAmount.TabIndex = 1;
            txtTotalAmount.TextChanged += txtTotalAmount_TextChanged;
            // 
            // txtExternalBillName
            // 
            txtExternalBillName.Location = new Point(123, 85);
            txtExternalBillName.Name = "txtExternalBillName";
            txtExternalBillName.Size = new Size(174, 23);
            txtExternalBillName.TabIndex = 0;
            // 
            // dataGridViewTemporaryInvList
            // 
            dataGridViewTemporaryInvList.AllowUserToAddRows = false;
            dataGridViewTemporaryInvList.AllowUserToDeleteRows = false;
            dataGridViewTemporaryInvList.AllowUserToResizeRows = false;
            dataGridViewTemporaryInvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewTemporaryInvList.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewTemporaryInvList.Dock = DockStyle.Top;
            dataGridViewTemporaryInvList.Location = new Point(0, 43);
            dataGridViewTemporaryInvList.Name = "dataGridViewTemporaryInvList";
            dataGridViewTemporaryInvList.RowHeadersVisible = false;
            dataGridViewTemporaryInvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTemporaryInvList.Size = new Size(1134, 393);
            dataGridViewTemporaryInvList.TabIndex = 8;
            // 
            // panel5
            // 
            panel5.AutoSize = true;
            panel5.Controls.Add(dataGridViewTemporaryInvList);
            panel5.Controls.Add(panel1);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 263);
            panel5.Name = "panel5";
            panel5.Size = new Size(1134, 436);
            panel5.TabIndex = 11;
            // 
            // InvoiceAddForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel5);
            Controls.Add(panel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InvoiceAddForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InvoiceAddForm";
            Load += InvoiceAddForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTemporaryInvList).EndInit();
            panel5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label3;
        private RichTextBox txtDetails;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button btnRealSave;
        private TextBox txtTotalAmount;
        private TextBox txtExternalBillName;
        private DateTimePicker txtIssueDate;
        private Label label7;
        private DataGridView dataGridViewInvCustomer;
        private Label label9;
        private DataGridViewTextBoxColumn SelectedID;
        private DataGridViewTextBoxColumn Ccode;
        private DataGridViewTextBoxColumn SelectedName;
        private Label labelCustomerName;
        private Panel panel4;
        private Label label2;
        private Button btnRealReset;
        private Label label10;
        private Label label11;
        private DataGridView dataGridView_InvoiceList;
        private Panel panel3;
        private Label label12;
        private DataGridView dataGridViewTemporaryInvList;
        private TextBox txt_SearchTemporaryInv;
        private Label label13;
        private Label labelCustomerID;
        private Button btnImportData;
        private Panel panel5;
        private Panel panel6;
        private Label labelDiscountAmount;
        private Label labelDepositAmount;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label labelTotalAmount;
    }
}