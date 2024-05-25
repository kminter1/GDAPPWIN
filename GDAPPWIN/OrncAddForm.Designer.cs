namespace GDAPPWIN
{
    partial class OrncAddForm
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
            textBox_OrncNo = new TextBox();
            dateTimePicker_Ornc = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            textBox_OrncAmount = new TextBox();
            label4 = new Label();
            label5 = new Label();
            richTextBox_OrncDetail = new RichTextBox();
            button_OrncOk = new Button();
            btn_OrncCancle = new Button();
            panel_orncTop = new Panel();
            label6 = new Label();
            labelCustomerID = new Label();
            label9 = new Label();
            textBox_OrncDeposit = new TextBox();
            textBox_OrncDiscount = new TextBox();
            label8 = new Label();
            label7 = new Label();
            btn_PrintReport = new Button();
            lbl_TotalOrnc = new Label();
            dateTimePickerOrncSearch = new DateTimePicker();
            txtSearchTerm = new TextBox();
            dataGridViewOrncList = new DataGridView();
            labelCustomerName = new Label();
            btn_AddCustomer = new Button();
            btnUpdateOrnc = new Button();
            btn_EditData = new Button();
            labelTemporaryInvoiceID = new Label();
            label10 = new Label();
            splitContainer1 = new SplitContainer();
            BtnIsVoided = new Button();
            label11 = new Label();
            OutStandingDate = new DateTimePicker();
            BtnByDate = new Button();
            BtnByMonth = new Button();
            panel_orncTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrncList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_OrncNo
            // 
            textBox_OrncNo.Location = new Point(110, 213);
            textBox_OrncNo.Name = "textBox_OrncNo";
            textBox_OrncNo.Size = new Size(200, 23);
            textBox_OrncNo.TabIndex = 4;
            // 
            // dateTimePicker_Ornc
            // 
            dateTimePicker_Ornc.Location = new Point(770, 209);
            dateTimePicker_Ornc.Name = "dateTimePicker_Ornc";
            dateTimePicker_Ornc.Size = new Size(200, 23);
            dateTimePicker_Ornc.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(0, 213);
            label2.Name = "label2";
            label2.Size = new Size(96, 21);
            label2.TabIndex = 3;
            label2.Text = "เลขที่ใบส่งของ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(660, 211);
            label3.Name = "label3";
            label3.Size = new Size(36, 21);
            label3.TabIndex = 3;
            label3.Text = "วันที่";
            // 
            // textBox_OrncAmount
            // 
            textBox_OrncAmount.Location = new Point(110, 242);
            textBox_OrncAmount.Name = "textBox_OrncAmount";
            textBox_OrncAmount.Size = new Size(200, 23);
            textBox_OrncAmount.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(0, 244);
            label4.Name = "label4";
            label4.Size = new Size(72, 21);
            label4.TabIndex = 3;
            label4.Text = "จำนวนเงิน";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(660, 237);
            label5.Name = "label5";
            label5.Size = new Size(75, 21);
            label5.TabIndex = 3;
            label5.Text = "รายละเอียด";
            // 
            // richTextBox_OrncDetail
            // 
            richTextBox_OrncDetail.Location = new Point(770, 238);
            richTextBox_OrncDetail.Name = "richTextBox_OrncDetail";
            richTextBox_OrncDetail.Size = new Size(200, 60);
            richTextBox_OrncDetail.TabIndex = 9;
            richTextBox_OrncDetail.Text = "";
            // 
            // button_OrncOk
            // 
            button_OrncOk.Font = new Font("Segoe UI", 11F);
            button_OrncOk.Location = new Point(161, 21);
            button_OrncOk.Name = "button_OrncOk";
            button_OrncOk.Size = new Size(75, 38);
            button_OrncOk.TabIndex = 11;
            button_OrncOk.Text = "บันทึก";
            button_OrncOk.UseVisualStyleBackColor = true;
            button_OrncOk.Click += button_OrncOk_Click;
            // 
            // btn_OrncCancle
            // 
            btn_OrncCancle.Font = new Font("Segoe UI", 11F);
            btn_OrncCancle.Location = new Point(242, 21);
            btn_OrncCancle.Name = "btn_OrncCancle";
            btn_OrncCancle.Size = new Size(75, 38);
            btn_OrncCancle.TabIndex = 12;
            btn_OrncCancle.Text = "ยกเลิก";
            btn_OrncCancle.UseVisualStyleBackColor = true;
            btn_OrncCancle.Click += btn_OrncCancle_click;
            // 
            // panel_orncTop
            // 
            panel_orncTop.BackColor = Color.RoyalBlue;
            panel_orncTop.Controls.Add(label6);
            panel_orncTop.Dock = DockStyle.Top;
            panel_orncTop.Location = new Point(0, 0);
            panel_orncTop.Name = "panel_orncTop";
            panel_orncTop.Size = new Size(1134, 53);
            panel_orncTop.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = Color.White;
            label6.Location = new Point(428, 14);
            label6.Name = "label6";
            label6.Size = new Size(191, 21);
            label6.TabIndex = 0;
            label6.Text = "เพิ่มใบส่งของชั่วคราว (ORNC)";
            // 
            // labelCustomerID
            // 
            labelCustomerID.AutoSize = true;
            labelCustomerID.Location = new Point(110, 158);
            labelCustomerID.Name = "labelCustomerID";
            labelCustomerID.Size = new Size(12, 15);
            labelCustomerID.TabIndex = 9;
            labelCustomerID.Text = "*";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(0, 183);
            label9.Name = "label9";
            label9.Size = new Size(62, 21);
            label9.TabIndex = 8;
            label9.Text = "ชื่อลูกค้า";
            // 
            // textBox_OrncDeposit
            // 
            textBox_OrncDeposit.Location = new Point(435, 240);
            textBox_OrncDeposit.Name = "textBox_OrncDeposit";
            textBox_OrncDeposit.Size = new Size(200, 23);
            textBox_OrncDeposit.TabIndex = 7;
            // 
            // textBox_OrncDiscount
            // 
            textBox_OrncDiscount.Location = new Point(435, 211);
            textBox_OrncDiscount.Name = "textBox_OrncDiscount";
            textBox_OrncDiscount.Size = new Size(200, 23);
            textBox_OrncDiscount.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(325, 242);
            label8.Name = "label8";
            label8.Size = new Size(56, 21);
            label8.TabIndex = 3;
            label8.Text = "เงินฝาก";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(325, 213);
            label7.Name = "label7";
            label7.Size = new Size(54, 21);
            label7.TabIndex = 3;
            label7.Text = "ส่วนลด";
            // 
            // btn_PrintReport
            // 
            btn_PrintReport.Font = new Font("Segoe UI", 10F);
            btn_PrintReport.Location = new Point(983, 79);
            btn_PrintReport.Name = "btn_PrintReport";
            btn_PrintReport.Size = new Size(90, 31);
            btn_PrintReport.TabIndex = 17;
            btn_PrintReport.Text = "พิมพ์รายงาน";
            btn_PrintReport.UseVisualStyleBackColor = true;
            btn_PrintReport.Click += btn_PrintReport_Click;
            // 
            // lbl_TotalOrnc
            // 
            lbl_TotalOrnc.AutoSize = true;
            lbl_TotalOrnc.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_TotalOrnc.Location = new Point(635, 338);
            lbl_TotalOrnc.Name = "lbl_TotalOrnc";
            lbl_TotalOrnc.Size = new Size(17, 21);
            lbl_TotalOrnc.TabIndex = 3;
            lbl_TotalOrnc.Text = "*";
            // 
            // dateTimePickerOrncSearch
            // 
            dateTimePickerOrncSearch.Location = new Point(770, 87);
            dateTimePickerOrncSearch.Name = "dateTimePickerOrncSearch";
            dateTimePickerOrncSearch.Size = new Size(200, 23);
            dateTimePickerOrncSearch.TabIndex = 16;
            // 
            // txtSearchTerm
            // 
            txtSearchTerm.Location = new Point(934, 340);
            txtSearchTerm.Name = "txtSearchTerm";
            txtSearchTerm.Size = new Size(200, 23);
            txtSearchTerm.TabIndex = 18;
            txtSearchTerm.TextChanged += txtSearchTerm1_TextChanged;
            // 
            // dataGridViewOrncList
            // 
            dataGridViewOrncList.AllowUserToAddRows = false;
            dataGridViewOrncList.AllowUserToDeleteRows = false;
            dataGridViewOrncList.AllowUserToResizeRows = false;
            dataGridViewOrncList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewOrncList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewOrncList.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewOrncList.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewOrncList.Location = new Point(0, 369);
            dataGridViewOrncList.Name = "dataGridViewOrncList";
            dataGridViewOrncList.RowHeadersVisible = false;
            dataGridViewOrncList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrncList.Size = new Size(1134, 342);
            dataGridViewOrncList.TabIndex = 0;
            dataGridViewOrncList.CellContentClick += dataGridViewOrncList_CellContentClick;
            // 
            // labelCustomerName
            // 
            labelCustomerName.AutoSize = true;
            labelCustomerName.Location = new Point(110, 183);
            labelCustomerName.Name = "labelCustomerName";
            labelCustomerName.Size = new Size(12, 15);
            labelCustomerName.TabIndex = 9;
            labelCustomerName.Text = "*";
            // 
            // btn_AddCustomer
            // 
            btn_AddCustomer.Font = new Font("Segoe UI", 10F);
            btn_AddCustomer.Location = new Point(25, 21);
            btn_AddCustomer.Name = "btn_AddCustomer";
            btn_AddCustomer.Size = new Size(130, 38);
            btn_AddCustomer.TabIndex = 10;
            btn_AddCustomer.Text = "เลือกลูกค้า";
            btn_AddCustomer.UseVisualStyleBackColor = true;
            btn_AddCustomer.Click += btn_AddCustomer_Click;
            // 
            // btnUpdateOrnc
            // 
            btnUpdateOrnc.Font = new Font("Segoe UI", 11F);
            btnUpdateOrnc.Location = new Point(126, 21);
            btnUpdateOrnc.Name = "btnUpdateOrnc";
            btnUpdateOrnc.Size = new Size(75, 38);
            btnUpdateOrnc.TabIndex = 14;
            btnUpdateOrnc.Text = "แก้ไข";
            btnUpdateOrnc.UseVisualStyleBackColor = true;
            btnUpdateOrnc.Click += BtnUpdateOrnc_Click;
            // 
            // btn_EditData
            // 
            btn_EditData.Font = new Font("Segoe UI", 11F);
            btn_EditData.Location = new Point(17, 21);
            btn_EditData.Name = "btn_EditData";
            btn_EditData.Size = new Size(103, 38);
            btn_EditData.TabIndex = 13;
            btn_EditData.Text = "นำเข้าข้อมูล";
            btn_EditData.UseVisualStyleBackColor = true;
            btn_EditData.Click += btn_EditData_Click;
            // 
            // labelTemporaryInvoiceID
            // 
            labelTemporaryInvoiceID.AutoSize = true;
            labelTemporaryInvoiceID.Font = new Font("Segoe UI", 12F);
            labelTemporaryInvoiceID.Location = new Point(257, 151);
            labelTemporaryInvoiceID.Name = "labelTemporaryInvoiceID";
            labelTemporaryInvoiceID.Size = new Size(17, 21);
            labelTemporaryInvoiceID.TabIndex = 9;
            labelTemporaryInvoiceID.Text = "*";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F);
            label10.ForeColor = Color.FromArgb(192, 0, 0);
            label10.Location = new Point(157, 151);
            label10.Name = "label10";
            label10.Size = new Size(83, 21);
            label10.TabIndex = 8;
            label10.Text = "ID (ORNC)";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 59);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.ActiveCaption;
            splitContainer1.Panel1.Controls.Add(button_OrncOk);
            splitContainer1.Panel1.Controls.Add(btn_AddCustomer);
            splitContainer1.Panel1.Controls.Add(btn_OrncCancle);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.Info;
            splitContainer1.Panel2.Controls.Add(btn_EditData);
            splitContainer1.Panel2.Controls.Add(BtnIsVoided);
            splitContainer1.Panel2.Controls.Add(btnUpdateOrnc);
            splitContainer1.Size = new Size(647, 77);
            splitContainer1.SplitterDistance = 331;
            splitContainer1.TabIndex = 11;
            // 
            // BtnIsVoided
            // 
            BtnIsVoided.Font = new Font("Segoe UI", 11F);
            BtnIsVoided.Location = new Point(207, 21);
            BtnIsVoided.Name = "BtnIsVoided";
            BtnIsVoided.Size = new Size(75, 38);
            BtnIsVoided.TabIndex = 15;
            BtnIsVoided.Text = "ยกเลิกบิล";
            BtnIsVoided.UseVisualStyleBackColor = true;
            BtnIsVoided.Click += BtnIsVoided_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.Location = new Point(0, 153);
            label11.Name = "label11";
            label11.Size = new Size(73, 21);
            label11.TabIndex = 8;
            label11.Text = "ID (ลูกค้า)";
            // 
            // OutStandingDate
            // 
            OutStandingDate.Location = new Point(12, 336);
            OutStandingDate.Name = "OutStandingDate";
            OutStandingDate.Size = new Size(200, 23);
            OutStandingDate.TabIndex = 19;
            // 
            // BtnByDate
            // 
            BtnByDate.Location = new Point(230, 336);
            BtnByDate.Name = "BtnByDate";
            BtnByDate.Size = new Size(75, 23);
            BtnByDate.TabIndex = 20;
            BtnByDate.Text = "รายวัน";
            BtnByDate.UseVisualStyleBackColor = true;
            BtnByDate.Click += BtnByDate_Click;
            // 
            // BtnByMonth
            // 
            BtnByMonth.Location = new Point(323, 336);
            BtnByMonth.Name = "BtnByMonth";
            BtnByMonth.Size = new Size(75, 23);
            BtnByMonth.TabIndex = 21;
            BtnByMonth.Text = "รายเดือน";
            BtnByMonth.UseVisualStyleBackColor = true;
            BtnByMonth.Click += BtnByMonth_Click;
            // 
            // OrncAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(BtnByMonth);
            Controls.Add(BtnByDate);
            Controls.Add(OutStandingDate);
            Controls.Add(splitContainer1);
            Controls.Add(btn_PrintReport);
            Controls.Add(labelTemporaryInvoiceID);
            Controls.Add(labelCustomerName);
            Controls.Add(labelCustomerID);
            Controls.Add(dateTimePickerOrncSearch);
            Controls.Add(lbl_TotalOrnc);
            Controls.Add(dataGridViewOrncList);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(label9);
            Controls.Add(txtSearchTerm);
            Controls.Add(panel_orncTop);
            Controls.Add(richTextBox_OrncDetail);
            Controls.Add(dateTimePicker_Ornc);
            Controls.Add(textBox_OrncNo);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(textBox_OrncAmount);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox_OrncDiscount);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(textBox_OrncDeposit);
            Name = "OrncAddForm";
            Text = "OrncAddForm";
            Load += OrncAddForm_Load;
            panel_orncTop.ResumeLayout(false);
            panel_orncTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrncList).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox_OrncNo;
        private DateTimePicker dateTimePicker_Ornc;
        private Label label2;
        private Label label3;
        private TextBox textBox_OrncAmount;
        private Label label4;
        private Label label5;
        private RichTextBox richTextBox_OrncDetail;
        private Button button_OrncOk;
        private Button btn_OrncCancle;
        private Panel panel_orncTop;
        private Panel panel1;
        private Label label6;
        private DataGridViewTextBoxColumn ornc_id;
        private DataGridViewTextBoxColumn ornc_date;
        private DataGridViewTextBoxColumn CusID;
        private DataGridViewTextBoxColumn ornc_detail;
        private DataGridViewTextBoxColumn ornc_no;
        private DataGridViewTextBoxColumn ornc_amount;
        private DateTimePicker dateTimePickerOrncSearch;
        private Label lbl_TotalOrnc;
        private Button btn_PrintReport;
        private TextBox textBox_OrncDeposit;
        private TextBox textBox_OrncDiscount;
        private Label label8;
        private Label label7;
        private Label labelCustomerID;
        private Label label9;
        private TextBox txtSearchTerm;
        private DataGridView dataGridViewOrncList;
        private Label labelCustomerName;
        private Button btn_AddCustomer;
        private Button btnUpdateOrnc;
        private Button btn_EditData;
        private Label labelTemporaryInvoiceID;
        private Label label10;
        private SplitContainer splitContainer1;
        private Label label11;
        private Button BtnIsVoided;
        private DateTimePicker OutStandingDate;
        private Button BtnByDate;
        private Button BtnByMonth;
    }
}