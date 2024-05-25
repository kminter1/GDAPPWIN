using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public partial class OrccForm : Form
    {
        InvoiceClass invoiceData;
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;

        public OrccForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            invoiceData = new InvoiceClass();
            
        }

        private string GetSelectedPaymentMethod()
        {
            string selectedMethod = comboBoxPaymentMethod.SelectedItem as string;
            if (selectedMethod != null)
            {
                return selectedMethod;
            }
            else
            {
                MessageBox.Show("Please select an item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        private void OrccForm_Load(object sender, EventArgs e)
        {
            // Load receipts data and add checkbox column
            LoadReceiptsData();
            comboBoxPaymentMethod.DataSource = new List<string> { "Transfer", "Cheque", "Cash" };
            // Subscribe to the TextChanged event of the txtSearchOrcc textbox
            txtSearchOrcc.TextChanged += txtSearchOrcc_TextChanged;
            // Add event handler for cell content click to toggle checkbox
            dataGridView_Orcc.CellContentClick += DataGridView_OrccList_CellContentClick;

            // Subscribe to the CellClick event
            dataGridView_Orcc.CellClick += dataGridView_OrccList_CellClick;
            BtnEditOrcc.Click += BtnEditOrcc_Click;
            BtnVoidOrcc.Click += BtnVoidOrcc_Click;
        }

        private void LoadReceiptsData()
        {
            try
            {
                // Retrieve receipts data from the database
                DataTable receiptsData = ReceiptDataAccess.GetAllReceipts();

                // Add checkbox column if needed
                AddCheckboxColumnIfNeeded();

                // Display data in DataGridView
                dataGridView_Orcc.DataSource = receiptsData;
                // Optionally, format the columns or set column headers here
                dataGridView_Orcc.DataBindingComplete += XDataGridView_OrccList_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void XDataGridView_OrccList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView_Orcc.Rows)
            {
                row.UpdateRowReceipt();
            }
        }
        private void DataGridView_OrccList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView_Orcc.Rows)
            {
                row.UpdateRowReceipt();
            }
        }

        private void AddCheckboxColumnIfNeeded()
        {
            // Check if the checkbox column already exists
            if (!dataGridView_Orcc.Columns.Contains("CheckboxColumnOrcc"))
            {
                // If the checkbox column doesn't exist, add it
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "เลือก";
                checkBoxColumn.Name = "CheckboxColumnOrcc";
                dataGridView_Orcc.Columns.Insert(0, checkBoxColumn);
            }
        }

        private void ToggleCheckboxSelection(int clickedRowIndex)
        {
            // Uncheck all checkboxes except the clicked one
            foreach (DataGridViewRow row in dataGridView_Orcc.Rows)
            {
                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)row.Cells["CheckboxColumnOrcc"];
                chkCell.Value = row.Index == clickedRowIndex;
            }
        }

        private void DataGridView_OrccList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView_Orcc.Columns["CheckboxColumnOrcc"].Index)
            {
                ToggleCheckboxSelection(e.RowIndex);
            }
        }

        private void SearchReceiptsData(string searchText)
        {
            try
            {
                // Retrieve filtered receipts data from the database
                DataTable receiptsData = ReceiptDataAccess.SearchReceiptsData(searchText);

                // Add checkbox column if needed
                AddCheckboxColumnIfNeeded();

                // Display filtered data in DataGridView
                dataGridView_Orcc.DataSource = receiptsData;

                // Optionally, format the columns or set column headers here
                dataGridView_Orcc.DataBindingComplete += DataGridView_OrccList_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtSearchOrcc_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchOrcc.Text.Trim();
            SearchReceiptsData(searchText);
        }
        private void dataGridView_OrccList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Check if the clicked cell is in the checkbox column
                if (e.ColumnIndex == dataGridView_Orcc.Columns["CheckboxColumnOrcc"].Index)
                {
                    // Toggle the checkbox selection
                    ToggleCheckboxSelection(e.RowIndex);

                    // Get data from the selected row and populate the textboxes
                    DataGridViewRow selectedRow = dataGridView_Orcc.Rows[e.RowIndex];
                    PopulateTextBoxes(selectedRow);
                }
            }
        }
        private void PopulateTextBoxes(DataGridViewRow row)
        {
            txtReceiptNumber.Text = row.Cells["ReceiptNumber"].Value?.ToString();
            txtNetCash.Text = row.Cells["NetCash"].Value?.ToString();
            txtWithHoldingTax.Text = row.Cells["WithHoldingTax"].Value?.ToString();
            txtFee.Text = row.Cells["Fee"].Value?.ToString();
            // Set values for other textboxes similarly
            dateTimePickerReceiptDate.Value = (DateTime)row.Cells["ReceiptDate"].Value;
            comboBoxPaymentMethod.SelectedItem = row.Cells["PaymentMethod"].Value.ToString();
            richTextBoxRemark.Text = row.Cells["Remark"].Value.ToString();
        }
        private void BtnEditOrcc_Click(object sender, EventArgs e)
        {
            UpdateReceiptData();
        }
        private void UpdateReceiptData()
        {
            try
            {
                bool anySelected = dataGridView_Orcc.Rows.Cast<DataGridViewRow>()
                        .Any(row => IsCheckboxChecked(row));
                if (dataGridView_Orcc.SelectedRows.Count == 0 || !IsCheckboxChecked(dataGridView_Orcc.SelectedRows[0]))
                {
                    MessageBox.Show("Please select a row with the checkbox checked to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the selected row
                DataGridViewRow selectedRow = dataGridView_Orcc.SelectedRows[0];

                // Extract the values from the textboxes
                string receiptNumber = txtReceiptNumber.Text.Trim();
                decimal netCash;
                if (!decimal.TryParse(txtNetCash.Text.Trim(), out netCash))
                {
                    MessageBox.Show("Please enter a valid net cash amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal withholdingTax;
                if (!decimal.TryParse(txtWithHoldingTax.Text.Trim(), out withholdingTax))
                {
                    MessageBox.Show("Please enter a valid withholding tax amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal fee;
                if (!decimal.TryParse(txtFee.Text.Trim(), out fee))
                {
                    MessageBox.Show("Please enter a valid fee amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime receiptDate = dateTimePickerReceiptDate.Value;

                string paymentMethod = GetSelectedPaymentMethod();
                if (string.IsNullOrEmpty(paymentMethod))
                {
                    MessageBox.Show("Please select a payment method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string remark = richTextBoxRemark.Text.Trim();

                // Update the values of the selected row in the DataGridView
                selectedRow.Cells["ReceiptNumber"].Value = receiptNumber;
                selectedRow.Cells["NetCash"].Value = netCash;
                selectedRow.Cells["WithHoldingTax"].Value = withholdingTax;
                selectedRow.Cells["Fee"].Value = fee;
                selectedRow.Cells["ReceiptDate"].Value = receiptDate;
                selectedRow.Cells["PaymentMethod"].Value = paymentMethod;
                selectedRow.Cells["Remark"].Value = remark;

                // Update the corresponding record in the database
                int receiptID = int.Parse(selectedRow.Cells["ReceiptID"].Value.ToString());
                ReceiptDataAccess.UpdateReceipt(receiptID, receiptNumber, netCash, withholdingTax, fee, receiptDate, paymentMethod, remark);

                MessageBox.Show("Receipt data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls(this);
                LoadReceiptsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsCheckboxChecked(DataGridViewRow row)
        {
            // Get the checkbox cell value
            DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckboxColumnOrcc"] as DataGridViewCheckBoxCell;
            return checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value);
        }
        private void ClearControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = -1;
                }
                else if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).Value = DateTime.Now;
                }
                else if (c is RichTextBox)
                {
                    ((RichTextBox)c).Clear();
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c is RadioButton)
                {
                    ((RadioButton)c).Checked = false;
                }
                else if (c.Controls.Count > 0)
                {
                    ClearControls(c);
                }
            }
        }

        private void BtnVoidOrcc_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any checkboxes are selected
                bool anySelected = dataGridView_Orcc.Rows.Cast<DataGridViewRow>()
                                        .Any(row => IsCheckboxChecked(row));

                if (!anySelected)
                {
                    MessageBox.Show("Please select a receipt to void.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the receipt ID of the selected receipt
                int receiptID = Convert.ToInt32(dataGridView_Orcc.SelectedRows[0].Cells["ReceiptID"].Value);

                // Update the IsVoided status of the receipt
                ReceiptDataAccess.UpdateReceiptIsVoided(receiptID);

                MessageBox.Show("Receipt voided successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the data in the DataGridView
                LoadReceiptsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
