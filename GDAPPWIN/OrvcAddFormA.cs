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
    public partial class OrvcAddFormA : Form
    {
        private InvoiceClass invoiceData;
        private List<InvoiceClass> selectedBills = new List<InvoiceClass>();
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;
        private string customerName;

        public int LoggedInUserID => loggedInUserID;
        public string LoggedInUsername => loggedInUsername;
        public string LoggedInUserRole => loggedInUserRole;
        public string CustomerName
        {
            get => customerName;
            set => customerName = value;
        }

        public OrvcAddFormA(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            invoiceData = new InvoiceClass();
        }

        private void OrvcAddFormA_Load(object sender, EventArgs e)
        {
            LoadRealInvoices();
        }

        private void LoadRealInvoices()
        {
            DataTable dataTable = RealInvoiceDataAccess.LoadRealInvoicesWithStatus();
            if (dataTable != null)
            {
                dataGridView_InvoiceList.DataSource = dataTable;
                // เรียกใช้เมทอดเพื่อกำหนดชื่อหัวของคอลัมน์ใน DataGridView
                ColumnsHeaderClass.SetInvoicesThaiColumnHeaders(dataGridView_InvoiceList);
                dataGridView_InvoiceList.DataBindingComplete += DataGridViewRealInvoices_DataBindingComplete;
                dataGridView_InvoiceList.SetDecimalFormatInColumns(invoiceData.InvoicesDecimalFormat);
                AddCheckboxColumnIfNeeded();
                // Hide RealInvoiceID and CustomerID columns
                dataGridView_InvoiceList.Columns["RealInvoiceID"].Visible = false;
                dataGridView_InvoiceList.Columns["CustomerID"].Visible = false;
            }

        }
        private void DataGridViewRealInvoices_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView_InvoiceList.Rows)
            {
                UpdateRowColor(row);
            }
        }
        private void UpdateRowColor(DataGridViewRow row)
        {
            int realInvoiceID = Convert.ToInt32(row.Cells["RealInvoiceID"].Value);

            // Check if the invoice is voided first
            if (InvoiceClass.IsVoidedMade(realInvoiceID))
            {
                // If the invoice is voided
                row.DefaultCellStyle.BackColor = Color.LightGray; // Set color to light gray
                row.DefaultCellStyle.ForeColor = Color.Red; // Set font color to red
            }
            else if (RealInvoiceTemporaryInvoiceClass.IsPaymentMadeWithNullTemporaryInvoice(realInvoiceID))
            {
                // If the invoice is linked to a null TemporaryInvoiceID
                row.DefaultCellStyle.BackColor = Color.LightYellow; // Set color to light yellow
                row.DefaultCellStyle.ForeColor = Color.Black; // Set font color to black
            }
            else if (InvoiceClass.IsPaymentMade(realInvoiceID))
            {
                // If the invoice is paid
                row.DefaultCellStyle.BackColor = Color.Green; // Set color to green
                row.DefaultCellStyle.ForeColor = Color.White; // Set font color to white
            }
            else
            {
                // If none of the above conditions are met, default colors
                row.DefaultCellStyle.BackColor = Color.White; // Set color to white
                row.DefaultCellStyle.ForeColor = Color.Black; // Set font color to black
            }
        }


        private void btnRealReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            // Clear TextBoxes
            txtExternalBillName.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtDepositAmount.Text = string.Empty;
            txtDetails.Text = string.Empty;
            txtDiscountAmount.Text = string.Empty;
            // Clear DateTimePicker
            txtInvoiceDate.Value = DateTime.Now;
            labelCustomerID.Text = string.Empty;
            labelCustomerName.Text = string.Empty;
        }
        private void btnRealSave_Click(object sender, EventArgs e)
        {
            // Check if all required fields are filled
            if (string.IsNullOrEmpty(txtInvoiceDate.Text) || string.IsNullOrEmpty(txtExternalBillName.Text) || string.IsNullOrEmpty(txtTotalAmount.Text) || string.IsNullOrEmpty(txtDiscountAmount.Text) || string.IsNullOrEmpty(txtDepositAmount.Text) || string.IsNullOrEmpty(txtDetails.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the ExternalBillName already exists
            string externalBillName = txtExternalBillName.Text;
            if (InvoiceClass.IsExternalBillNameExists(externalBillName))
            {
                MessageBox.Show("มีเลขที่บิล(ORVC)ซ้ำกันอยู่ในระบบ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Retrieve data from TextBoxes
                DateTime invoiceDate = txtInvoiceDate.Value.Date;
                decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
                decimal depositAmount = Convert.ToDecimal(txtDepositAmount.Text);
                decimal discountAmount = Convert.ToDecimal(txtDiscountAmount.Text);
                int selectedCustomerID = Convert.ToInt32(labelCustomerID.Text);

                int realInvoiceID = 0;

                // Create a new RealInvoice object
                InvoiceClass realInvoice = new InvoiceClass
                {
                    InvoiceDate = invoiceDate,
                    TotalAmount = totalAmount,
                    DepositAmount = depositAmount,
                    ExternalBillName = externalBillName,
                    Details = txtDetails.Text,
                    CreateByUser = loggedInUserID,
                    CustomerID = selectedCustomerID,
                    DiscountAmount = discountAmount,
                    OutstandingDebt = totalAmount
                };

                // Insert the real invoice to the database and get the generated RealInvoiceID
                realInvoiceID = InvoiceClass.InsertRealInvoiceToDatabase(realInvoice);
                // สร้าง real invoice โดยไม่ระบุ temporary invoice
                if (realInvoiceID > 0)
                {
                    RealInvoiceTemporaryInvoiceClass realInvoiceTemporaryInvoice = new RealInvoiceTemporaryInvoiceClass
                    {
                        RealInvoiceID = realInvoiceID,
                        TemporaryInvoiceID = null
                    }; realInvoiceTemporaryInvoice.InsertToDatabase(); // ทำการบันทึกข้อมูล
                }
                    // If RealInvoiceID is valid, proceed to save transactions
                if (realInvoiceID > 0)
                {
                    // Create a new transaction object
                    RealInvoiceTransactionClass realInvoiceTransaction = new RealInvoiceTransactionClass
                    {
                        RealInvoiceID = realInvoiceID,
                        TransactionDate = DateTime.Now,
                        DiscountAmount = discountAmount,
                        DepositAmount = depositAmount,
                        CustomerID = selectedCustomerID,
                        CreateByUser = loggedInUserID
                        // Add other transaction details here if needed
                    };

                    // Insert the transaction to the database
                    realInvoiceTransaction.InsertToDatabase();
                }

                // Reload the real invoices and clear the form after successful saving
                LoadRealInvoices();
                ClearForm();
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSearchOrvc_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearchOrvc.Text.Trim();
            SearchOrvcInvoiceData(searchTerm);
        }

        private void SearchOrvcInvoiceData(string searchTerm)
        {
            try
            {
                DataTable searchResult = RealInvoiceDataAccess.SearchRealInvoicesWithStatus(searchTerm);
                dataGridView_InvoiceList.DataSource = searchResult;

                AddCheckboxColumnIfNeeded();
                UncheckAllCheckboxes();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void Btn_ImportOrvc_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedBills();

                if (selectedBills.Count > 0)
                {
                    // Check if the selected bill is fully paid
                    if (IsFullyPaid(selectedBills[0]))
                    {
                        // If fully paid, unselect the checkbox and inform the user
                        ShowInformationMessage("The selected bill is fully paid and cannot be edited.");
                        UncheckAllCheckboxes();
                    }
                    else
                    {
                        // If not fully paid, proceed to populate the data for editing
                        PopulateRealInvoiceData(selectedBills[0]);
                    }
                }
                else
                {
                    ShowInformationMessage("Please select a bill to edit.");
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private bool IsFullyPaid(InvoiceClass invoice)
        {
            // Use the IsPaymentMade method to check if payment has been made for the invoice
            bool paymentMade = InvoiceClass.IsPaymentMade(invoice.RealInvoiceID);

            // If payment has been made, consider the invoice as fully paid
            return paymentMade;
        }


        private void Btn_EditOrvc_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormPopulated())
                {
                    UpdateSelectedBills();
                    ClearFormAndLoadData();
                }
                else
                {
                    ShowErrorMessage("Please select a bill to update.");
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private bool IsFormPopulated()
        {
            return !string.IsNullOrEmpty(labelRealInvoiceID.Text) &&
                   !string.IsNullOrEmpty(labelCustomerID.Text) &&
                   !string.IsNullOrEmpty(txtExternalBillName.Text) &&
                   !string.IsNullOrEmpty(txtTotalAmount.Text) &&
                   !string.IsNullOrEmpty(txtDetails.Text) &&
                   !string.IsNullOrEmpty(txtDepositAmount.Text) &&
                   !string.IsNullOrEmpty(txtDiscountAmount.Text) &&
                   txtInvoiceDate.Value != DateTime.MinValue;
        }

        private void AddCheckboxColumnIfNeeded()
        {
            if (dataGridView_InvoiceList.Columns["CheckboxOrnc"] == null)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "เลือก";
                checkBoxColumn.Name = "CheckboxOrnc";
                dataGridView_InvoiceList.Columns.Insert(0, checkBoxColumn);
            }
        }

        private void UncheckAllCheckboxes()
        {
            foreach (DataGridViewRow row in dataGridView_InvoiceList.Rows)
            {
                row.Cells["CheckboxOrnc"].Value = false;
            }
        }

        private void GetSelectedBills()
        {
            selectedBills.Clear();

            foreach (DataGridViewRow row in dataGridView_InvoiceList.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckboxOrnc"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                {
                    selectedBills.Add(CreateInvoiceFromRow(row));
                }
            }
        }

        private void UpdateSelectedBills()
        {
            foreach (InvoiceClass bill in selectedBills)
            {
                UpdateInvoiceInDatabase(bill);
            }
        }

        private void UpdateInvoiceInDatabase(InvoiceClass invoice)
        {
            // Update the properties of the invoice object
            invoice.RealInvoiceID = Convert.ToInt32(labelRealInvoiceID.Text);
            invoice.InvoiceDate = txtInvoiceDate.Value;
            invoice.Details = txtDetails.Text;
            invoice.ExternalBillName = txtExternalBillName.Text;
            invoice.TotalAmount = decimal.Parse(txtTotalAmount.Text);
            invoice.DiscountAmount = decimal.Parse(txtDiscountAmount.Text);
            invoice.DepositAmount = decimal.Parse(txtDepositAmount.Text);

            try
            {
                // Call the method to update the invoice in the database
                bool success = RealInvoiceDataAccess.UpdateRealInvoice(invoice);
                if (success)
                {
                    MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateRealInvoiceData(InvoiceClass invoice)
        {
            txtExternalBillName.Text = invoice.ExternalBillName;
            txtTotalAmount.Text = invoice.TotalAmount.ToString();
            txtDiscountAmount.Text = invoice.DiscountAmount.ToString();
            txtDepositAmount.Text = invoice.DepositAmount.ToString();
            txtInvoiceDate.Value = invoice.InvoiceDate;
            txtDetails.Text = invoice.Details;
            labelRealInvoiceID.Text = invoice.RealInvoiceID.ToString();
            labelCustomerID.Text = invoice.CustomerID.ToString();
            labelCustomerName.Text = invoice.CustomerName.ToString();
            // Adjust the code to populate other fields as needed
        }

        private InvoiceClass CreateInvoiceFromRow(DataGridViewRow row)
        {
            return new InvoiceClass
            {
                RealInvoiceID = Convert.ToInt32(row.Cells["RealInvoiceID"].Value),
                CustomerID = Convert.ToInt32(row.Cells["CustomerID"].Value),
                CustomerName = row.Cells["CustomerName"].Value?.ToString(),
                InvoiceDate = Convert.ToDateTime(row.Cells["InvoiceDate"].Value),
                TotalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value),
                DepositAmount = Convert.ToDecimal(row.Cells["DepositAmount"].Value),
                Details = row.Cells["Details"].Value?.ToString(),
                ExternalBillName = row.Cells["ExternalBillName"].Value?.ToString(),
                DiscountAmount = Convert.ToDecimal(row.Cells["DiscountAmount"].Value)
            };
        }

        private void dataGridView_InvoiceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView_InvoiceList.Columns["CheckboxOrnc"].Index)
            {
                ToggleCheckboxOrvcSelection(e.RowIndex);
            }
        }

        private void ToggleCheckboxOrvcSelection(int clickedRowIndex)
        {
            DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)dataGridView_InvoiceList.Rows[clickedRowIndex].Cells["CheckboxOrnc"];

            // Uncheck all checkboxes except the clicked one
            foreach (DataGridViewRow row in dataGridView_InvoiceList.Rows)
            {
                if (row.Index != clickedRowIndex)
                {
                    DataGridViewCheckBoxCell otherChkCell = (DataGridViewCheckBoxCell)row.Cells["CheckboxOrnc"];
                    otherChkCell.Value = false;
                }
            }

            // Toggle the clicked checkbox
            bool currentValue = Convert.ToBoolean(chkCell.Value);
            chkCell.Value = !currentValue;
        }

        private void ClearFormAndLoadData()
        {
            ClearForm();
            LoadRealInvoices();
        }
        private void HandleError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInformationMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnVoidOrvc_Click(object sender, EventArgs e)
        {
            int invoiceID = GetSelectedInvoiceID(); // Get the ID of the selected invoice
            if (invoiceID != -1)
            {
                InvoiceClass invoiceClassx = new InvoiceClass();
                invoiceClassx.VoidInvoice(invoiceID);
                // Optionally, you can update your DataGridView to reflect the changes
                // Show a success message
                MessageBox.Show("ยกเลิกบิลแล้ว.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRealInvoices();
            }
            else
            {
                MessageBox.Show("เลือกบิลที่ต้องการยกเลืก.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to get the ID of the selected invoice from your DataGridView
        private int GetSelectedInvoiceID()
        {
            int invoiceID = -1; // Default value if no invoice is selected

            // Get the current cell
            DataGridViewCell currentCell = dataGridView_InvoiceList.CurrentCell;

            if (currentCell != null)
            {
                // Get the row index of the current cell
                int rowIndex = currentCell.RowIndex;

                // Check if the checkbox is checked in the current row
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView_InvoiceList.Rows[rowIndex].Cells["CheckboxOrnc"];
                if (checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    // Get the RealInvoiceID if the checkbox is checked
                    if (dataGridView_InvoiceList.Rows[rowIndex].Cells["RealInvoiceID"].Value != null)
                    {
                        invoiceID = Convert.ToInt32(dataGridView_InvoiceList.Rows[rowIndex].Cells["RealInvoiceID"].Value);
                    }
                }
            }
            return invoiceID;
        }

        private void btn_AddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new instance of CustomerListForm and pass the customer data
                CustomerListForm customerListForm = new CustomerListForm(CustomerHelper.GetCustomerList());

                // Show the CustomerListForm as a dialog
                DialogResult result = customerListForm.ShowDialog();

                // Check if the user clicked OK
                if (result == DialogResult.OK)
                {
                    // Retrieve the selected customers from the CustomerListForm
                    List<CustomerDataAccess> selectedCustomers = customerListForm.SelectedCustomers;

                    // Display selected customer information
                    if (selectedCustomers.Count > 0)
                    {
                        // Update the labels or fields with the selected customer's data
                        labelCustomerID.Text = selectedCustomers[0].CustomerID;
                        labelCustomerName.Text = selectedCustomers[0].CustomerName;
                    }
                    else
                    {
                        // If no customer is selected, clear the labels
                        labelCustomerID.Text = "";
                        labelCustomerName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
