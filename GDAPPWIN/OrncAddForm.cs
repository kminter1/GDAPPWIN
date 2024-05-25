//using PdfSharp.Fonts;
using System.Data;
using System.DirectoryServices;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public partial class OrncAddForm : Form
    {
        private TemporaryDataAccess tempoData;
        private OrncClass orncData;
        private List<OrncBill> selectedBills;
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
        public OrncAddForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            tempoData = new TemporaryDataAccess();
            orncData = new OrncClass();
            selectedBills = new List<OrncBill>();
        }

        bool Orncverify()
        {
            if (string.IsNullOrEmpty(textBox_OrncNo.Text) || string.IsNullOrEmpty(textBox_OrncAmount.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "เพิ่มรายการคำสั่งซื้อ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string orncNo = textBox_OrncNo.Text;
            if (orncData.IsOrncNoExists(orncNo))
            {
                MessageBox.Show("หมายเลข ORNC ซ้ำกันอยู่ในระบบ", "เพิ่มรายการคำสั่งซื้อ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button_OrncOk_Click(object sender, EventArgs e)
        {
            DateTime OrncDate = dateTimePicker_Ornc.Value;
            string OrncDetail = richTextBox_OrncDetail.Text;
            string OrncNo = textBox_OrncNo.Text;
            //decimal OrncDiscount = Convert.ToDecimal(textBox_OrncDiscount.Text);
            //decimal OrncDeposit = Convert.ToDecimal(textBox_OrncDeposit.Text);

                // ดึง ID ของลูกค้าที่ถูกเลือก
                int OrncCID = Convert.ToInt32(labelCustomerID.Text);

                if (decimal.TryParse(textBox_OrncAmount.Text, out decimal OrncAmount) && decimal.TryParse(textBox_OrncDiscount.Text, out decimal OrncDiscount) && decimal.TryParse(textBox_OrncDeposit.Text, out decimal OrncDeposit))
                {
                    if (Orncverify())
                    {
                        try
                        {
                            // เพิ่มบิลใบส่งของ
                            if (orncData.InsertOrnc(OrncCID, OrncDate, OrncDetail, OrncNo, OrncAmount, loggedInUserID, OrncDiscount, OrncDeposit))
                            {
                                MessageBox.Show("เพิ่มบิลใบส่งของสำเร็จ", "เพิ่มบิลใบส่งของ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadOrncBills();
                                ClearForm();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ใส่จำนวนเงิน", "เพิ่มใบส่งของ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        private void OrncAddForm_Load(object sender, EventArgs e)
        {
            LoadOrncBills();
            DisplayTotalOrnc();
        }

        private void LoadOrncBills()
        {
            try
            {
                DataTable orncBills = TemporaryDataAccess.LoadTemporaryInvoices();
                dataGridViewOrncList.DataSource = orncBills;
                ColumnsHeaderClass.SetTemporaryThaiColumnHeaders(dataGridViewOrncList);
                dataGridViewOrncList.SetDecimalFormatInColumns(tempoData.StringList);
                AddCheckboxColumnIfNeeded();
                dataGridViewOrncList.DataBindingComplete += DataGridViewOrncList_DataBindingComplete;
                dataGridViewOrncList.Columns["TemporaryInvoiceID"].Visible = false;
                dataGridViewOrncList.Columns["CustomerID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in LoadOrncBills: {ex.Message}");
            }
        }

        private void DataGridViewOrncList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewOrncList.Rows)
            {
                row.UpdateRowTemporary();
            }
        }
        
        private void DisplayTotalOrnc()
        {
            // คำนวณยอดรวมของรายการขายทั้งหมด
            decimal totalOrnc = OrncClass.GetTotalOrncAmountByDate(null); // ส่งค่า null เพื่อคำนวณยอดรวมทั้งหมด

            // แสดงยอดขายทั้งหมดใน label
            lbl_TotalOrnc.Text = $"ยอดขายทั้งหมด: {totalOrnc.ToString("#,##0.00")} บาท";
        }

        // Search functionality
        private void txtSearchTerm1_TextChanged(object sender, EventArgs e)
        {
                string searchTerm = txtSearchTerm.Text.Trim();
                SearchTemporaryInvoiceData(searchTerm);
        }
        private void SearchTemporaryInvoiceData(string searchTerm)
        {
            // Search temporary invoices with the provided term
            DataTable searchResult = TemporaryDataAccess.LoadTemporaryInvoices(searchTerm);
            dataGridViewOrncList.DataSource = searchResult;

            // Add checkbox column if not present
            AddCheckboxColumnIfNeeded();

            // Uncheck all checkboxes
            UncheckAllCheckboxes();
        }
        // Event handler for filtering by date
        private void BtnByDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = OutStandingDate.Value; // Get the selected date
                                                               // Convert the Thai Buddhist calendar year to Gregorian year
                int thaiYear = selectedDate.Year;
                int gregorianYear = thaiYear - 543;
                DateTime gregorianDate = new DateTime(gregorianYear, selectedDate.Month, selectedDate.Day);
                // Search temporary invoices with the provided term
                DataTable searchResult = TemporaryDataAccess.LoadTemporaryInvoices(selectedDate: gregorianDate);
                dataGridViewOrncList.DataSource = searchResult;

                // Add checkbox column if not present
                AddCheckboxColumnIfNeeded();

                // Uncheck all checkboxes
                UncheckAllCheckboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for filtering by month
        private void BtnByMonth_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = OutStandingDate.Value;
                int selectedMonth = selectedDate.Month;
                int selectedYear = selectedDate.Year;
                // Search temporary invoices with the provided term
                DataTable searchResult = TemporaryDataAccess.LoadTemporaryInvoices(selectedMonth: selectedMonth, selectedYear: selectedYear);
                dataGridViewOrncList.DataSource = searchResult;

                // Add checkbox column if not present
                AddCheckboxColumnIfNeeded();

                // Uncheck all checkboxes
                UncheckAllCheckboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Edit functionality
        private void btn_EditData_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectedBills();

                if (selectedBills.Count > 0)
                {
                    PopulateFormWithSelectedBill(selectedBills[0]);
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

        // Update functionality
        private void BtnUpdateOrnc_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the form is populated with selected bill details
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
            // Check if any relevant fields are empty
            return !string.IsNullOrEmpty(labelCustomerID.Text) &&
                   !string.IsNullOrEmpty(labelTemporaryInvoiceID.Text) &&
                   !string.IsNullOrEmpty(richTextBox_OrncDetail.Text) &&
                   !string.IsNullOrEmpty(textBox_OrncNo.Text) &&
                   !string.IsNullOrEmpty(textBox_OrncAmount.Text) &&
                   !string.IsNullOrEmpty(textBox_OrncDiscount.Text) &&
                   !string.IsNullOrEmpty(textBox_OrncDeposit.Text);
        }

        // Helper methods
        private void AddCheckboxColumnIfNeeded()
        {
            if (dataGridViewOrncList.Columns["CheckboxOrnc"] == null)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "เลือก";
                checkBoxColumn.Name = "CheckboxOrnc";
                dataGridViewOrncList.Columns.Insert(0, checkBoxColumn);
            }
        }

        private void UncheckAllCheckboxes()
        {
            foreach (DataGridViewRow row in dataGridViewOrncList.Rows)
            {
                row.Cells["CheckboxOrnc"].Value = false;
            }
        }

        private void GetSelectedBills()
        {
            selectedBills.Clear();

            foreach (DataGridViewRow row in dataGridViewOrncList.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckboxOrnc"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value))
                {
                    selectedBills.Add(CreateOrncBillFromRow(row));
                }
            }
        }

        private void UpdateSelectedBills()
        {
            foreach (OrncBill bill in selectedBills)
            {
                UpdateBillInDatabase(bill);
            }
        }

        private void UpdateBillInDatabase(OrncBill bill)
        {
            bill.CustomerID = int.Parse(labelCustomerID.Text);
            bill.TemporaryInvoiceID = int.Parse(labelTemporaryInvoiceID.Text);
            bill.IssueDate = dateTimePicker_Ornc.Value;
            bill.Details = richTextBox_OrncDetail.Text;
            bill.Ornc_No = textBox_OrncNo.Text;
            bill.TotalAmount = decimal.Parse(textBox_OrncAmount.Text);
            bill.DiscountAmount = decimal.Parse(textBox_OrncDiscount.Text);
            bill.DepositAmount = decimal.Parse(textBox_OrncDeposit.Text);

            bool success = TemporaryDataAccess.UpdateTemporaryInvoice(bill);
            if (success)
            {
                ShowInformationMessage("Data updated successfully.");
            }
            else
            {
                ShowErrorMessage("Failed to update data.");
            }
        }

        private void PopulateFormWithSelectedBill(OrncBill bill)
        {
            labelCustomerID.Text = bill.CustomerID.ToString();
            labelCustomerName.Text = bill.CustomerName;
            dateTimePicker_Ornc.Value = bill.IssueDate;
            richTextBox_OrncDetail.Text = bill.Details;
            textBox_OrncNo.Text = bill.Ornc_No;
            textBox_OrncAmount.Text = bill.TotalAmount.ToString();
            textBox_OrncDiscount.Text = bill.DiscountAmount.ToString();
            textBox_OrncDeposit.Text = bill.DepositAmount.ToString();
            labelTemporaryInvoiceID.Text = bill.TemporaryInvoiceID.ToString();
        }

        private OrncBill CreateOrncBillFromRow(DataGridViewRow row)
        {
            return new OrncBill
            {
                TemporaryInvoiceID = Convert.ToInt32(row.Cells["TemporaryInvoiceID"].Value),
                CustomerID = Convert.ToInt32(row.Cells["CustomerID"].Value),
                CustomerName = row.Cells["CustomerName"].Value.ToString(),
                IssueDate = Convert.ToDateTime(row.Cells["IssueDate"].Value),
                Details = row.Cells["Details"].Value.ToString(),
                Ornc_No = row.Cells["Ornc_No"].Value.ToString(),
                TotalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value),
                DepositAmount = Convert.ToDecimal(row.Cells["DepositAmount"].Value),
                DiscountAmount = Convert.ToDecimal(row.Cells["DiscountAmount"].Value)
            };
        }
        private void dataGridViewOrncList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewOrncList.Columns["CheckboxOrnc"].Index)
            {
                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)dataGridViewOrncList.Rows[e.RowIndex].Cells["CheckboxOrnc"];

                // Uncheck all checkboxes except the clicked one
                UncheckAllCheckboxesExceptClicked(e.RowIndex);

                // Toggle the clicked checkbox
                bool currentValue = Convert.ToBoolean(chkCell.Value);
                chkCell.Value = !currentValue;
            }
        }

        private void UncheckAllCheckboxesExceptClicked(int clickedRowIndex)
        {
            foreach (DataGridViewRow row in dataGridViewOrncList.Rows)
            {
                if (row.Index != clickedRowIndex)
                {
                    DataGridViewCheckBoxCell otherChkCell = (DataGridViewCheckBoxCell)row.Cells["CheckboxOrnc"];
                    otherChkCell.Value = false;
                }
            }
        }

        private void ClearFormAndLoadData()
        {
            ClearForm();
            LoadOrncBills();
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

        private void btn_OrncCancle_click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            // ล้างค่าทุกคอนโทรลบนฟอร์ม
            dateTimePicker_Ornc.Value = DateTime.Now;
            richTextBox_OrncDetail.Clear();
            textBox_OrncNo.Clear();
            textBox_OrncAmount.Clear();
            labelTemporaryInvoiceID.Text = string.Empty;
            labelCustomerName.Text = string.Empty;
            labelCustomerID.Text = string.Empty;
            textBox_OrncDeposit.Text = string.Empty;
            textBox_OrncDiscount.Text = string.Empty;
        }
        private void BtnIsVoided_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dataGridViewOrncList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a temporary invoice to void.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if a checkbox is checked for the selected row
            DataGridViewCheckBoxCell chkCell = dataGridViewOrncList.SelectedRows[0].Cells["CheckboxOrnc"] as DataGridViewCheckBoxCell;
            if (chkCell == null || !Convert.ToBoolean(chkCell.Value))
            {
                MessageBox.Show("Please check the checkbox for the selected temporary invoice.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected temporary invoice ID from the DataGridView
            int temporaryInvoiceID = Convert.ToInt32(dataGridViewOrncList.SelectedRows[0].Cells["TemporaryInvoiceID"].Value);

            try
            {
                // Call the method to void the temporary invoice
                TemporaryDataAccess tempDataAccess = new TemporaryDataAccess();
                tempDataAccess.VoidTemporaryInvoice(temporaryInvoiceID);

                // Display success message
                MessageBox.Show("Temporary invoice voided successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the temporary invoice data
                LoadOrncBills();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
