namespace GDAPPWIN
{
    partial class OrvcAddFormA
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
            panel6 = new Panel();
            dataGridView_InvoiceList = new DataGridView();
            panel3 = new Panel();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label1 = new Label();
            txtSearchOrvc = new TextBox();
            panel4 = new Panel();
            label2 = new Label();
            txtInvoiceDate = new DateTimePicker();
            txtDetails = new RichTextBox();
            label6 = new Label();
            label7 = new Label();
            label11 = new Label();
            label10 = new Label();
            panel2 = new Panel();
            splitContainer1 = new SplitContainer();
            btnRealSave = new Button();
            btn_AddCustomer = new Button();
            btnRealReset = new Button();
            Btn_ImportOrvc = new Button();
            BtnVoidOrvc = new Button();
            Btn_EditOrvc = new Button();
            txtDiscountAmount = new TextBox();
            txtDepositAmount = new TextBox();
            label5 = new Label();
            label4 = new Label();
            labelRealInvoiceID = new Label();
            labelCustomerID = new Label();
            labelCustomerName = new Label();
            label9 = new Label();
            label13 = new Label();
            label3 = new Label();
            txtTotalAmount = new TextBox();
            txtExternalBillName = new TextBox();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_InvoiceList).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.Controls.Add(dataGridView_InvoiceList);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 344);
            panel6.Name = "panel6";
            panel6.Size = new Size(1134, 367);
            panel6.TabIndex = 15;
            // 
            // dataGridView_InvoiceList
            // 
            dataGridView_InvoiceList.AllowUserToAddRows = false;
            dataGridView_InvoiceList.AllowUserToDeleteRows = false;
            dataGridView_InvoiceList.AllowUserToResizeRows = false;
            dataGridView_InvoiceList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView_InvoiceList.BackgroundColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView_InvoiceList.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView_InvoiceList.Dock = DockStyle.Fill;
            dataGridView_InvoiceList.Location = new Point(0, 0);
            dataGridView_InvoiceList.Name = "dataGridView_InvoiceList";
            dataGridView_InvoiceList.RowHeadersVisible = false;
            dataGridView_InvoiceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_InvoiceList.Size = new Size(1134, 367);
            dataGridView_InvoiceList.TabIndex = 25;
            dataGridView_InvoiceList.CellContentClick += dataGridView_InvoiceList_CellContentClick;
            // 
            // panel3
            // 
            panel3.BackColor = Color.RoyalBlue;
            panel3.Controls.Add(label17);
            panel3.Controls.Add(label16);
            panel3.Controls.Add(label15);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(txtSearchOrvc);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 294);
            panel3.Name = "panel3";
            panel3.Size = new Size(1134, 50);
            panel3.TabIndex = 26;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 10F);
            label17.ForeColor = SystemColors.Info;
            label17.Location = new Point(177, 13);
            label17.Name = "label17";
            label17.Size = new Size(58, 19);
            label17.TabIndex = 20;
            label17.Text = "บิลตั้งเบิก";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 10F);
            label16.ForeColor = Color.DarkGreen;
            label16.Location = new Point(98, 13);
            label16.Name = "label16";
            label16.Size = new Size(50, 19);
            label16.TabIndex = 19;
            label16.Text = "จ่ายแล้ว";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10F);
            label15.ForeColor = Color.Red;
            label15.Location = new Point(12, 13);
            label15.Name = "label15";
            label15.Size = new Size(60, 19);
            label15.TabIndex = 18;
            label15.Text = "ยกเลิกบิล";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(413, 13);
            label1.Name = "label1";
            label1.Size = new Size(200, 21);
            label1.TabIndex = 0;
            label1.Text = "รายการใบส่งของตัวจริง (ORVC)";
            // 
            // txtSearchOrvc
            // 
            txtSearchOrvc.Location = new Point(828, 15);
            txtSearchOrvc.Name = "txtSearchOrvc";
            txtSearchOrvc.Size = new Size(174, 23);
            txtSearchOrvc.TabIndex = 13;
            txtSearchOrvc.TextChanged += txtSearchOrvc_TextChanged;
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
            label2.Location = new Point(463, 9);
            label2.Name = "label2";
            label2.Size = new Size(178, 21);
            label2.TabIndex = 0;
            label2.Text = "เพิ่ม/แก้ไข/ยกเลิก  (ORVC)";
            // 
            // txtInvoiceDate
            // 
            txtInvoiceDate.Location = new Point(672, 178);
            txtInvoiceDate.Name = "txtInvoiceDate";
            txtInvoiceDate.Size = new Size(174, 23);
            txtInvoiceDate.TabIndex = 5;
            // 
            // txtDetails
            // 
            txtDetails.Location = new Point(863, 182);
            txtDetails.Name = "txtDetails";
            txtDetails.Size = new Size(252, 51);
            txtDetails.TabIndex = 6;
            txtDetails.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(863, 148);
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
            label7.Location = new Point(615, 180);
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
            label11.Location = new Point(353, 180);
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
            label10.Location = new Point(309, 210);
            label10.Name = "label10";
            label10.Size = new Size(98, 21);
            label10.TabIndex = 0;
            label10.Text = "จำนวนเงินฝาก";
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(splitContainer1);
            panel2.Controls.Add(txtDiscountAmount);
            panel2.Controls.Add(txtDepositAmount);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(txtInvoiceDate);
            panel2.Controls.Add(txtDetails);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(labelRealInvoiceID);
            panel2.Controls.Add(labelCustomerID);
            panel2.Controls.Add(labelCustomerName);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtTotalAmount);
            panel2.Controls.Add(txtExternalBillName);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 294);
            panel2.TabIndex = 13;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(422, 46);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.GradientInactiveCaption;
            splitContainer1.Panel1.Controls.Add(btnRealSave);
            splitContainer1.Panel1.Controls.Add(btn_AddCustomer);
            splitContainer1.Panel1.Controls.Add(btnRealReset);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.Info;
            splitContainer1.Panel2.Controls.Add(Btn_ImportOrvc);
            splitContainer1.Panel2.Controls.Add(BtnVoidOrvc);
            splitContainer1.Panel2.Controls.Add(Btn_EditOrvc);
            splitContainer1.Size = new Size(700, 75);
            splitContainer1.SplitterDistance = 339;
            splitContainer1.TabIndex = 24;
            // 
            // btnRealSave
            // 
            btnRealSave.Font = new Font("Segoe UI", 11F);
            btnRealSave.Location = new Point(122, 11);
            btnRealSave.Name = "btnRealSave";
            btnRealSave.Size = new Size(96, 50);
            btnRealSave.TabIndex = 8;
            btnRealSave.Text = "บันทึก";
            btnRealSave.UseVisualStyleBackColor = true;
            btnRealSave.Click += btnRealSave_Click;
            // 
            // btn_AddCustomer
            // 
            btn_AddCustomer.Font = new Font("Segoe UI", 11F);
            btn_AddCustomer.Location = new Point(24, 11);
            btn_AddCustomer.Name = "btn_AddCustomer";
            btn_AddCustomer.Size = new Size(92, 49);
            btn_AddCustomer.TabIndex = 7;
            btn_AddCustomer.Text = "เลือกลูกค้า";
            btn_AddCustomer.UseVisualStyleBackColor = true;
            btn_AddCustomer.Click += btn_AddCustomer_Click;
            // 
            // btnRealReset
            // 
            btnRealReset.Font = new Font("Segoe UI", 11F);
            btnRealReset.Location = new Point(224, 11);
            btnRealReset.Name = "btnRealReset";
            btnRealReset.Size = new Size(96, 50);
            btnRealReset.TabIndex = 9;
            btnRealReset.Text = "ยกเลิก";
            btnRealReset.UseVisualStyleBackColor = true;
            btnRealReset.Click += btnRealReset_Click;
            // 
            // Btn_ImportOrvc
            // 
            Btn_ImportOrvc.Font = new Font("Segoe UI", 11F);
            Btn_ImportOrvc.Location = new Point(27, 11);
            Btn_ImportOrvc.Name = "Btn_ImportOrvc";
            Btn_ImportOrvc.Size = new Size(96, 50);
            Btn_ImportOrvc.TabIndex = 10;
            Btn_ImportOrvc.Text = "นำเข้าข้อมูล";
            Btn_ImportOrvc.UseVisualStyleBackColor = true;
            Btn_ImportOrvc.Click += Btn_ImportOrvc_Click;
            // 
            // BtnVoidOrvc
            // 
            BtnVoidOrvc.Font = new Font("Segoe UI", 11F);
            BtnVoidOrvc.Location = new Point(231, 11);
            BtnVoidOrvc.Name = "BtnVoidOrvc";
            BtnVoidOrvc.Size = new Size(96, 50);
            BtnVoidOrvc.TabIndex = 12;
            BtnVoidOrvc.Text = "ยกเลิกบิล";
            BtnVoidOrvc.UseVisualStyleBackColor = true;
            BtnVoidOrvc.Click += BtnVoidOrvc_Click;
            // 
            // Btn_EditOrvc
            // 
            Btn_EditOrvc.Font = new Font("Segoe UI", 11F);
            Btn_EditOrvc.Location = new Point(129, 12);
            Btn_EditOrvc.Name = "Btn_EditOrvc";
            Btn_EditOrvc.Size = new Size(96, 50);
            Btn_EditOrvc.TabIndex = 11;
            Btn_EditOrvc.Text = "แก้ไข";
            Btn_EditOrvc.UseVisualStyleBackColor = true;
            Btn_EditOrvc.Click += Btn_EditOrvc_Click;
            // 
            // txtDiscountAmount
            // 
            txtDiscountAmount.Location = new Point(422, 181);
            txtDiscountAmount.Name = "txtDiscountAmount";
            txtDiscountAmount.Size = new Size(174, 23);
            txtDiscountAmount.TabIndex = 3;
            // 
            // txtDepositAmount
            // 
            txtDepositAmount.Location = new Point(422, 210);
            txtDepositAmount.Name = "txtDepositAmount";
            txtDepositAmount.Size = new Size(174, 23);
            txtDepositAmount.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(20, 210);
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
            label4.Location = new Point(50, 179);
            label4.Name = "label4";
            label4.Size = new Size(42, 21);
            label4.TabIndex = 0;
            label4.Text = "เลขที่";
            // 
            // labelRealInvoiceID
            // 
            labelRealInvoiceID.AutoSize = true;
            labelRealInvoiceID.Font = new Font("Segoe UI", 12F);
            labelRealInvoiceID.ForeColor = Color.DarkRed;
            labelRealInvoiceID.Location = new Point(112, 87);
            labelRealInvoiceID.Name = "labelRealInvoiceID";
            labelRealInvoiceID.Size = new Size(17, 21);
            labelRealInvoiceID.TabIndex = 0;
            labelRealInvoiceID.Text = "*";
            // 
            // labelCustomerID
            // 
            labelCustomerID.AutoSize = true;
            labelCustomerID.Font = new Font("Segoe UI", 12F);
            labelCustomerID.ForeColor = Color.Black;
            labelCustomerID.Location = new Point(112, 116);
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
            labelCustomerName.Location = new Point(112, 148);
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
            label9.Location = new Point(60, 148);
            label9.Name = "label9";
            label9.Size = new Size(28, 21);
            label9.TabIndex = 0;
            label9.Text = "ชื่อ";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.ForeColor = Color.DarkRed;
            label13.Location = new Point(32, 87);
            label13.Name = "label13";
            label13.Size = new Size(60, 15);
            label13.TabIndex = 0;
            label13.Text = "ID (ORVC)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(11, 116);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 0;
            label3.Text = "ID (Customer)";
            // 
            // txtTotalAmount
            // 
            txtTotalAmount.Location = new Point(116, 208);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.Size = new Size(174, 23);
            txtTotalAmount.TabIndex = 2;
            // 
            // txtExternalBillName
            // 
            txtExternalBillName.Location = new Point(116, 179);
            txtExternalBillName.Name = "txtExternalBillName";
            txtExternalBillName.Size = new Size(174, 23);
            txtExternalBillName.TabIndex = 1;
            // 
            // OrvcAddFormA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel6);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Name = "OrvcAddFormA";
            Text = "OrvcAddFormA";
            Load += OrvcAddFormA_Load;
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_InvoiceList).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel6;
        private DataGridView dataGridView_InvoiceList;
        private Panel panel3;
        private Label label1;
        private Panel panel4;
        private Label label2;
        private DateTimePicker txtInvoiceDate;
        private RichTextBox txtDetails;
        private Label label6;
        private Label label7;
        private Label label11;
        private Label label10;
        private Panel panel2;
        private TextBox txtDiscountAmount;
        private TextBox txtDepositAmount;
        private Label label5;
        private Label label4;
        private Button btnRealReset;
        private Button btnRealSave;
        private Label labelCustomerID;
        private Label labelCustomerName;
        private Label label9;
        private Label label3;
        private TextBox txtTotalAmount;
        private TextBox txtExternalBillName;
        private Button btn_AddCustomer;
        private Label labelRealInvoiceID;
        private Label label13;
        private TextBox txtSearchOrvc;
        private Panel panel1;
        private Label label12;
        private Button Btn_EditOrvc;
        private Button Btn_ImportOrvc;
        private SplitContainer splitContainer1;
        private Label label17;
        private Label label16;
        private Label label15;
        private Button BtnVoidOrvc;
    }
}