using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace GDAPPWIN
{
    public partial class ApproveForm : Form
    {
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
        public ApproveForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
        }

        private void ApproveForm_Load(object sender, EventArgs e)
        {
            LoadReceiptsData();
        }
        private void LoadReceiptsData()
        {
            try
            {
                // Call the method from ReceiptsClass to load data
                DataTable receiptsData = ReceiptDataAccess.GetUnapprovedReceipts();

                // Bind the DataTable to the DataGridView
                dataGridViewListReceipts.DataSource = receiptsData;

                // Add checkbox column if not exists
                dataGridViewListReceipts.AddCheckboxColumnIfNeeded();

                ReceiptDataAccess.SetDataThaiColumnHeaders(dataGridViewListReceipts);
                // Format decimal numbers in DataGridView columns
                dataGridViewListReceipts.SetDecimalFormatInColumns(ReceiptDataAccess.ReceiptDecimalFormat);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void txt_SearchReceipts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txt_SearchReceipts.Text;
                LoadReceiptsDataByTerm(searchTerm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoadReceiptsDataByTerm(string searchTerm)
        {
            try
            {
                DataTable receiptsData = ReceiptDataAccess.LoadUnapprovedReceiptsData(searchTerm);
                dataGridViewListReceipts.DataSource = receiptsData;
                // Create an instance of ReceiptDataAccess
                ReceiptDataAccess receiptDataAccess = new ReceiptDataAccess();

                // Set column headers using ReceiptDataAccess instance
                ReceiptDataAccess.SetDataThaiColumnHeaders(dataGridViewListReceipts);

                // Add checkbox column using DataGridViewExtensions
                dataGridViewListReceipts.AddCheckboxColumnIfNeeded();

                // Format decimal numbers in DataGridView columns
                dataGridViewListReceipts.SetDecimalFormatInColumns(new List<string> { "TotalAmount", "DiscountAmount", "DepositAmount", "DepositPayment" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private bool dataImported = false;
        private void btnImportORCC_Click(object sender, EventArgs e)
        {
            try
            {
                bool atLeastOneChecked = false;
                StringBuilder details = new StringBuilder();
                decimal totalDiscountAmount = 0;
                decimal totalDepositAmount = 0;
                decimal totalOutstandingDebt = 0;
                decimal totalTotalAmount = 0;

                // Check if any row has the CheckBox checked
                foreach (DataGridViewRow row in dataGridViewListReceipts.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckBoxColumn"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                    {
                        atLeastOneChecked = true; // Set flag indicating at least one CheckBox is checked

                        // Get the ReceiptID from the checked row
                        int receiptID = Convert.ToInt32(row.Cells["ReceiptID"].Value);

                        // Call the method from ReceiptsClass to calculate the values in the database
                        ReceiptsClass.CalculateValuesInDatabase(receiptID, out decimal netCash, out decimal withHoldingTax, out decimal fee, out decimal discountAmount, out decimal depositAmount);

                        // Calculate total DiscountAmount and total DepositAmount
                        totalDiscountAmount += discountAmount;
                        totalDepositAmount += depositAmount;

                        // Append details of the checked row
                        details.AppendLine($"ID: {receiptID}, จำนวนเงินรับจริง: {netCash}, หัก ณ ที่จ่าย: {withHoldingTax}, ค่าบริการ: {fee}, ส่วนลด: {discountAmount}, จำนวนเงินฝาก: {depositAmount}");

                        // Fetch OutstandingDebt and TotalAmount from realinvoices table
                        (decimal outstandingDebt, decimal totalAmount) = ReceiptsClass.GetOutstandingDebtAndTotalAmount(receiptID);
                        totalOutstandingDebt += outstandingDebt;
                        totalTotalAmount += totalAmount;
                    }
                }

                if (atLeastOneChecked)
                {
                    // Calculate discounted total deposit amount
                    decimal discountedTotalDepositAmount = totalDepositAmount * 0.8m;
                    // Display the total DiscountAmount, total DepositAmount, total OutstandingDebt, and total TotalAmount
                    label_ReceiveDetails.Text = $"รายละเอียด: \r\n{details}, \r\n จำนวนเงิน: {totalTotalAmount}, เงินส่วนลดทั้งหมด: {totalDiscountAmount}, เงินฝากทั้งหมด: {totalDepositAmount}, จ่ายจริง(20%): {discountedTotalDepositAmount}, หนี้ค้างจ่าย: {totalOutstandingDebt}";

                    MessageBox.Show("Data imported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataImported = true;
                }
                else
                {
                    // If no row has the CheckBox checked, show a message
                    MessageBox.Show("Please select a row with the CheckBox checked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_SaveApprove_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if data has been imported before allowing approval
                if (!dataImported)
                {
                    MessageBox.Show("Please import data by clicking the 'Import' button first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Check if any row has the CheckBox checked
                bool atLeastOneChecked = false;
                foreach (DataGridViewRow row in dataGridViewListReceipts.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckBoxColumn"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                    {
                        atLeastOneChecked = true;
                        break; // Exit the loop if at least one CheckBox is checked
                    }
                }

                if (!atLeastOneChecked)
                {
                    MessageBox.Show("Please select a row with the CheckBox checked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit the method if no CheckBox is checked
                }

                // If at least one CheckBox is checked, proceed to save data
                foreach (DataGridViewRow row in dataGridViewListReceipts.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckBoxColumn"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                    {
                        // Get the values from the row
                        int receiptID = Convert.ToInt32(row.Cells["ReceiptID"].Value);
                        decimal discountAmount = row.Cells["DiscountAmount"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["DiscountAmount"].Value) : 0;
                        decimal depositAmount = row.Cells["DepositAmount"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["DepositAmount"].Value) : 0;
                        int customerID = Convert.ToInt32(row.Cells["CustomerID"].Value);
                        string disdepName = row.Cells["ReceiptNumber"].Value.ToString();
                        int createByUser = loggedInUserID;

                        // Check if DiscountAmount is greater than 0
                        if (discountAmount > 0)
                        {
                            // Get data from controls

                            // Add discount to discounts table and link it to the receipt
                            int discountID = DiscountDataAccess.AddDiscount(customerID, disdepName, discountAmount, DateTime.Now.Date, createByUser);
                            bool linkSuccess = DiscountReceiptDataAccess.AddDiscountReceiptLink(discountID, receiptID, discountAmount, DateTime.Now);
                            bool updateStatus = ReceiptDataAccess.UpdateReceiptStatus(receiptID);

                            if (!linkSuccess || !updateStatus)
                            {
                                MessageBox.Show("Failed to link discount to receipt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Check if DepositAmount is greater than 0
                        if (depositAmount > 0)
                        {
                            // Save data to deposits table and deposits_receipts table
                            // Your code to save data to deposits table and deposits_receipts table
                            int depositID = DepositDataAccess.AddDeposit(customerID, disdepName, depositAmount, DateTime.Now.Date, createByUser);
                            bool linkSuccess = DepositReceiptDataAccess.AddDepositReceiptLink(depositID, receiptID, depositAmount, DateTime.Now);
                            bool updateStatus = ReceiptDataAccess.UpdateReceiptStatus(receiptID);
                            if (!linkSuccess || !updateStatus)
                            {
                                MessageBox.Show("Failed to link discount to receipt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Update the outstanding debt after updating the receipt status
                        bool updateDebtSuccess = RealInvoiceDataAccess.UpdateOutstandingDebt(receiptID);
                        if (!updateDebtSuccess)
                        {
                            MessageBox.Show("Failed to update outstanding debt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReceiptsData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            // Clear label_ReceiveDetails
            label_ReceiveDetails.Text = "";

            // Clear CheckBox in DataGridViewListReceipts
            foreach (DataGridViewRow row in dataGridViewListReceipts.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckBoxColumn"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = false;
                }
            }
        }


    }
}
