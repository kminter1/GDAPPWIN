namespace GDAPPWIN
{
    partial class ApproveForm
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
            dataGridViewListReceipts = new DataGridView();
            label1 = new Label();
            txt_SearchReceipts = new TextBox();
            label_ReceiveDetails = new Label();
            btn_SaveApprove = new Button();
            panel1 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            label3 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            btnClear = new Button();
            btnImportORCC = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListReceipts).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewListReceipts
            // 
            dataGridViewListReceipts.AllowUserToAddRows = false;
            dataGridViewListReceipts.AllowUserToDeleteRows = false;
            dataGridViewListReceipts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewListReceipts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewListReceipts.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewListReceipts.Dock = DockStyle.Fill;
            dataGridViewListReceipts.Location = new Point(0, 299);
            dataGridViewListReceipts.Name = "dataGridViewListReceipts";
            dataGridViewListReceipts.RowHeadersVisible = false;
            dataGridViewListReceipts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewListReceipts.Size = new Size(1134, 412);
            dataGridViewListReceipts.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(873, 239);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 1;
            label1.Text = "ค้นหา";
            // 
            // txt_SearchReceipts
            // 
            txt_SearchReceipts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_SearchReceipts.Location = new Point(912, 236);
            txt_SearchReceipts.Name = "txt_SearchReceipts";
            txt_SearchReceipts.Size = new Size(219, 23);
            txt_SearchReceipts.TabIndex = 2;
            txt_SearchReceipts.TextChanged += txt_SearchReceipts_TextChanged;
            // 
            // label_ReceiveDetails
            // 
            label_ReceiveDetails.AutoSize = true;
            label_ReceiveDetails.Font = new Font("Segoe UI", 10F);
            label_ReceiveDetails.Location = new Point(18, 24);
            label_ReceiveDetails.Name = "label_ReceiveDetails";
            label_ReceiveDetails.Size = new Size(15, 19);
            label_ReceiveDetails.TabIndex = 3;
            label_ReceiveDetails.Text = "*";
            // 
            // btn_SaveApprove
            // 
            btn_SaveApprove.Font = new Font("Segoe UI", 11F);
            btn_SaveApprove.Location = new Point(912, 189);
            btn_SaveApprove.Name = "btn_SaveApprove";
            btn_SaveApprove.Size = new Size(107, 35);
            btn_SaveApprove.TabIndex = 4;
            btn_SaveApprove.Text = "อนุมัติ";
            btn_SaveApprove.UseVisualStyleBackColor = true;
            btn_SaveApprove.Click += btn_SaveApprove_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 34);
            panel1.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(550, 6);
            label2.Name = "label2";
            label2.Size = new Size(79, 21);
            label2.TabIndex = 0;
            label2.Text = "บิลรออนุมัติ";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Info;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label_ReceiveDetails);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1134, 173);
            panel2.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(49, 3);
            label3.Name = "label3";
            label3.Size = new Size(134, 21);
            label3.TabIndex = 3;
            label3.Text = "รายละเอียดการอนุมัติ";
            // 
            // panel3
            // 
            panel3.Controls.Add(dataGridViewListReceipts);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1134, 711);
            panel3.TabIndex = 7;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnClear);
            panel4.Controls.Add(btnImportORCC);
            panel4.Controls.Add(panel2);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(btn_SaveApprove);
            panel4.Controls.Add(txt_SearchReceipts);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 34);
            panel4.Name = "panel4";
            panel4.Size = new Size(1134, 265);
            panel4.TabIndex = 6;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 11F);
            btnClear.Location = new Point(1025, 189);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(102, 35);
            btnClear.TabIndex = 8;
            btnClear.Text = "ยกเลิก";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnImportORCC
            // 
            btnImportORCC.Font = new Font("Segoe UI", 11F);
            btnImportORCC.Location = new Point(807, 191);
            btnImportORCC.Name = "btnImportORCC";
            btnImportORCC.Size = new Size(99, 35);
            btnImportORCC.TabIndex = 7;
            btnImportORCC.Text = "นำเข้าข้อมูล";
            btnImportORCC.UseVisualStyleBackColor = true;
            btnImportORCC.Click += btnImportORCC_Click;
            // 
            // ApproveForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1134, 711);
            Controls.Add(panel3);
            Name = "ApproveForm";
            Text = "ApproveForm";
            Load += ApproveForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewListReceipts).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewListReceipts;
        private Label label1;
        private TextBox txt_SearchReceipts;
        private Label label_ReceiveDetails;
        private Button btn_SaveApprove;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private Label label3;
        private Panel panel3;
        private Panel panel4;
        private Button btnImportORCC;
        private Button btnClear;
    }
}