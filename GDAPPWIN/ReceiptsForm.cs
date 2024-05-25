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
    public partial class ReceiptsForm : Form
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

        public ReceiptsForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
        }
        private void ReceiptsForm_Load(object sender, EventArgs e)
        {
            LoadReceiptsData();
        }
        private void LoadReceiptsData()
        {
            try
            {
                DataTable receiptsData = ReceiptDataAccess.GetAllReceipts();

                // Display data in DataGridView
                dataGridViewListReceipts.DataSource = receiptsData;

                // Create an instance of ReceiptDataAccess
                ReceiptDataAccess receiptDataAccess = new ReceiptDataAccess();

                // Set column headers using ReceiptDataAccess instance
                ReceiptDataAccess.SetDataThaiColumnHeaders(dataGridViewListReceipts);

                // Set decimal format in DataGridView columns
                ReceiptDataAccess.SetDecimalFormatInDataGridViewColumns(dataGridViewListReceipts);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dataGridViewListReceipts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int receiptID = Convert.ToInt32(dataGridViewListReceipts.Rows[e.RowIndex].Cells["ReceiptID"].Value);
                LoadRealInvoiceData(receiptID);
            }
        }
        private void LoadRealInvoiceData(int receiptID)
        {
            List<InvoiceClass> realInvoices = ReceiptsClass.GetRealInvoicesByReceiptID(receiptID);
            dataGridViewListRealInvoice.DataSource = realInvoices;
        }
        
    }
}
