namespace GDAPPWIN
{
    partial class SummaryForm
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
            dataGridViewSummary = new DataGridView();
            txtSearchTerm = new TextBox();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSummary).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewSummary
            // 
            dataGridViewSummary.AllowUserToAddRows = false;
            dataGridViewSummary.AllowUserToDeleteRows = false;
            dataGridViewSummary.AllowUserToResizeColumns = false;
            dataGridViewSummary.AllowUserToResizeRows = false;
            dataGridViewSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewSummary.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewSummary.Dock = DockStyle.Fill;
            dataGridViewSummary.Location = new Point(0, 40);
            dataGridViewSummary.Name = "dataGridViewSummary";
            dataGridViewSummary.RowHeadersVisible = false;
            dataGridViewSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSummary.Size = new Size(1134, 671);
            dataGridViewSummary.TabIndex = 10;
            // 
            // txtSearchTerm
            // 
            txtSearchTerm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtSearchTerm.Location = new Point(915, 11);
            txtSearchTerm.Name = "txtSearchTerm";
            txtSearchTerm.Size = new Size(216, 23);
            txtSearchTerm.TabIndex = 4;
            txtSearchTerm.TextChanged += txtSearchTerm_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtSearchTerm);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 40);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(444, 9);
            label1.Name = "label1";
            label1.Size = new Size(222, 21);
            label1.TabIndex = 0;
            label1.Text = "รายงานรวม ORNC/ORVC/ORCC";
            // 
            // SummaryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 711);
            Controls.Add(dataGridViewSummary);
            Controls.Add(panel1);
            Name = "SummaryForm";
            Text = "SummaryForm";
            Load += SummaryForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewSummary).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridViewSummary;
        private TextBox txtSearchTerm;
        private Panel panel1;
        private Label label1;
    }
}