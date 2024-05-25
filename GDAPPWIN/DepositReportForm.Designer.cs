namespace GDAPPWIN
{
    partial class DepositReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepositReportForm));
            btn_PrintReport2 = new Button();
            dateTimePickerByDate = new DateTimePicker();
            label_TotalDiscount = new Label();
            label_TotalDeposit = new Label();
            radioButtonAll = new RadioButton();
            radioButtonByDate = new RadioButton();
            buttonSearch = new Button();
            textBoxSearch = new TextBox();
            dateTimePickerStartDate = new DateTimePicker();
            dateTimePickerEndDate = new DateTimePicker();
            label3 = new Label();
            radioButtonBetweenDates = new RadioButton();
            dateTimePickerReport = new DateTimePicker();
            panel3 = new Panel();
            dataGridViewDiscountList = new DataGridView();
            panel2 = new Panel();
            label2 = new Label();
            dataGridViewDepositList = new DataGridView();
            panel1 = new Panel();
            label1 = new Label();
            panel4 = new Panel();
            btnSelectReport = new Button();
            label4 = new Label();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDiscountList).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDepositList).BeginInit();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // btn_PrintReport2
            // 
            btn_PrintReport2.Location = new Point(999, 8);
            btn_PrintReport2.Name = "btn_PrintReport2";
            btn_PrintReport2.Size = new Size(109, 23);
            btn_PrintReport2.TabIndex = 1;
            btn_PrintReport2.Text = "PRINT REPORT";
            btn_PrintReport2.UseVisualStyleBackColor = true;
            btn_PrintReport2.Click += btn_PrintReport2_Click;
            // 
            // dateTimePickerByDate
            // 
            dateTimePickerByDate.Location = new Point(114, 39);
            dateTimePickerByDate.Name = "dateTimePickerByDate";
            dateTimePickerByDate.Size = new Size(200, 23);
            dateTimePickerByDate.TabIndex = 2;
            // 
            // label_TotalDiscount
            // 
            label_TotalDiscount.AutoSize = true;
            label_TotalDiscount.Location = new Point(967, 15);
            label_TotalDiscount.Name = "label_TotalDiscount";
            label_TotalDiscount.Size = new Size(12, 15);
            label_TotalDiscount.TabIndex = 4;
            label_TotalDiscount.Text = "*";
            // 
            // label_TotalDeposit
            // 
            label_TotalDeposit.AutoSize = true;
            label_TotalDeposit.Location = new Point(967, 14);
            label_TotalDeposit.Name = "label_TotalDeposit";
            label_TotalDeposit.Size = new Size(12, 15);
            label_TotalDeposit.TabIndex = 5;
            label_TotalDeposit.Text = "*";
            // 
            // radioButtonAll
            // 
            radioButtonAll.AutoSize = true;
            radioButtonAll.Location = new Point(12, 12);
            radioButtonAll.Name = "radioButtonAll";
            radioButtonAll.Size = new Size(91, 19);
            radioButtonAll.TabIndex = 6;
            radioButtonAll.TabStop = true;
            radioButtonAll.Text = "รายงานทั้งหมด";
            radioButtonAll.UseVisualStyleBackColor = true;
            radioButtonAll.Click += radioButton_Click;
            // 
            // radioButtonByDate
            // 
            radioButtonByDate.AutoSize = true;
            radioButtonByDate.Location = new Point(12, 41);
            radioButtonByDate.Name = "radioButtonByDate";
            radioButtonByDate.Size = new Size(96, 19);
            radioButtonByDate.TabIndex = 7;
            radioButtonByDate.TabStop = true;
            radioButtonByDate.Text = "รายงานแยกวันที่";
            radioButtonByDate.UseVisualStyleBackColor = true;
            radioButtonByDate.Click += radioButton_Click;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(1007, 61);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(73, 23);
            buttonSearch.TabIndex = 8;
            buttonSearch.Text = "ค้นหา";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(777, 61);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(200, 23);
            textBoxSearch.TabIndex = 9;
            // 
            // dateTimePickerStartDate
            // 
            dateTimePickerStartDate.Location = new Point(114, 65);
            dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            dateTimePickerStartDate.Size = new Size(200, 23);
            dateTimePickerStartDate.TabIndex = 13;
            // 
            // dateTimePickerEndDate
            // 
            dateTimePickerEndDate.Location = new Point(344, 65);
            dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            dateTimePickerEndDate.Size = new Size(200, 23);
            dateTimePickerEndDate.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(320, 69);
            label3.Name = "label3";
            label3.Size = new Size(18, 15);
            label3.TabIndex = 15;
            label3.Text = "to";
            // 
            // radioButtonBetweenDates
            // 
            radioButtonBetweenDates.AutoSize = true;
            radioButtonBetweenDates.Location = new Point(12, 69);
            radioButtonBetweenDates.Name = "radioButtonBetweenDates";
            radioButtonBetweenDates.Size = new Size(75, 19);
            radioButtonBetweenDates.TabIndex = 16;
            radioButtonBetweenDates.TabStop = true;
            radioButtonBetweenDates.Text = "ระหว่างวันที่";
            radioButtonBetweenDates.UseVisualStyleBackColor = true;
            radioButtonBetweenDates.Click += radioButton_Click;
            // 
            // dateTimePickerReport
            // 
            dateTimePickerReport.Location = new Point(777, 8);
            dateTimePickerReport.Name = "dateTimePickerReport";
            dateTimePickerReport.Size = new Size(202, 23);
            dateTimePickerReport.TabIndex = 17;
            // 
            // panel3
            // 
            panel3.Controls.Add(dataGridViewDiscountList);
            panel3.Controls.Add(panel2);
            panel3.Controls.Add(dataGridViewDepositList);
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1134, 711);
            panel3.TabIndex = 18;
            // 
            // dataGridViewDiscountList
            // 
            dataGridViewDiscountList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDiscountList.Dock = DockStyle.Top;
            dataGridViewDiscountList.Location = new Point(0, 448);
            dataGridViewDiscountList.Name = "dataGridViewDiscountList";
            dataGridViewDiscountList.RowHeadersVisible = false;
            dataGridViewDiscountList.Size = new Size(1134, 300);
            dataGridViewDiscountList.TabIndex = 22;
            // 
            // panel2
            // 
            panel2.BackColor = Color.CadetBlue;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label_TotalDiscount);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 408);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 40);
            panel2.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(535, 10);
            label2.Name = "label2";
            label2.Size = new Size(99, 21);
            label2.TabIndex = 0;
            label2.Text = "รายงานส่วนลด";
            // 
            // dataGridViewDepositList
            // 
            dataGridViewDepositList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDepositList.Dock = DockStyle.Top;
            dataGridViewDepositList.Location = new Point(0, 161);
            dataGridViewDepositList.Name = "dataGridViewDepositList";
            dataGridViewDepositList.RowHeadersVisible = false;
            dataGridViewDepositList.Size = new Size(1134, 247);
            dataGridViewDepositList.TabIndex = 20;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label_TotalDeposit);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 121);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 40);
            panel1.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(516, 8);
            label1.Name = "label1";
            label1.Size = new Size(101, 21);
            label1.TabIndex = 0;
            label1.Text = "รายงานเงินฝาก";
            // 
            // panel4
            // 
            panel4.Controls.Add(btnSelectReport);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(radioButtonAll);
            panel4.Controls.Add(dateTimePickerEndDate);
            panel4.Controls.Add(dateTimePickerStartDate);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(textBoxSearch);
            panel4.Controls.Add(dateTimePickerByDate);
            panel4.Controls.Add(dateTimePickerReport);
            panel4.Controls.Add(btn_PrintReport2);
            panel4.Controls.Add(buttonSearch);
            panel4.Controls.Add(radioButtonByDate);
            panel4.Controls.Add(radioButtonBetweenDates);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(1134, 121);
            panel4.TabIndex = 18;
            // 
            // btnSelectReport
            // 
            btnSelectReport.Location = new Point(571, 65);
            btnSelectReport.Name = "btnSelectReport";
            btnSelectReport.Size = new Size(75, 23);
            btnSelectReport.TabIndex = 19;
            btnSelectReport.Text = "รายงาน";
            btnSelectReport.UseVisualStyleBackColor = true;
            btnSelectReport.Click += btnSelectReport_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(728, 65);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 18;
            label4.Text = "ค้นหา";
            // 
            // DepositReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DepositReportForm";
            Text = "DepositReportForm";
            Load += DepositReportForm_Load;
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewDiscountList).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDepositList).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btn_PrintReport2;
        private DateTimePicker dateTimePickerByDate;
        private Label label_TotalDiscount;
        private Label label_TotalDeposit;
        private RadioButton radioButtonAll;
        private RadioButton radioButtonByDate;
        private Button buttonSearch;
        private TextBox textBoxSearch;
        private DateTimePicker dateTimePickerStartDate;
        private DateTimePicker dateTimePickerEndDate;
        private Label label3;
        private RadioButton radioButtonBetweenDates;
        private DateTimePicker dateTimePickerReport;
        private Panel panel3;
        private Panel panel1;
        private Panel panel4;
        private Panel panel2;
        private Label label2;
        private DataGridView dataGridViewDepositList;
        private Label label1;
        private DataGridView dataGridViewDiscountList;
        private Button btnSelectReport;
        private Label label4;
    }
}