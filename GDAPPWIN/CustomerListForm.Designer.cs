namespace GDAPPWIN
{
    partial class CustomerListForm
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
            dataGridViewCustomerList = new DataGridView();
            btnOK = new Button();
            txtSearchCustomer = new TextBox();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustomerList).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewCustomerList
            // 
            dataGridViewCustomerList.AllowUserToAddRows = false;
            dataGridViewCustomerList.AllowUserToDeleteRows = false;
            dataGridViewCustomerList.AllowUserToResizeRows = false;
            dataGridViewCustomerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCustomerList.Location = new Point(12, 77);
            dataGridViewCustomerList.Name = "dataGridViewCustomerList";
            dataGridViewCustomerList.RowHeadersVisible = false;
            dataGridViewCustomerList.Size = new Size(655, 257);
            dataGridViewCustomerList.TabIndex = 0;
            dataGridViewCustomerList.CellContentClick += dataGridViewCustomerList_CellContentClick;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(592, 340);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // txtSearchCustomer
            // 
            txtSearchCustomer.Location = new Point(486, 48);
            txtSearchCustomer.Name = "txtSearchCustomer";
            txtSearchCustomer.Size = new Size(181, 23);
            txtSearchCustomer.TabIndex = 2;
            txtSearchCustomer.TextChanged += txtSearchCustomer_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CadetBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(686, 40);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(317, 12);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 0;
            label1.Text = "ค้นหาลูกค้า";
            // 
            // CustomerListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 375);
            Controls.Add(panel1);
            Controls.Add(txtSearchCustomer);
            Controls.Add(btnOK);
            Controls.Add(dataGridViewCustomerList);
            Name = "CustomerListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CustomerListForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCustomerList).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewCustomerList;
        private Button btnOK;
        private TextBox txtSearchCustomer;
        private Panel panel1;
        private Label label1;
    }
}