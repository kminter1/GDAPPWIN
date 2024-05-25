namespace GDAPPWIN
{
    partial class CashInForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            label1 = new Label();
            panel5 = new Panel();
            label13 = new Label();
            label9 = new Label();
            txtSearchRealInvoice = new TextBox();
            label10 = new Label();
            dataGridViewRealInvoices = new DataGridView();
            panel6 = new Panel();
            richTextBoxRemark = new RichTextBox();
            label6 = new Label();
            txtReceiptNumber = new TextBox();
            label7 = new Label();
            label17 = new Label();
            dateTimePickerReceiptDate = new DateTimePicker();
            label3 = new Label();
            comboBoxPaymentMethod = new ComboBox();
            label16 = new Label();
            label11 = new Label();
            txtFee = new TextBox();
            label5 = new Label();
            label18 = new Label();
            label4 = new Label();
            label8 = new Label();
            txtWithHoldingTax = new TextBox();
            label12 = new Label();
            label19 = new Label();
            label_CustomerName = new Label();
            bntImportORVC = new Button();
            label_CustomerID = new Label();
            labelDiscountAmount = new Label();
            label2 = new Label();
            labelTotalAmount = new Label();
            labelGetMoney = new Label();
            labelDiscountToPay = new Label();
            labelDepositToPay = new Label();
            txtNetCash = new TextBox();
            btnCancle = new Button();
            label14 = new Label();
            labelDepositAmount = new Label();
            label_Invdetails = new Label();
            btnSaveReceipts = new Button();
            label15 = new Label();
            panel4 = new Panel();
            labeltotalAmountPaid = new Label();
            label20 = new Label();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRealInvoices).BeginInit();
            panel6.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 42);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(461, 9);
            label1.Name = "label1";
            label1.Size = new Size(232, 21);
            label1.TabIndex = 1;
            label1.Text = "เพิ่มบิล/เบิกส่วนลด/เงินฝาก (ORCC)";
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.ControlLight;
            panel5.Controls.Add(label13);
            panel5.Controls.Add(label9);
            panel5.Controls.Add(txtSearchRealInvoice);
            panel5.Controls.Add(label10);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(1134, 34);
            panel5.TabIndex = 7;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.White;
            label13.Location = new Point(113, 7);
            label13.Name = "label13";
            label13.Size = new Size(38, 15);
            label13.TabIndex = 5;
            label13.Text = "รอเบิก";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Green;
            label9.Location = new Point(12, 6);
            label9.Name = "label9";
            label9.Size = new Size(79, 15);
            label9.TabIndex = 4;
            label9.Text = "ส่งเรื่องเบิกแล้ว";
            // 
            // txtSearchRealInvoice
            // 
            txtSearchRealInvoice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearchRealInvoice.Location = new Point(973, 5);
            txtSearchRealInvoice.Name = "txtSearchRealInvoice";
            txtSearchRealInvoice.Size = new Size(158, 23);
            txtSearchRealInvoice.TabIndex = 10;
            txtSearchRealInvoice.TextChanged += txtSearchRealInvoice_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(863, 7);
            label10.Name = "label10";
            label10.Size = new Size(104, 19);
            label10.TabIndex = 1;
            label10.Text = "ค้นหาบิล (ORVC)";
            // 
            // dataGridViewRealInvoices
            // 
            dataGridViewRealInvoices.AllowUserToAddRows = false;
            dataGridViewRealInvoices.AllowUserToDeleteRows = false;
            dataGridViewRealInvoices.AllowUserToOrderColumns = true;
            dataGridViewRealInvoices.AllowUserToResizeRows = false;
            dataGridViewRealInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewRealInvoices.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewRealInvoices.Dock = DockStyle.Fill;
            dataGridViewRealInvoices.Location = new Point(0, 34);
            dataGridViewRealInvoices.Name = "dataGridViewRealInvoices";
            dataGridViewRealInvoices.RowHeadersVisible = false;
            dataGridViewRealInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewRealInvoices.Size = new Size(1134, 497);
            dataGridViewRealInvoices.TabIndex = 8;
            // 
            // panel6
            // 
            panel6.Controls.Add(dataGridViewRealInvoices);
            panel6.Controls.Add(panel5);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 319);
            panel6.Name = "panel6";
            panel6.Size = new Size(1134, 531);
            panel6.TabIndex = 14;
            // 
            // richTextBoxRemark
            // 
            richTextBoxRemark.Location = new Point(476, 163);
            richTextBoxRemark.Name = "richTextBoxRemark";
            richTextBoxRemark.Size = new Size(200, 66);
            richTextBoxRemark.TabIndex = 6;
            richTextBoxRemark.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(434, 104);
            label6.Name = "label6";
            label6.Size = new Size(36, 21);
            label6.TabIndex = 1;
            label6.Text = "วันที่";
            // 
            // txtReceiptNumber
            // 
            txtReceiptNumber.Location = new Point(137, 69);
            txtReceiptNumber.Name = "txtReceiptNumber";
            txtReceiptNumber.Size = new Size(200, 23);
            txtReceiptNumber.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(407, 129);
            label7.Name = "label7";
            label7.Size = new Size(63, 21);
            label7.TabIndex = 1;
            label7.Text = "ชำระโดย";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 12F);
            label17.Location = new Point(43, 158);
            label17.Name = "label17";
            label17.Size = new Size(88, 21);
            label17.TabIndex = 1;
            label17.Text = "ค่าธรรมเนียม";
            // 
            // dateTimePickerReceiptDate
            // 
            dateTimePickerReceiptDate.Location = new Point(476, 102);
            dateTimePickerReceiptDate.Name = "dateTimePickerReceiptDate";
            dateTimePickerReceiptDate.Size = new Size(200, 23);
            dateTimePickerReceiptDate.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(42, 100);
            label3.Name = "label3";
            label3.Size = new Size(89, 21);
            label3.TabIndex = 1;
            label3.Text = "จำนวนรับจริง";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.FormattingEnabled = true;
            comboBoxPaymentMethod.Location = new Point(476, 131);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(200, 23);
            comboBoxPaymentMethod.TabIndex = 5;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F);
            label16.Location = new Point(398, 15);
            label16.Name = "label16";
            label16.Size = new Size(72, 21);
            label16.TabIndex = 1;
            label16.Text = "จำนวนเงิน";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 16F);
            label11.Location = new Point(737, 10);
            label11.Name = "label11";
            label11.Size = new Size(133, 30);
            label11.TabIndex = 1;
            label11.Text = "จำนวนเงินที่รับ";
            // 
            // txtFee
            // 
            txtFee.Location = new Point(137, 156);
            txtFee.Name = "txtFee";
            txtFee.Size = new Size(200, 23);
            txtFee.TabIndex = 3;
            txtFee.TextChanged += txtFee_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(416, 40);
            label5.Name = "label5";
            label5.Size = new Size(54, 21);
            label5.TabIndex = 1;
            label5.Text = "ส่วนลด";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 16F);
            label18.Location = new Point(717, 52);
            label18.Name = "label18";
            label18.Size = new Size(153, 30);
            label18.TabIndex = 1;
            label18.Text = "ส่วนลดที่ต้องจ่าย";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(49, 129);
            label4.Name = "label4";
            label4.Size = new Size(82, 21);
            label4.TabIndex = 1;
            label4.Text = "หัก ณ ที่จ่าย";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(407, 170);
            label8.Name = "label8";
            label8.Size = new Size(65, 21);
            label8.TabIndex = 1;
            label8.Text = "หมายเหตุ";
            // 
            // txtWithHoldingTax
            // 
            txtWithHoldingTax.Location = new Point(137, 127);
            txtWithHoldingTax.Name = "txtWithHoldingTax";
            txtWithHoldingTax.Size = new Size(200, 23);
            txtWithHoldingTax.TabIndex = 2;
            txtWithHoldingTax.TextChanged += txtWithHoldingTax_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F);
            label12.Location = new Point(414, 67);
            label12.Name = "label12";
            label12.Size = new Size(56, 21);
            label12.TabIndex = 11;
            label12.Text = "เงินฝาก";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 16F);
            label19.Location = new Point(713, 92);
            label19.Name = "label19";
            label19.Size = new Size(157, 30);
            label19.TabIndex = 11;
            label19.Text = "เงินฝากที่ต้องจ่าย";
            // 
            // label_CustomerName
            // 
            label_CustomerName.AutoSize = true;
            label_CustomerName.Location = new Point(210, 42);
            label_CustomerName.Name = "label_CustomerName";
            label_CustomerName.Size = new Size(12, 15);
            label_CustomerName.TabIndex = 10;
            label_CustomerName.Text = "*";
            // 
            // bntImportORVC
            // 
            bntImportORVC.Font = new Font("Segoe UI", 10F);
            bntImportORVC.Location = new Point(30, 216);
            bntImportORVC.Name = "bntImportORVC";
            bntImportORVC.Size = new Size(101, 40);
            bntImportORVC.TabIndex = 7;
            bntImportORVC.Text = "นำเข้าข้อมูล";
            bntImportORVC.UseVisualStyleBackColor = true;
            bntImportORVC.Click += btnImportORVC_Click;
            // 
            // label_CustomerID
            // 
            label_CustomerID.AutoSize = true;
            label_CustomerID.Location = new Point(137, 42);
            label_CustomerID.Name = "label_CustomerID";
            label_CustomerID.Size = new Size(12, 15);
            label_CustomerID.TabIndex = 9;
            label_CustomerID.Text = "*";
            // 
            // labelDiscountAmount
            // 
            labelDiscountAmount.AutoSize = true;
            labelDiscountAmount.Location = new Point(477, 48);
            labelDiscountAmount.Name = "labelDiscountAmount";
            labelDiscountAmount.Size = new Size(12, 15);
            labelDiscountAmount.TabIndex = 14;
            labelDiscountAmount.Text = "*";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(10, 71);
            label2.Name = "label2";
            label2.Size = new Size(121, 21);
            label2.TabIndex = 1;
            label2.Text = "เลขที่ใบเสร็จรับเงิน";
            // 
            // labelTotalAmount
            // 
            labelTotalAmount.AutoSize = true;
            labelTotalAmount.Location = new Point(476, 21);
            labelTotalAmount.Name = "labelTotalAmount";
            labelTotalAmount.Size = new Size(12, 15);
            labelTotalAmount.TabIndex = 13;
            labelTotalAmount.Text = "*";
            // 
            // labelGetMoney
            // 
            labelGetMoney.AutoSize = true;
            labelGetMoney.Font = new Font("Segoe UI", 16F);
            labelGetMoney.Location = new Point(883, 10);
            labelGetMoney.Name = "labelGetMoney";
            labelGetMoney.Size = new Size(22, 30);
            labelGetMoney.TabIndex = 16;
            labelGetMoney.Text = "*";
            // 
            // labelDiscountToPay
            // 
            labelDiscountToPay.AutoSize = true;
            labelDiscountToPay.Font = new Font("Segoe UI", 16F);
            labelDiscountToPay.Location = new Point(883, 52);
            labelDiscountToPay.Name = "labelDiscountToPay";
            labelDiscountToPay.Size = new Size(22, 30);
            labelDiscountToPay.TabIndex = 17;
            labelDiscountToPay.Text = "*";
            // 
            // labelDepositToPay
            // 
            labelDepositToPay.AutoSize = true;
            labelDepositToPay.Font = new Font("Segoe UI", 16F);
            labelDepositToPay.Location = new Point(883, 93);
            labelDepositToPay.Name = "labelDepositToPay";
            labelDepositToPay.Size = new Size(22, 30);
            labelDepositToPay.TabIndex = 18;
            labelDepositToPay.Text = "*";
            // 
            // txtNetCash
            // 
            txtNetCash.Location = new Point(137, 98);
            txtNetCash.Name = "txtNetCash";
            txtNetCash.Size = new Size(200, 23);
            txtNetCash.TabIndex = 1;
            txtNetCash.TextChanged += txtNetCash_TextChanged;
            // 
            // btnCancle
            // 
            btnCancle.Font = new Font("Segoe UI", 10F);
            btnCancle.Location = new Point(218, 216);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new Size(76, 40);
            btnCancle.TabIndex = 9;
            btnCancle.Text = "ยกเลิก";
            btnCancle.UseVisualStyleBackColor = true;
            btnCancle.Click += btnCancle_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(30, 42);
            label14.Name = "label14";
            label14.Size = new Size(61, 15);
            label14.TabIndex = 15;
            label14.Text = "ID/ชื่อลูกค้า";
            // 
            // labelDepositAmount
            // 
            labelDepositAmount.AutoSize = true;
            labelDepositAmount.Location = new Point(479, 72);
            labelDepositAmount.Name = "labelDepositAmount";
            labelDepositAmount.Size = new Size(12, 15);
            labelDepositAmount.TabIndex = 15;
            labelDepositAmount.Text = "*";
            // 
            // label_Invdetails
            // 
            label_Invdetails.AutoSize = true;
            label_Invdetails.Location = new Point(137, 21);
            label_Invdetails.Name = "label_Invdetails";
            label_Invdetails.Size = new Size(12, 15);
            label_Invdetails.TabIndex = 8;
            label_Invdetails.Text = "*";
            // 
            // btnSaveReceipts
            // 
            btnSaveReceipts.Font = new Font("Segoe UI", 10F);
            btnSaveReceipts.Location = new Point(137, 216);
            btnSaveReceipts.Name = "btnSaveReceipts";
            btnSaveReceipts.Size = new Size(75, 40);
            btnSaveReceipts.TabIndex = 8;
            btnSaveReceipts.Text = "บันทึก";
            btnSaveReceipts.UseVisualStyleBackColor = true;
            btnSaveReceipts.Click += btnSaveReceipt_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(30, 21);
            label15.Name = "label15";
            label15.Size = new Size(72, 15);
            label15.TabIndex = 16;
            label15.Text = "เลขที่ (ORVC)";
            // 
            // panel4
            // 
            panel4.Controls.Add(label15);
            panel4.Controls.Add(btnSaveReceipts);
            panel4.Controls.Add(label_Invdetails);
            panel4.Controls.Add(labelDepositAmount);
            panel4.Controls.Add(label14);
            panel4.Controls.Add(btnCancle);
            panel4.Controls.Add(txtNetCash);
            panel4.Controls.Add(labeltotalAmountPaid);
            panel4.Controls.Add(labelDepositToPay);
            panel4.Controls.Add(labelDiscountToPay);
            panel4.Controls.Add(labelGetMoney);
            panel4.Controls.Add(labelTotalAmount);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(labelDiscountAmount);
            panel4.Controls.Add(label_CustomerID);
            panel4.Controls.Add(bntImportORVC);
            panel4.Controls.Add(label_CustomerName);
            panel4.Controls.Add(label20);
            panel4.Controls.Add(label19);
            panel4.Controls.Add(label12);
            panel4.Controls.Add(txtWithHoldingTax);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(label18);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(txtFee);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(label16);
            panel4.Controls.Add(comboBoxPaymentMethod);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(dateTimePickerReceiptDate);
            panel4.Controls.Add(label17);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(txtReceiptNumber);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(richTextBoxRemark);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 42);
            panel4.Name = "panel4";
            panel4.Size = new Size(1134, 277);
            panel4.TabIndex = 13;
            // 
            // labeltotalAmountPaid
            // 
            labeltotalAmountPaid.AutoSize = true;
            labeltotalAmountPaid.Font = new Font("Segoe UI", 16F);
            labeltotalAmountPaid.Location = new Point(883, 189);
            labeltotalAmountPaid.Name = "labeltotalAmountPaid";
            labeltotalAmountPaid.Size = new Size(22, 30);
            labeltotalAmountPaid.TabIndex = 18;
            labeltotalAmountPaid.Text = "*";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 16F);
            label20.Location = new Point(738, 189);
            label20.Name = "label20";
            label20.Size = new Size(79, 30);
            label20.TabIndex = 11;
            label20.Text = "จ่ายแล้ว";
            // 
            // CashInForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1134, 850);
            Controls.Add(panel6);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Name = "CashInForm";
            Text = "CashInForm";
            Load += CashInForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRealInvoices).EndInit();
            panel6.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private Panel panel5;
        private Label label10;
        private TextBox txtSearchRealInvoice;
        private DataGridView dataGridViewRealInvoices;
        private Label label13;
        private Label label9;
        private Panel panel6;
        private RichTextBox richTextBoxRemark;
        private Label label6;
        private TextBox txtReceiptNumber;
        private Label label7;
        private Label label17;
        private DateTimePicker dateTimePickerReceiptDate;
        private Label label3;
        private ComboBox comboBoxPaymentMethod;
        private Label label16;
        private Label label11;
        private TextBox txtFee;
        private Label label5;
        private Label label18;
        private Label label4;
        private Label label8;
        private TextBox txtWithHoldingTax;
        private Label label12;
        private Label label19;
        private Label label_CustomerName;
        private Button bntImportORVC;
        private Label label_CustomerID;
        private Label labelDiscountAmount;
        private Label label2;
        private Label labelTotalAmount;
        private Label labelGetMoney;
        private Label labelDiscountToPay;
        private Label labelDepositToPay;
        private TextBox txtNetCash;
        private Button btnCancle;
        private Label label14;
        private Label labelDepositAmount;
        private Label label_Invdetails;
        private Button btnSaveReceipts;
        private Label label15;
        private Panel panel4;
        private Label labeltotalAmountPaid;
        private Label label20;
    }
}