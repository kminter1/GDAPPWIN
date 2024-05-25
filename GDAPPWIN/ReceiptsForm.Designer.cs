namespace GDAPPWIN
{
    partial class ReceiptsForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel3 = new Panel();
            dataGridViewListRealInvoice = new DataGridView();
            dataGridViewListReceipts = new DataGridView();
            panel1 = new Panel();
            label11 = new Label();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListRealInvoice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListReceipts).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.Controls.Add(dataGridViewListRealInvoice);
            panel3.Controls.Add(dataGridViewListReceipts);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1134, 711);
            panel3.TabIndex = 6;
            // 
            // dataGridViewListRealInvoice
            // 
            dataGridViewListRealInvoice.AllowUserToAddRows = false;
            dataGridViewListRealInvoice.AllowUserToDeleteRows = false;
            dataGridViewListRealInvoice.AllowUserToResizeRows = false;
            dataGridViewListRealInvoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewListRealInvoice.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewListRealInvoice.Dock = DockStyle.Bottom;
            dataGridViewListRealInvoice.Location = new Point(0, 511);
            dataGridViewListRealInvoice.Name = "dataGridViewListRealInvoice";
            dataGridViewListRealInvoice.RowHeadersVisible = false;
            dataGridViewListRealInvoice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewListRealInvoice.Size = new Size(1134, 200);
            dataGridViewListRealInvoice.TabIndex = 1;
            // 
            // dataGridViewListReceipts
            // 
            dataGridViewListReceipts.AllowUserToAddRows = false;
            dataGridViewListReceipts.AllowUserToDeleteRows = false;
            dataGridViewListReceipts.AllowUserToResizeRows = false;
            dataGridViewListReceipts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewListReceipts.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewListReceipts.Dock = DockStyle.Top;
            dataGridViewListReceipts.Location = new Point(0, 42);
            dataGridViewListReceipts.Name = "dataGridViewListReceipts";
            dataGridViewListReceipts.RowHeadersVisible = false;
            dataGridViewListReceipts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewListReceipts.Size = new Size(1134, 463);
            dataGridViewListReceipts.TabIndex = 0;
            dataGridViewListReceipts.CellClick += dataGridViewListReceipts_CellClick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label11);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 42);
            panel1.TabIndex = 3;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = Color.White;
            label11.Location = new Point(505, 9);
            label11.Name = "label11";
            label11.Size = new Size(145, 21);
            label11.TabIndex = 2;
            label11.Text = "ใบเสร็จรับเงิน (ORCC)";
            // 
            // ReceiptsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel3);
            Name = "ReceiptsForm";
            Text = "ReceiptsForm";
            Load += ReceiptsForm_Load;
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewListRealInvoice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListReceipts).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Label label11;
        private DataGridView dataGridViewListRealInvoice;
        private DataGridView dataGridViewListReceipts;
        private Panel panel1;
    }
}