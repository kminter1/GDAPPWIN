using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GDAPPWIN
{
    public partial class CashInForm : Form
    {
        InvoiceClass invoiceData;
        ReceiptsClass receiptsData;
        public int LoggedInUserID
        {
            get { return loggedInUserID; }
        }

        public string LoggedInUsername
        {
            get { return loggedInUsername; }
        }

        public string LoggedInUserRole
        {
            get { return loggedInUserRole; }
        }
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;

        public CashInForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            invoiceData = new InvoiceClass();
            receiptsData = new ReceiptsClass();
        }

        private void CashInForm_Load(object sender, EventArgs e)
        {
            LoadAllRealInvoiceData();
            comboBoxPaymentMethod.DataSource = new List<string> { "Transfer", "Cheque", "Cash" };
        }

        private string GetSelectedPaymentMethod()
        {
            return comboBoxPaymentMethod.SelectedItem.ToString();
        }

        private void LoadAllRealInvoiceData()
        {
            DataTable realInvoiceData = RealInvoiceDataAccess.LoadRealInvoicesWithStatus();
            dataGridViewRealInvoices.DataSource = realInvoiceData;

            // Add checkbox column if not exists
            dataGridViewRealInvoices.AddCheckboxColumnIfNeeded();

            // Set DataGridView properties after adding the column
            dataGridViewRealInvoices.SetDataGridViewProperties();

            // Clear selection
            dataGridViewRealInvoices.ClearSelection();
            dataGridViewRealInvoices.DataBindingComplete += DataGridViewRealInvoices_DataBindingComplete;
            dataGridViewRealInvoices.Columns["RealInvoiceID"].Visible = false;
            dataGridViewRealInvoices.Columns["CustomerID"].Visible = false;
        }

        private void DataGridViewRealInvoices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRealInvoices.Rows)
            {
                row.UpdateRowColor();
            }
        }

        private void txtSearchRealInvoice_TextChanged(object sender, EventArgs e)
        {
            SearchRealInvoices(txtSearchRealInvoice.Text);
        }

        private void SearchRealInvoices(string searchTerm)
        {
            dataGridViewRealInvoices.DataSource = RealInvoiceDataAccess.SearchRealInvoicesWithStatus(searchTerm);

            // Add checkbox column if not exists
            dataGridViewRealInvoices.AddCheckboxColumnIfNeeded();

            // Set decimal format in specified columns
            dataGridViewRealInvoices.SetDecimalFormatInColumns(invoiceData.InvoicesDecimalFormat);

            // Set column headers from dictionary
            dataGridViewRealInvoices.SetColumnHeadersFromDictionary(invoiceData.InvoicesHeaderFormat);
        }

        private void btnSaveReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedPaymentMethod = GetSelectedPaymentMethod();

                int selectedCustomerID = GetCustomerIDFromSelectedRows(); // ดึงค่า CustomerID ของแถวแรกที่มี CheckboxColumn ถูกเลือก

                if (selectedCustomerID != -1 && ValidateInputs())
                {
                    // Get data from controls
                    string receiptNumber = txtReceiptNumber.Text;

                    // Check if the receipt number already exists in the receipts table
                    if (ReceiptDataAccess.IsReceiptNumberExists(receiptNumber))
                    {
                        MessageBox.Show("มีเลขที่ใบเสร็จ(ORCC)นี้แล้ว.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    decimal netCash = Convert.ToDecimal(txtNetCash.Text);
                    decimal withHoldingTax = Convert.ToDecimal(txtWithHoldingTax.Text);
                    decimal discountAmount = Convert.ToDecimal(labelDiscountToPay.Text);
                    decimal depositAmount = Convert.ToDecimal(labelDepositToPay.Text);
                    decimal feeAmount = Convert.ToDecimal(txtFee.Text);
                    DateTime receiptDate = dateTimePickerReceiptDate.Value;
                    string paymentMethod = selectedPaymentMethod;
                    string remark = richTextBoxRemark.Text;
                    int createByUser = loggedInUserID;
                    decimal getMoney = Convert.ToDecimal(labelGetMoney.Text);
                    ReceiptsClass receiptInsertData = new ReceiptsClass
                    {
                        CustomerID = selectedCustomerID,
                        ReceiptNumber = receiptNumber,
                        NetCash = netCash,
                        WithHoldingTax = withHoldingTax,
                        DiscountAmount = discountAmount,
                        DepositAmount = depositAmount,
                        Fee = feeAmount,
                        ReceiptDate = receiptDate,
                        PaymentMethod = paymentMethod,
                        Remark = remark,
                        CreatedByUser = createByUser
                    };
                     
                    // Add receipt and get the last inserted ID
                    int receiptID = ReceiptsClass.AddReceiptAndGetLastID(receiptInsertData);

                    if (receiptID != -1)
                    {
                        // Add selected real invoices to the receipt
                        foreach (DataGridViewRow selectedRow in dataGridViewRealInvoices.Rows)
                        {
                            DataGridViewCheckBoxCell checkBoxCell = selectedRow.Cells["CheckboxColumn"] as DataGridViewCheckBoxCell;
                            if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                            {
                                int realInvoiceID = Convert.ToInt32(selectedRow.Cells["RealInvoiceID"].Value);
                                bool successReceiptsRealInvoices = ReceiptsClass.AddReceiptsRealInvoices(receiptID, realInvoiceID);

                                if (!successReceiptsRealInvoices)
                                {
                                    MessageBox.Show("An error occurred while saving data in receipts_realinvoices table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                        // Clear controls and reload data
                        ClearControls();
                        LoadAllRealInvoiceData();
                        ClearCheckboxSelection(dataGridViewRealInvoices);
                        MessageBox.Show("Receipt data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while saving receipt data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to import data or fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Validate required inputs
            if (string.IsNullOrEmpty(txtReceiptNumber.Text) ||
                string.IsNullOrEmpty(txtNetCash.Text) ||
                string.IsNullOrEmpty(txtWithHoldingTax.Text) ||
                comboBoxPaymentMethod.SelectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void ClearControls()
        {
            // Clear all controls
            txtReceiptNumber.Clear();
            txtNetCash.Clear();
            txtWithHoldingTax.Clear();
            txtFee.Clear();
            labelDiscountAmount.Text = "";
            labelDepositAmount.Text = "";
            label_Invdetails.Text = string.Empty;
            label_CustomerID.Text = string.Empty;
            label_CustomerName.Text = string.Empty;
            labelTotalAmount.Text = string.Empty;
            comboBoxPaymentMethod.SelectedIndex = -1;
            richTextBoxRemark.Clear();

        }

        private void btnImportORVC_Click(object sender, EventArgs e)
        {
            bool anySelected = dataGridViewRealInvoices.Rows.Cast<DataGridViewRow>()
                        .Any(row => IsRowSelected(row));
            if (dataGridViewRealInvoices.SelectedRows.Count == 0 || !IsRowSelected(dataGridViewRealInvoices.SelectedRows[0]))
            {
                MessageBox.Show("Please select a row with the checkbox checked to update0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal totalAmountPaid = 0; // Reset total amount paid
            decimal totalAmount = 0;
            decimal totalDiscountAmount = 0;
            decimal totalDepositAmount = 0;
            string combinedExternalBillNames = "";

            // Get selected customer ID
            int selectedCustomerID = GetCustomerIDFromSelectedRows();
            string selectedCustomerName = GetCustomerNameFromSelectedRows();

            foreach (DataGridViewRow row in dataGridViewRealInvoices.Rows)
            {
                if (IsRowSelected(row))
                {
                    int realInvoiceID = GetRealInvoiceID(row);

                    Console.WriteLine(realInvoiceID);

                    // Check if the real invoice can be paid again
                    (bool canPayAgain, decimal totalAmountPaidInLoop) = ReceiptDataAccess.CanPayRealInvoiceAgain(realInvoiceID);

                    // Display a message if the real invoice cannot be paid again
                    if (canPayAgain)
                    {
                        MessageBox.Show("ทำเรื่องเบิกเงินครบแล้ว");
                        ClearControls();
                        return;
                    }

                    // Accumulate total amount paid
                    totalAmountPaid += totalAmountPaidInLoop;

                    // Update total amounts
                    UpdateTotalAmounts(row, ref totalAmount, ref totalDiscountAmount, ref totalDepositAmount);

                    // Get and append external bill name
                    combinedExternalBillNames += GetExternalBillName(row) + ", ";
                }
            }

            // Update UI elements
            UpdateUI(selectedCustomerID, selectedCustomerName, totalAmount, totalDiscountAmount, totalDepositAmount, combinedExternalBillNames, totalAmountPaid);
            // Show message box with details
            ShowMessageBox(totalAmount, totalDiscountAmount, totalDepositAmount, combinedExternalBillNames);
        }


        private void UpdateTotalAmountPaid(decimal amountPaid)
        {
            labeltotalAmountPaid.Text = amountPaid.ToString(); // Assuming labeltotalAmountPaid is the name of your label
        }

        private string GetCustomerNameFromSelectedRows()
        {
            foreach (DataGridViewRow row in dataGridViewRealInvoices.Rows)
            {
                if (IsRowSelected(row))
                {
                    return row.Cells["CustomerName"].Value.ToString();
                }
            }
            return ""; // Return an empty string if no row is selected
        }

        private bool IsRowSelected(DataGridViewRow row)
        {
            DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckboxColumn"] as DataGridViewCheckBoxCell;
            return checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value);
        }

        private int GetRealInvoiceID(DataGridViewRow row)
        {
            return Convert.ToInt32(row.Cells["RealInvoiceID"].Value);
        }

        private void UpdateTotalAmounts(DataGridViewRow row, ref decimal totalAmount, ref decimal totalDiscountAmount, ref decimal totalDepositAmount)
        {
            totalAmount += Convert.ToDecimal(row.Cells["TotalAmount"].Value);
            totalDiscountAmount += Convert.ToDecimal(row.Cells["DiscountAmount"].Value);
            totalDepositAmount += Convert.ToDecimal(row.Cells["DepositAmount"].Value);
        }

        private string GetExternalBillName(DataGridViewRow row)
        {
            return row.Cells["ExternalBillName"].Value.ToString();
        }

        private void UpdateUI(int selectedCustomerID, string selectedCustomerName, decimal totalAmount, decimal totalDiscountAmount, decimal totalDepositAmount, string combinedExternalBillNames, decimal totalAmountPaid)
        {
            label_CustomerID.Text = selectedCustomerID.ToString();
            label_CustomerName.Text = selectedCustomerName;
            labelDiscountAmount.Text = totalDiscountAmount.ToString();
            labelDepositAmount.Text = totalDepositAmount.ToString();
            richTextBoxRemark.Text = combinedExternalBillNames.TrimEnd(',', ' ');
            label_Invdetails.Text = combinedExternalBillNames;
            labelTotalAmount.Text = totalAmount.ToString();
            UpdateTotalAmountPaid(totalAmountPaid);
        }

        private void ShowMessageBox(decimal totalAmount, decimal totalDiscountAmount, decimal totalDepositAmount, string combinedExternalBillNames)
        {
            MessageBox.Show($"Total Amount: {totalAmount}, Total Discount Amount: {totalDiscountAmount}, Total Deposit Amount: {totalDepositAmount}, Combined External Bill Names: {combinedExternalBillNames}");
        }

        private int GetCustomerIDFromSelectedRows()
        {
            foreach (DataGridViewRow row in dataGridViewRealInvoices.Rows)
            {
                if (IsRowSelected(row))
                {
                    return Convert.ToInt32(row.Cells["CustomerID"].Value);
                }
            }
            return -1; // Return -1 if no row is selected
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            ClearControls();
            ClearCheckboxSelection(dataGridViewRealInvoices);
        }

        private void ClearCheckboxSelection(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckboxColumn"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = false;
                }
            }
        }

        private void CalculateAndSetAmounts()
        {
            // Parse values from textboxes
            decimal netCash = 0;
            decimal withHoldingTax = 0;
            decimal fee = 0;
            // Parse label values
            decimal discountAmountFromLabel = 0;
            decimal depositAmountFromLabel = 0;

            decimal.TryParse(txtNetCash.Text.Trim(), out netCash);
            decimal.TryParse(txtWithHoldingTax.Text.Trim(), out withHoldingTax);
            decimal.TryParse(txtFee.Text.Trim(), out fee);

            // Calculate total amount
            decimal totalAmount = netCash + withHoldingTax + fee;
            labelGetMoney.Text = totalAmount.ToString("N2");

            decimal.TryParse(labelDiscountAmount.Text.Trim(), out discountAmountFromLabel);
            decimal.TryParse(labelDepositAmount.Text.Trim(), out depositAmountFromLabel);

            // Calculate payment percentage
            decimal paymentPercentage = 0;
            if (totalAmount != 0)
            {
                decimal.TryParse(labelTotalAmount.Text.Trim(), out decimal labelTotal);
                paymentPercentage = totalAmount / labelTotal;
            }

            // Calculate discount amount based on payment percentage
            decimal discountAmount = discountAmountFromLabel * paymentPercentage;

            // Calculate deposit amount based on payment percentage
            decimal depositAmount = depositAmountFromLabel * paymentPercentage;

            // Update label text with calculated amounts
            labelDiscountToPay.Text = discountAmount.ToString("N2");
            labelDepositToPay.Text = depositAmount.ToString("N2");
        }

        private void txtNetCash_TextChanged(object sender, EventArgs e)
        {
            CalculateAndSetAmounts();
        }

        private void txtWithHoldingTax_TextChanged(object sender, EventArgs e)
        {
            CalculateAndSetAmounts();
        }

        private void txtFee_TextChanged(object sender, EventArgs e)
        {
            CalculateAndSetAmounts();
        }
    }
}