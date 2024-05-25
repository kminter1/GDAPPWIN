using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public partial class NotPaidForm : Form
    {
        public int LoggedInUserID { get; private set; }
        public string LoggedInUsername { get; private set; }
        public string LoggedInUserRole { get; private set; }

        public NotPaidForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.LoggedInUserID = userID;
            this.LoggedInUsername = username;
            this.LoggedInUserRole = userRole;
        }

        private void NotPaidForm_Load(object sender, EventArgs e)
        {
            LoadNotPaidInvoices();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchTerm.Text.Trim();
            DateTime? startDate = ConvertThaiDateToGregorian(datePickerStart.Value.Date);
            DateTime? endDate = ConvertThaiDateToGregorian(datePickerEnd.Value.Date);

            LoadNotPaidInvoices(searchTerm, startDate, endDate);
        }

        private DateTime ConvertThaiDateToGregorian(DateTime thaiDate)
        {
            int thaiYear = thaiDate.Year;
            int gregorianYear = thaiYear - 543;
            return new DateTime(gregorianYear, thaiDate.Month, thaiDate.Day);
        }

        private void LoadNotPaidInvoices(string searchTerm = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var (notPaidTemporaryInvoices, tempTotalAmountSum, tempDiscountAmountSum, tempDepositAmountSum) = TemporaryDataAccess.GetNotPaidTemporaryInvoices(searchTerm, startDate, endDate);
                var (notPaidRealInvoices, realOutstandingDebtSum, realDiscountAmountSum, realDepositAmountSum) = RealInvoiceDataAccess.GetNotPaidRealInvoices(searchTerm, startDate, endDate);

                DataTable combinedData = new DataTable();
                combinedData.Merge(notPaidTemporaryInvoices);
                combinedData.Merge(notPaidRealInvoices);

                dataGridViewNotPaidInvoices.DataSource = combinedData;
                ColumnsHeaderClass.SetNotPaidThaiColumnHeaders(dataGridViewNotPaidInvoices);
                // Hide columns
                if (dataGridViewNotPaidInvoices.Columns.Contains("TemporaryInvoiceID"))
                {
                    dataGridViewNotPaidInvoices.Columns["TemporaryInvoiceID"].Visible = false;
                }
                if (dataGridViewNotPaidInvoices.Columns.Contains("CustomerID"))
                {
                    dataGridViewNotPaidInvoices.Columns["CustomerID"].Visible = false;
                }
                if (dataGridViewNotPaidInvoices.Columns.Contains("RealInvoiceID"))
                {
                    dataGridViewNotPaidInvoices.Columns["RealInvoiceID"].Visible = false;
                }

                decimal totalAmountSum = tempTotalAmountSum + realOutstandingDebtSum;
                decimal discountAmountSum = tempDiscountAmountSum + realDiscountAmountSum;
                decimal depositAmountSum = tempDepositAmountSum + realDepositAmountSum;

                labelTotalAmountSum.Text = $"ยอดรวม: {totalAmountSum.ToString("C")}";
                labelDiscountAmountSum.Text = $"ส่วนลด: {discountAmountSum.ToString("C")}";
                labelDepositAmountSum.Text = $"ยอดเงินฝาก: {depositAmountSum.ToString("C")}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
