using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GDAPPWIN
{
    public partial class SummaryForm : Form
    {
        TemporaryDataAccess tempoData;
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;

        public int LoggedInUserID => loggedInUserID;
        public string LoggedInUsername => loggedInUsername;
        public string LoggedInUserRole => loggedInUserRole;

        public SummaryForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            tempoData = new TemporaryDataAccess();
        }

        private void SummaryForm_Load(object sender, EventArgs e)
        {
            LoadTemporaryInvoicePaymentStatus();
        }

        private void SearchAllInvoices(string searchTerm)
        {
            try
            {
                DataTable allInvoiceStatus = TemporaryDataAccess.GetInvoicePaymentStatus(searchTerm);
                dataGridViewSummary.DataSource = allInvoiceStatus;
                ColumnsHeaderClass.SetSummaryThaiColumnHeaders(dataGridViewSummary);
                // Hide specific columns
                if (dataGridViewSummary.Columns["TemporaryInvoiceID"] != null)
                {
                    dataGridViewSummary.Columns["TemporaryInvoiceID"].Visible = false;
                }
                if (dataGridViewSummary.Columns["RealInvoiceID"] != null)
                {
                    dataGridViewSummary.Columns["RealInvoiceID"].Visible = false;
                }
                if (dataGridViewSummary.Columns["CustomerID"] != null)
                {
                    dataGridViewSummary.Columns["CustomerID"].Visible = false;
                }

                SetDecimalFormatInColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchTerm_TextChanged(object sender, EventArgs e)
        {
            SearchAllInvoices(txtSearchTerm.Text);
        }

        private void LoadTemporaryInvoicePaymentStatus()
        {
            try
            {
                DataTable temporaryInvoiceStatus = TemporaryDataAccess.GetInvoicePaymentStatus("");
                dataGridViewSummary.DataSource = temporaryInvoiceStatus;
                ColumnsHeaderClass.SetSummaryThaiColumnHeaders(dataGridViewSummary);

                // Hide specific columns
                if (dataGridViewSummary.Columns["TemporaryInvoiceID"] != null)
                {
                    dataGridViewSummary.Columns["TemporaryInvoiceID"].Visible = false;
                }
                if (dataGridViewSummary.Columns["RealInvoiceID"] != null)
                {
                    dataGridViewSummary.Columns["RealInvoiceID"].Visible = false;
                }
                if (dataGridViewSummary.Columns["CustomerID"] != null)
                {
                    dataGridViewSummary.Columns["CustomerID"].Visible = false;
                }

                SetDecimalFormatInColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDecimalFormatInColumns()
        {
            foreach (DataGridViewColumn column in dataGridViewSummary.Columns)
            {
                if (tempoData.StringList.Contains(column.Name))
                {
                    column.DefaultCellStyle.Format = "N2";
                }
            }
        }
    }
}
