namespace GDAPPWIN
{
    partial class NotPaidForm
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
            panel1 = new Panel();
            label1 = new Label();
            dataGridViewNotPaidInvoices = new DataGridView();
            panel2 = new Panel();
            label3 = new Label();
            label2 = new Label();
            datePickerEnd = new DateTimePicker();
            txtSearchTerm = new TextBox();
            btnFilter = new Button();
            datePickerStart = new DateTimePicker();
            labelDiscountAmountSum = new Label();
            labelDepositAmountSum = new Label();
            labelTotalAmountSum = new Label();
            panel3 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewNotPaidInvoices).BeginInit();
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
            label1.Location = new Point(472, 11);
            label1.Name = "label1";
            label1.Size = new Size(152, 21);
            label1.TabIndex = 0;
            label1.Text = "รายงานบิลลูกค้าค้างจ่าย";
            // 
            // dataGridViewNotPaidInvoices
            // 
            dataGridViewNotPaidInvoices.AllowUserToAddRows = false;
            dataGridViewNotPaidInvoices.AllowUserToDeleteRows = false;
            dataGridViewNotPaidInvoices.AllowUserToResizeRows = false;
            dataGridViewNotPaidInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewNotPaidInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewNotPaidInvoices.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewNotPaidInvoices.Dock = DockStyle.Fill;
            dataGridViewNotPaidInvoices.Location = new Point(0, 0);
            dataGridViewNotPaidInvoices.Name = "dataGridViewNotPaidInvoices";
            dataGridViewNotPaidInvoices.RowHeadersVisible = false;
            dataGridViewNotPaidInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewNotPaidInvoices.Size = new Size(1134, 560);
            dataGridViewNotPaidInvoices.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(datePickerEnd);
            panel2.Controls.Add(txtSearchTerm);
            panel2.Controls.Add(btnFilter);
            panel2.Controls.Add(datePickerStart);
            panel2.Controls.Add(labelDiscountAmountSum);
            panel2.Controls.Add(labelDepositAmountSum);
            panel2.Controls.Add(labelTotalAmountSum);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 600);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 111);
            panel2.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(205, 21);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 13;
            label3.Text = "ค้นหา";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(219, 74);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 12;
            label2.Text = "ถึง";
            // 
            // datePickerEnd
            // 
            datePickerEnd.Location = new Point(244, 70);
            datePickerEnd.Name = "datePickerEnd";
            datePickerEnd.Size = new Size(200, 23);
            datePickerEnd.TabIndex = 11;
            // 
            // txtSearchTerm
            // 
            txtSearchTerm.Location = new Point(244, 16);
            txtSearchTerm.Name = "txtSearchTerm";
            txtSearchTerm.Size = new Size(200, 23);
            txtSearchTerm.TabIndex = 10;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(472, 72);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(75, 23);
            btnFilter.TabIndex = 9;
            btnFilter.Text = "ตกลง";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // datePickerStart
            // 
            datePickerStart.Location = new Point(13, 70);
            datePickerStart.Name = "datePickerStart";
            datePickerStart.Size = new Size(200, 23);
            datePickerStart.TabIndex = 8;
            // 
            // labelDiscountAmountSum
            // 
            labelDiscountAmountSum.AutoSize = true;
            labelDiscountAmountSum.Font = new Font("Segoe UI", 14F);
            labelDiscountAmountSum.Location = new Point(661, 36);
            labelDiscountAmountSum.Name = "labelDiscountAmountSum";
            labelDiscountAmountSum.Size = new Size(22, 25);
            labelDiscountAmountSum.TabIndex = 0;
            labelDiscountAmountSum.Text = "2";
            // 
            // labelDepositAmountSum
            // 
            labelDepositAmountSum.AutoSize = true;
            labelDepositAmountSum.Font = new Font("Segoe UI", 14F);
            labelDepositAmountSum.Location = new Point(661, 61);
            labelDepositAmountSum.Name = "labelDepositAmountSum";
            labelDepositAmountSum.Size = new Size(22, 25);
            labelDepositAmountSum.TabIndex = 0;
            labelDepositAmountSum.Text = "3";
            // 
            // labelTotalAmountSum
            // 
            labelTotalAmountSum.AutoSize = true;
            labelTotalAmountSum.Font = new Font("Segoe UI", 14F);
            labelTotalAmountSum.Location = new Point(660, 11);
            labelTotalAmountSum.Name = "labelTotalAmountSum";
            labelTotalAmountSum.Size = new Size(22, 25);
            labelTotalAmountSum.TabIndex = 0;
            labelTotalAmountSum.Text = "1";
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.Controls.Add(dataGridViewNotPaidInvoices);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 40);
            panel3.Name = "panel3";
            panel3.Size = new Size(1134, 560);
            panel3.TabIndex = 3;
            // 
            // NotPaidForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "NotPaidForm";
            Text = "NotPaidForm";
            Load += NotPaidForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewNotPaidInvoices).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView dataGridViewNotPaidInvoices;
        private Panel panel2;
        private Label labelTotalAmountSum;
        private Label labelDiscountAmountSum;
        private Label labelDepositAmountSum;
        private Panel panel3;
        private DateTimePicker datePickerStart;
        private Button btnFilter;
        private TextBox txtSearchTerm;
        private DateTimePicker datePickerEnd;
        private Label label3;
        private Label label2;
    }
}