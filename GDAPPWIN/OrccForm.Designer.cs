namespace GDAPPWIN
{
    partial class OrccForm
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
            panel1 = new Panel();
            label1 = new Label();
            dataGridView_Orcc = new DataGridView();
            panel2 = new Panel();
            BtnVoidOrcc = new Button();
            BtnEditOrcc = new Button();
            txtSearchOrcc = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label9 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            richTextBoxRemark = new RichTextBox();
            comboBoxPaymentMethod = new ComboBox();
            txtFee = new TextBox();
            txtWithHoldingTax = new TextBox();
            txtNetCash = new TextBox();
            dateTimePickerReceiptDate = new DateTimePicker();
            txtReceiptNumber = new TextBox();
            panel3 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Orcc).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 40);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(508, 9);
            label1.Name = "label1";
            label1.Size = new Size(170, 21);
            label1.TabIndex = 0;
            label1.Text = "รายการบิลใบเสร็จ (ORCC)";
            // 
            // dataGridView_Orcc
            // 
            dataGridView_Orcc.AllowUserToAddRows = false;
            dataGridView_Orcc.AllowUserToDeleteRows = false;
            dataGridView_Orcc.AllowUserToResizeRows = false;
            dataGridView_Orcc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView_Orcc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Orcc.Dock = DockStyle.Fill;
            dataGridView_Orcc.Location = new Point(0, 0);
            dataGridView_Orcc.Name = "dataGridView_Orcc";
            dataGridView_Orcc.RowHeadersVisible = false;
            dataGridView_Orcc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Orcc.Size = new Size(1134, 433);
            dataGridView_Orcc.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(BtnVoidOrcc);
            panel2.Controls.Add(BtnEditOrcc);
            panel2.Controls.Add(txtSearchOrcc);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(richTextBoxRemark);
            panel2.Controls.Add(comboBoxPaymentMethod);
            panel2.Controls.Add(txtFee);
            panel2.Controls.Add(txtWithHoldingTax);
            panel2.Controls.Add(txtNetCash);
            panel2.Controls.Add(dateTimePickerReceiptDate);
            panel2.Controls.Add(txtReceiptNumber);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 238);
            panel2.TabIndex = 2;
            // 
            // BtnVoidOrcc
            // 
            BtnVoidOrcc.Font = new Font("Segoe UI", 11F);
            BtnVoidOrcc.Location = new Point(793, 6);
            BtnVoidOrcc.Name = "BtnVoidOrcc";
            BtnVoidOrcc.Size = new Size(84, 37);
            BtnVoidOrcc.TabIndex = 8;
            BtnVoidOrcc.Text = "ยกเลิกบิล";
            BtnVoidOrcc.UseVisualStyleBackColor = true;
            // 
            // BtnEditOrcc
            // 
            BtnEditOrcc.Font = new Font("Segoe UI", 11F);
            BtnEditOrcc.Location = new Point(703, 6);
            BtnEditOrcc.Name = "BtnEditOrcc";
            BtnEditOrcc.Size = new Size(84, 37);
            BtnEditOrcc.TabIndex = 7;
            BtnEditOrcc.Text = "แก้ไข";
            BtnEditOrcc.UseVisualStyleBackColor = true;
            // 
            // txtSearchOrcc
            // 
            txtSearchOrcc.Location = new Point(111, 209);
            txtSearchOrcc.Name = "txtSearchOrcc";
            txtSearchOrcc.Size = new Size(200, 23);
            txtSearchOrcc.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(639, 71);
            label8.Name = "label8";
            label8.Size = new Size(58, 19);
            label8.TabIndex = 7;
            label8.Text = "หมายเหตุ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(340, 100);
            label7.Name = "label7";
            label7.Size = new Size(55, 19);
            label7.TabIndex = 7;
            label7.Text = "ชำระโดย";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(364, 71);
            label6.Name = "label6";
            label6.Size = new Size(31, 19);
            label6.TabIndex = 7;
            label6.Text = "วันที่";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(8, 213);
            label9.Name = "label9";
            label9.Size = new Size(55, 19);
            label9.TabIndex = 7;
            label9.Text = "ค้นหาบิล";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(8, 159);
            label5.Name = "label5";
            label5.Size = new Size(77, 19);
            label5.TabIndex = 7;
            label5.Text = "ค่าธรรมเนียม";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(12, 130);
            label4.Name = "label4";
            label4.Size = new Size(73, 19);
            label4.TabIndex = 7;
            label4.Text = "หัก ณ ที่จ่าย";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(12, 101);
            label3.Name = "label3";
            label3.Size = new Size(76, 19);
            label3.TabIndex = 7;
            label3.Text = "จำนวนรับจริง";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(75, 19);
            label2.TabIndex = 7;
            label2.Text = "เลขที่ใบเสร็จ";
            // 
            // richTextBoxRemark
            // 
            richTextBoxRemark.Location = new Point(703, 69);
            richTextBoxRemark.Name = "richTextBoxRemark";
            richTextBoxRemark.Size = new Size(200, 65);
            richTextBoxRemark.TabIndex = 6;
            richTextBoxRemark.Text = "";
            // 
            // comboBoxPaymentMethod
            // 
            comboBoxPaymentMethod.FormattingEnabled = true;
            comboBoxPaymentMethod.Location = new Point(401, 96);
            comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
            comboBoxPaymentMethod.Size = new Size(200, 23);
            comboBoxPaymentMethod.TabIndex = 5;
            // 
            // txtFee
            // 
            txtFee.Location = new Point(111, 155);
            txtFee.Name = "txtFee";
            txtFee.Size = new Size(200, 23);
            txtFee.TabIndex = 3;
            // 
            // txtWithHoldingTax
            // 
            txtWithHoldingTax.Location = new Point(111, 126);
            txtWithHoldingTax.Name = "txtWithHoldingTax";
            txtWithHoldingTax.Size = new Size(200, 23);
            txtWithHoldingTax.TabIndex = 2;
            // 
            // txtNetCash
            // 
            txtNetCash.Location = new Point(111, 97);
            txtNetCash.Name = "txtNetCash";
            txtNetCash.Size = new Size(200, 23);
            txtNetCash.TabIndex = 1;
            // 
            // dateTimePickerReceiptDate
            // 
            dateTimePickerReceiptDate.Location = new Point(401, 67);
            dateTimePickerReceiptDate.Name = "dateTimePickerReceiptDate";
            dateTimePickerReceiptDate.Size = new Size(200, 23);
            dateTimePickerReceiptDate.TabIndex = 4;
            // 
            // txtReceiptNumber
            // 
            txtReceiptNumber.Location = new Point(111, 68);
            txtReceiptNumber.Name = "txtReceiptNumber";
            txtReceiptNumber.Size = new Size(200, 23);
            txtReceiptNumber.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.Controls.Add(dataGridView_Orcc);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 278);
            panel3.Name = "panel3";
            panel3.Size = new Size(1134, 433);
            panel3.TabIndex = 3;
            // 
            // OrccForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "OrccForm";
            Text = "OrccForm";
            Load += OrccForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Orcc).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView dataGridView_Orcc;
        private Panel panel2;
        private DateTimePicker dateTimePickerReceiptDate;
        private TextBox txtReceiptNumber;
        private Panel panel3;
        private TextBox txtFee;
        private TextBox txtWithHoldingTax;
        private TextBox txtNetCash;
        private ComboBox comboBoxPaymentMethod;
        private RichTextBox richTextBoxRemark;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label5;
        private Label label8;
        private Label label7;
        private Label label6;
        private TextBox txtSearchOrcc;
        private Label label9;
        private Button BtnEditOrcc;
        private Button BtnVoidOrcc;
    }
}