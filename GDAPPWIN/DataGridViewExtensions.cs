using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPPWIN
{
    internal static class DataGridViewExtensions
    {
        public static void SetDecimalFormatInColumns(this DataGridView dataGridView, IEnumerable<string> columns)
        {
            foreach (var columnName in columns)
            {
                if (dataGridView.Columns.Contains(columnName))
                {
                    dataGridView.Columns[columnName].DefaultCellStyle.Format = "#,##0.00";
                }
            }
        }

        public static void SetColumnHeadersFromDictionary(this DataGridView dataGridView, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (dataGridView.Columns.Contains(header.Key))
                {
                    dataGridView.Columns[header.Key].HeaderText = header.Value;
                }
            }
        }
        public static void AddCheckboxColumnIfNeeded(this DataGridView dataGridView)
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

        public static void SetDataGridViewProperties(this DataGridView dataGridView)
        {
            // Set Thai column headers
            InvoiceClass invoiceData = new InvoiceClass();
            Dictionary<string, string> invoicewithstatuscolumnHeader = invoiceData.InvoicesHeaderFormat;
            dataGridView.SetDataGridViewColumnHeadersFromDictionary(invoicewithstatuscolumnHeader);

            // Format decimal numbers in DataGridView columns
            dataGridView.SetDecimalFormatInColumns(invoiceData.InvoicesDecimalFormat);
        }

        public static void SetDataGridViewColumnHeadersFromDictionary(this DataGridView dataGridView, Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                if (dataGridView.Columns.Contains(header.Key))
                {
                    dataGridView.Columns[header.Key].HeaderText = header.Value;
                }
            }
        }

        public static void UpdateRowColor(this DataGridViewRow row)
        {
            int realInvoiceID = Convert.ToInt32(row.Cells["RealInvoiceID"].Value);
            (bool payAgain, decimal totalPaid) = ReceiptDataAccess.CanPayRealInvoiceAgain(realInvoiceID);
            bool isrealinvoiceAssociated = ReceiptDataAccess.IsRealInvoiceAssociated(realInvoiceID);

            if (payAgain)
            {
                // Set color to light yellow
                row.DefaultCellStyle.BackColor = Color.Green;
                // Set font color to black
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (!isrealinvoiceAssociated)
            {
                // Set color to green
                row.DefaultCellStyle.BackColor = Color.White;
                // Set font color to white
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                // Set color to white
                row.DefaultCellStyle.BackColor = Color.LightYellow;
                // Set font color to black
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        public static void UpdateRowTemporary(this DataGridViewRow row)
        {
            int temporaryID = Convert.ToInt32(row.Cells["TemporaryInvoiceID"].Value);
            if (TemporaryDataAccess.IsVoidedTemporary(temporaryID))
            {
                row.DefaultCellStyle.BackColor = Color.LightGray; // Set color to dark red
                row.DefaultCellStyle.ForeColor = Color.Red; // Set font color to white
            }
            else if(TemporaryDataAccess.IsPaidFully(temporaryID))
            {
                row.DefaultCellStyle.BackColor = Color.Green; // Set color to dark red
                row.DefaultCellStyle.ForeColor = Color.White; // Set font color to white
            }
            else
            {
                // เพิ่มโค้ดที่ต้องการให้แถวที่ไม่ได้ผูกบิล
                // ถ้าต้องการกำหนดสีอื่น ๆ หรือการกระทำเพิ่มเติมในกรณีที่ไม่ได้ผูกบิล
                row.DefaultCellStyle.BackColor = Color.White; // Set color to dark red
                row.DefaultCellStyle.ForeColor = Color.Black; // Set font color to white
            }
        }

        public static void UpdateRowReceipt(this DataGridViewRow row)
        {
            int receiptID = Convert.ToInt32(row.Cells["ReceiptID"].Value);
            if (ReceiptDataAccess.IsVoidedReceipt(receiptID))
            {
                row.DefaultCellStyle.BackColor= Color.LightGray;
                row.DefaultCellStyle.ForeColor= Color.Red;
            }
        }
    }
}
