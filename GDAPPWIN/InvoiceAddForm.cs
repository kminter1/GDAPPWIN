using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Linq;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public partial class InvoiceAddForm : Form
    {
        private decimal originalDepositAmount;
        private TemporaryDataAccess tempoData;
        private InvoiceClass invoiceData;
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
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;
        private string customerName;
        public InvoiceAddForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            tempoData = new TemporaryDataAccess();
            invoiceData = new InvoiceClass();
        }

        private void InvoiceAddForm_Load(object sender, EventArgs e)
        {
            LoadTemporaryInvoiceData();
        }

        private void btnRealReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRealSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateForm(); // Validate form fields

                // Check if any customer is selected
                if (!IsCustomerSelected())
                {
                    MessageBox.Show("นำเข้าข้อมูลลูกค้า.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if any temporary invoice is selected
                if (!IsTemporaryInvoiceSelected())
                {
                    MessageBox.Show("นำเข้าข้อมูลใบส่งของชั่วคราว.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if external bill name already exists
                if (IsExternalBillNameExists(txtExternalBillName.Text))
                {
                    MessageBox.Show("มีเลขที่บิล(ORVC)ซ้ำกันอยู่ในระบบ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save real invoice
                SaveRealInvoice();
                MessageBox.Show("บันทึกข้อมูลสำเร็จ.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateForm()
        {
            if (string.IsNullOrEmpty(txtIssueDate.Text) || string.IsNullOrEmpty(txtExternalBillName.Text))
            {
                throw new Exception("กรอกข้อมูลให้ครบถ้วน.");
            }
        }

        private bool IsCustomerSelected()
        {
            return !string.IsNullOrEmpty(labelCustomerID.Text);
        }

        private bool IsTemporaryInvoiceSelected()
        {
            foreach (DataGridViewRow row in dataGridViewTemporaryInvList.Rows)
            {
                if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell && Convert.ToBoolean(checkBoxCell.Value))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsExternalBillNameExists(string externalBillName)
        {
            return InvoiceClass.IsExternalBillNameExists(externalBillName);
        }

        private void SaveRealInvoice()
        {
            // Get data from form
            int selectedCustomerID = Convert.ToInt32(labelCustomerID.Text);
            DateTime invoiceDate = txtIssueDate.Value.Date;
            decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            decimal depositAmount = Convert.ToDecimal(labelDepositAmount.Text);
            decimal discountAmount = Convert.ToDecimal(labelDiscountAmount.Text);
            string externalBillName = txtExternalBillName.Text;
            string details = txtDetails.Text;

            // Create real invoice object
            InvoiceClass realInvoice = new InvoiceClass
            {
                InvoiceDate = invoiceDate,
                TotalAmount = totalAmount,
                DepositAmount = depositAmount,
                ExternalBillName = externalBillName,
                Details = details,
                CreateByUser = loggedInUserID,
                CustomerID = selectedCustomerID,
                DiscountAmount = discountAmount,
                OutstandingDebt = totalAmount
            };

            // Save real invoice to database
            int realInvoiceID = InvoiceClass.InsertRealInvoiceToDatabase(realInvoice);

            // Save selected temporary invoices
            SaveSelectedTemporaryInvoices(realInvoiceID);

            // Save transaction
            SaveTransaction(realInvoiceID, selectedCustomerID);

            // Reload data and clear form
            ClearForm();
        }

        private void SaveSelectedTemporaryInvoices(int realInvoiceID)
        {
            foreach (DataGridViewRow row in dataGridViewTemporaryInvList.Rows)
            {
                if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell && Convert.ToBoolean(checkBoxCell.Value))
                {
                    int temporaryInvoiceID = Convert.ToInt32(row.Cells["TemporaryInvoiceID"].Value);
                    RealInvoiceTemporaryInvoiceClass realInvoiceTemporaryInvoice = new RealInvoiceTemporaryInvoiceClass
                    {
                        RealInvoiceID = realInvoiceID,
                        TemporaryInvoiceID = temporaryInvoiceID
                    };
                    realInvoiceTemporaryInvoice.InsertToDatabase();
                }
            }
        }

        private void SaveTransaction(int realInvoiceID, int selectedCustomerID)
        {
            RealInvoiceTransactionClass realInvoiceTransaction = new RealInvoiceTransactionClass
            {
                RealInvoiceID = realInvoiceID,
                TransactionDate = DateTime.Now,
                DiscountAmount = decimal.Parse(labelDiscountAmount.Text),
                DepositAmount = decimal.Parse(labelDepositAmount.Text),
                CustomerID = selectedCustomerID,
                CreateByUser = loggedInUserID,
                // Add additional transaction data here
            };
            realInvoiceTransaction.InsertToDatabase();
        }

        private void ClearForm()
        {
            // Unsubscribe from the TextChanged event temporarily
            txtTotalAmount.TextChanged -= txtTotalAmount_TextChanged;

            try
            {
                // Your existing code for clearing the form goes here
                // Clear TextBoxes
                txtExternalBillName.Text = string.Empty;
                txtTotalAmount.Text = string.Empty;
                labelDepositAmount.Text = string.Empty;
                txtDetails.Text = string.Empty;
                labelDiscountAmount.Text = string.Empty;
                labelTotalAmount.Text = string.Empty;
                // Clear DateTimePicker
                txtIssueDate.Value = DateTime.Now;
                labelCustomerID.Text = string.Empty;
                labelCustomerName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-subscribe to the TextChanged event
                txtTotalAmount.TextChanged += txtTotalAmount_TextChanged;
            }
        }
        private void LoadTemporaryInvoiceData()
        {
            try
            { // Load temporary data from the database
                DataTable temporaryData = TemporaryDataAccess.LoadTemporaryInvoices();
                dataGridViewTemporaryInvList.DataSource = temporaryData;
                ColumnsHeaderClass.SetTemporaryThaiColumnHeaders(dataGridViewTemporaryInvList);
                // Add checkbox column if needed
                AddCheckboxColumnIfNeeded(dataGridViewTemporaryInvList);
                dataGridViewTemporaryInvList.SetDecimalFormatInColumns(tempoData.StringList);
                dataGridViewTemporaryInvList.DataBindingComplete += DataGridViewTemporaryInvList_DataBindingComplete;
                // Hide RealInvoiceID and CustomerID columns
                dataGridViewTemporaryInvList.Columns["TemporaryInvoiceID"].Visible = false;
                dataGridViewTemporaryInvList.Columns["CustomerID"].Visible = false;
            } catch (Exception ex)
            {
                // จัดการข้อผิดพลาด
                HandleError(ex);
            }   
        }

        private void DataGridViewTemporaryInvList_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewTemporaryInvList.Rows)
            {
                row.UpdateRowTemporary();
            }
        }
        private void txt_SearchTemporaryInv_TextChanged(object sender, EventArgs e)
        {
            // Perform search when the search text changes
            string searchTerm = txt_SearchTemporaryInv.Text.Trim();
            SearchTemporaryInvoiceData(searchTerm);
        }

        private void SearchTemporaryInvoiceData(string searchTerm)
        {
            // Search temporary invoices with the provided term
            DataTable searchResult = TemporaryDataAccess.LoadTemporaryInvoices(searchTerm);
            dataGridViewTemporaryInvList.DataSource = searchResult;

            // Add checkbox column if not present
            AddCheckboxColumnIfNeeded(dataGridViewTemporaryInvList);

            // Uncheck all checkboxes
            UncheckAllCheckboxes(dataGridViewTemporaryInvList);
        }

        private void AddCheckboxColumnIfNeeded(DataGridView dataGridView)
        {
            // Add checkbox column if not present
            if (dataGridView.Columns["CheckboxColumn"] == null)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "เลือก";
                checkBoxColumn.Name = "CheckboxColumn";
                dataGridView.Columns.Insert(0, checkBoxColumn);
            }
        }

        private void UncheckAllCheckboxes(DataGridView dataGridView)
        {
            // Uncheck all checkboxes
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewCheckBoxCell? checkBoxCell = row.Cells["CheckboxColumn"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = false;
                }
            }
        }
        private void btnImportData_Click(object sender, EventArgs e)
        {
            if (!IsAnyRowSelected())
            {
                MessageBox.Show("เลือกและนำเข้าข้อมูล.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImportAndDisplaySelectedData();
        }

        private bool IsAnyRowSelected()
        {
            foreach (DataGridViewRow row in dataGridViewTemporaryInvList.Rows)
            {
                if (IsRowSelected(row))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsRowSelected(DataGridViewRow row)
        {
            if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell && Convert.ToBoolean(checkBoxCell.Value))
            {
                return true;
            }
            return false;
        }

        private void ImportAndDisplaySelectedData()
        {
            // Unsubscribe from the TextChanged event temporarily
            txtTotalAmount.TextChanged -= txtTotalAmount_TextChanged;
            try
            {
                decimal totalDiscountAmount = 0;
                decimal totalDepositAmount = 0;
                decimal totalAmount = 0;
                int customerID = 0;
                string customerName = "";

                foreach (DataGridViewRow row in dataGridViewTemporaryInvList.Rows)
                {
                    if (IsRowSelected(row))
                    {
                        int temporaryInvoiceID = Convert.ToInt32(row.Cells["TemporaryInvoiceID"].Value);

                        // Check if temporary invoice is already imported
                        if (IsTemporaryInvoiceImported(temporaryInvoiceID))
                        {
                            // Prompt user to select again
                            MessageBox.Show("บิลนี้ถูกใช้งานแล้ว. กรุณาเลือกใหม่อีกครั้ง.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LoadTemporaryInvoiceData(); // Reload temporary invoice data
                            ClearForm();
                            return;
                        }
                        else
                        {
                            ProcessSelectedRow(row, ref totalDiscountAmount, ref totalDepositAmount, ref totalAmount, ref customerID, ref customerName);
                        }
                    }
                }
                // Save the original deposit amount value
                originalDepositAmount = totalDepositAmount;
                UpdateUIWithCalculatedValues(totalDiscountAmount, totalDepositAmount, totalAmount, customerID, customerName);
                DisplayDetailsOfSelectedRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-subscribe to the TextChanged event
                txtTotalAmount.TextChanged += txtTotalAmount_TextChanged;
            }
        }

        private void ProcessSelectedRow(DataGridViewRow row, ref decimal totalDiscountAmount, ref decimal totalDepositAmount, ref decimal totalAmount, ref int customerID, ref string customerName)
        {
            if (decimal.TryParse(row.Cells["DiscountAmount"].Value?.ToString(), out decimal discountAmount))
            {
                totalDiscountAmount += discountAmount;
            }

            if (decimal.TryParse(row.Cells["DepositAmount"].Value?.ToString(), out decimal depositAmount))
            {
                totalDepositAmount += depositAmount;
            }

            if (decimal.TryParse(row.Cells["TotalAmount"].Value?.ToString(), out decimal amount))
            {
                totalAmount += amount;
            }

            if (customerID == 0)
            {
                customerID = Convert.ToInt32(row.Cells["CustomerID"].Value);
                customerName = row.Cells["CustomerName"].Value?.ToString();
            }
        }

        private void UpdateUIWithCalculatedValues(decimal totalDiscountAmount, decimal totalDepositAmount, decimal totalAmount, int customerID, string customerName)
        {
            labelDiscountAmount.Text = totalDiscountAmount.ToString();
            labelDepositAmount.Text = totalDepositAmount.ToString();
            txtTotalAmount.Text = totalAmount.ToString();
            labelCustomerID.Text = customerID.ToString();
            labelCustomerName.Text = customerName;
            labelTotalAmount.Text = totalAmount.ToString();
        }

        private void DisplayDetailsOfSelectedRows()
        {
            StringBuilder detailsBuilder = new StringBuilder();

            foreach (DataGridViewRow row in dataGridViewTemporaryInvList.Rows)
            {
                if (IsRowSelected(row))
                {
                    AppendRowDetailsToBuilder(row, detailsBuilder);
                }
            }

            txtDetails.Text = detailsBuilder.ToString();
        }

        private void AppendRowDetailsToBuilder(DataGridViewRow row, StringBuilder detailsBuilder)
        {
            if (row.Cells["Ornc_No"].Value != null)
            {
                detailsBuilder.AppendLine(row.Cells["Ornc_No"].Value.ToString());
            }

            if (row.Cells["Details"].Value != null)
            {
                detailsBuilder.AppendLine("*** :" + row.Cells["Details"].Value.ToString());
            }
        }
        private bool IsTemporaryInvoiceImported(int temporaryInvoiceID)
        {
            // Query the database to check if the temporary invoice ID exists in realinvoices_temporaryinvoices table
            return RealInvoiceTemporaryInvoiceClass.IsTemporaryInvoiceLinkedToRealInvoice(temporaryInvoiceID);
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Convert text values to decimal for comparison
                decimal newTotalAmount = decimal.Parse(txtTotalAmount.Text);
                decimal labelTotal = decimal.Parse(labelTotalAmount.Text);

                if (newTotalAmount > labelTotal)
                {
                    // Calculate the difference between new total amount and current deposit amount
                    decimal difference = newTotalAmount - labelTotal;

                    // Add the difference to the current deposit amount and update labelDepositAmount
                    decimal currentDepositAmount = decimal.Parse(labelDepositAmount.Text);
                    decimal updatedDepositAmount = currentDepositAmount + difference;
                    labelDepositAmount.Text = updatedDepositAmount.ToString();
                }
                else
                {
                    // Reset labelDepositAmount to its original value
                    labelDepositAmount.Text = originalDepositAmount.ToString();
                }
            }
            catch (FormatException)
            {
                // Handle invalid input format (non-numeric text)
                MessageBox.Show("ตรวจเช็คว่ากรอกจำนวนเงินถูกต้องหรือไม่.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally, you can reset txtTotalAmount or take other appropriate action
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HandleError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
