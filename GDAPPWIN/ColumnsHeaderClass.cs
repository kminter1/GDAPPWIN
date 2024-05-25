using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public class ColumnsHeaderClass
    {
        public string Name { get; }
        public string DisplayName { get; }
        public ColumnsHeaderClass(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public static readonly List<ColumnsHeaderClass> ReceiptsColumnHeaders = new List<ColumnsHeaderClass>
        {
            new ColumnsHeaderClass("ReceiptID", "Receipt ID"),
            new ColumnsHeaderClass("CustomerID", "ID (ลูกค้า)"),
            new ColumnsHeaderClass("Ccode", "รหัสลูกค้า"),
            new ColumnsHeaderClass("CustomerName", "ชื่อลูกค้า"),
            new ColumnsHeaderClass("ReceiptNumber", "เลขที่ใบเสร็จ"),
            new ColumnsHeaderClass("NetCash", "เงินรับสุทธิ"),
            new ColumnsHeaderClass("WithHoldingTax", "หัก ณ ที่จ่าย"),
            new ColumnsHeaderClass("Fee", "ค่าธรรมเนียม"),
            new ColumnsHeaderClass("DiscountAmount", "ส่วนลด"),
            new ColumnsHeaderClass("DepositAmount", "ยอดรวม(เงินฝาก)"),
            new ColumnsHeaderClass("DepositPayment", "ยอดที่ต้องจ่าย(20%)"),
            new ColumnsHeaderClass("ReceiptDate", "วันที่"),
            new ColumnsHeaderClass("PaymentMethod", "วิธีจ่ายเงิน"),
            new ColumnsHeaderClass("Remark", "หมายเหตุ"),
            new ColumnsHeaderClass("CreateByUser", "พนักงาน"),
            new ColumnsHeaderClass("CreateUser", "พนักงาน"),
            new ColumnsHeaderClass("CreatedDate", "วันที่ทำธุรกรรม"),
            new ColumnsHeaderClass("InvoiceType", "ประเภทบิล"),
            new ColumnsHeaderClass("IssueDate", "วันที่"),
            new ColumnsHeaderClass("TotalAmount", "จำนวนเงิน"),
            new ColumnsHeaderClass("Details", "รายละเอียด"),
            new ColumnsHeaderClass("Ornc_No", "เลขที่บิล (ORNC)"),
            new ColumnsHeaderClass("ExternalBillName", "เลขที่บิล (ORVC)"),
            new ColumnsHeaderClass("Status", "สถานะใบเสร็จ")
        };

        public static readonly List<ColumnsHeaderClass> CustomersColumnHeaders = new List<ColumnsHeaderClass>
        {
            new ColumnsHeaderClass("Select", "เลือก"),
            new ColumnsHeaderClass("CustomerID", "ID (ลูกค้า)"),
            new ColumnsHeaderClass("Ccode", "รหัสลูกค้า"),
            new ColumnsHeaderClass("Cname", "ชื่อลูกค้า")
        };

        public static readonly List<ColumnsHeaderClass> InvoicesColumnHeaders = new List<ColumnsHeaderClass>
        {
            new ColumnsHeaderClass("RealInvoiceID", "ID (ORVC)"),
            new ColumnsHeaderClass("CustomerID", "ID (ลูกค้า)"),
            new ColumnsHeaderClass("Ccode", "รหัสลูกค้า"),
            new ColumnsHeaderClass("InvoiceDate", "วันที่ออก"),
            new ColumnsHeaderClass("CustomerName", "ชื่อลูกค้า"),
            new ColumnsHeaderClass("TotalAmount", "จำนวนเงิน"),
            new ColumnsHeaderClass("Details", "รายละเอียด"),
            new ColumnsHeaderClass("ExternalBillName", "เลขที่(ORVC)"),
            new ColumnsHeaderClass("createdByName", "พนักงาน"),
            new ColumnsHeaderClass("DiscountAmount", "ส่วนลด"),
            new ColumnsHeaderClass("TotalDiscountUsed", "จ่ายส่วนลดแล้ว"),
            new ColumnsHeaderClass("DiscountTransactionDate", "วันที่จ่ายส่วนลด"),
            new ColumnsHeaderClass("DiscountStatus", "สถานะส่วนลด"),
            new ColumnsHeaderClass("DepositAmount", "จำนวนเงินฝาก"),
            new ColumnsHeaderClass("TotalDepositUsed", "ยอดรวม(เงินฝาก)"),
            new ColumnsHeaderClass("AmountToPay", "ยอดที่ต้องจ่าย(20%)"),
            new ColumnsHeaderClass("DepositTransactionDate", "วันที่จ่ายเงินฝาก"),
            new ColumnsHeaderClass("DepositStatus", "สถานะเงินฝาก"),
            new ColumnsHeaderClass("VoidStatus", "สถานะ"),
            new ColumnsHeaderClass("OutstandingDebt", "ยอดหนี้"),
            new ColumnsHeaderClass("TotalPayment", "ยอดชำระหนี้"),
            new ColumnsHeaderClass("ReceiptNumber", "เลขที่ใบเสร็จ"),
            new ColumnsHeaderClass("CreatedDate", "วันที่จ่าย")
        };

        public static readonly List<ColumnsHeaderClass> TemporaryColumnHeaders = new List<ColumnsHeaderClass>
        {
            new ColumnsHeaderClass("TemporaryInvoiceID", "ID(ORNC)"),
            new ColumnsHeaderClass("CustomerID", "ID (ลูกค้า)"),
            new ColumnsHeaderClass("Ccode", "รหัสลูกค้า"),
            new ColumnsHeaderClass("IssueDate", "วันที่ออก"),
            new ColumnsHeaderClass("CustomerName", "ชื่อลูกค้า"),
            new ColumnsHeaderClass("Details", "รายละเอียด"),
            new ColumnsHeaderClass("Ornc_No", "เลขที่ (ORNC)"),
            new ColumnsHeaderClass("TotalAmount", "จำนวนเงิน"),
            new ColumnsHeaderClass("createByUser", "พนักงาน"),
            new ColumnsHeaderClass("DiscountAmount", "เงินส่วนลด"),
            new ColumnsHeaderClass("TotalDiscountUsed", "จ่ายส่วนลดแล้ว"),
            new ColumnsHeaderClass("DiscountTransactionDate", "วันที่จ่าย"),
            new ColumnsHeaderClass("DiscountStatus", "สถานะส่วนลด"),
            new ColumnsHeaderClass("DepositAmount", "เงินฝาก"),
            new ColumnsHeaderClass("TotalDepositUsed", "ยอดรวมเงินฝาก"),
            new ColumnsHeaderClass("AmountToPay", "ยอดที่ต้องจ่าย"),
            new ColumnsHeaderClass("DepositTransactionDate", "วันที่จ่าย"),
            new ColumnsHeaderClass("DepositStatus", "สถานะเงินฝาก"),
            new ColumnsHeaderClass("ExternalBillName", "P2 เปลี่ยนบิล (ORVC)"),
            new ColumnsHeaderClass("ReceiptNumber", "P3 ลูกค้าจ่ายเงิน (ORCC)"),
            new ColumnsHeaderClass("Status", "สถานะ"),
            new ColumnsHeaderClass("Ucode", "รหัสพนักงาน"),
            new ColumnsHeaderClass("OutstandingDebt", "ยอดหนี้"),
            new ColumnsHeaderClass("TotalPayment", "ยอดที่ชำระหนี้")
        };

        public static readonly List<ColumnsHeaderClass> SummaryColumnHeaders = new List<ColumnsHeaderClass>
        {
            new ColumnsHeaderClass("TemporaryInvoiceID", "ID (ORNC)"),
            new ColumnsHeaderClass("RealInvoiceID", "ID (ORVC)"),
            new ColumnsHeaderClass("CustomerID", "ID (ลูกค้า)"),
            new ColumnsHeaderClass("Ccode", "รหัสลูกค้า"),
            new ColumnsHeaderClass("CustomerName", "ชื่อลูกค้า"),
            new ColumnsHeaderClass("TemporaryInvoiceDate", "วันที่ออก (ORNC)"),
            new ColumnsHeaderClass("RealInvoiceDate", "วันที่ออก (ORVC)"),
            new ColumnsHeaderClass("TemporaryTotalAmount", "จำนวนเงิน (ORNC)"),
            new ColumnsHeaderClass("RealTotalAmount", "จำนวนเงิน (ORVC)"),
            new ColumnsHeaderClass("TemporaryDetails", "รายละเอียด (ORNC)"),
            new ColumnsHeaderClass("RealDetails", "รายละเอียด (ORVC)"),
            new ColumnsHeaderClass("CreatedByName", "พนักงาน"),
            new ColumnsHeaderClass("CreatedDate", "วันที่ทำธุรกรรม"),
            new ColumnsHeaderClass("RealDiscountAmount", "ส่วนลด (ORVC)"),
            new ColumnsHeaderClass("TemporaryDiscountAmount", "ส่วนลด (ORNC)"),
            new ColumnsHeaderClass("TotalDiscountUsed", "จ่ายส่วนลดแล้ว"),
            new ColumnsHeaderClass("DiscountTransactionDate", "วันที่จ่ายส่วนลด"),
            new ColumnsHeaderClass("DiscountStatus", "สถานะส่วนลด"),
            new ColumnsHeaderClass("RealDepositAmount", "เงินฝาก (ORVC)"),
            new ColumnsHeaderClass("TemporaryDepositAmount", "เงินฝาก (ORNC)"),
            new ColumnsHeaderClass("TotalDepositUsed", "ยอดรวมเงินฝาก"),
            new ColumnsHeaderClass("AmountToPay", "ยอดที่ต้องจ่าย"),
            new ColumnsHeaderClass("DepositTransactionDate", "วันที่จ่ายเงินฝาก"),
            new ColumnsHeaderClass("DepositStatus", "สถานะเงินฝาก"),
            new ColumnsHeaderClass("VoidStatus", "สถานะ"),
            new ColumnsHeaderClass("ReceiptNumber", "เลขที่ใบเสร็จ"),
            new ColumnsHeaderClass("TotalPayment", "ยอดชำระหนี้"),
            new ColumnsHeaderClass("OutstandingDebt", "ยอดหนี้")
        };

        public static readonly List<ColumnsHeaderClass> NotPaidColumnHeaders = new List<ColumnsHeaderClass>
        {
            new ColumnsHeaderClass("TemporaryInvoiceID", "ID (ORNC)"),
            new ColumnsHeaderClass("RealInvoiceID", "ID (ORVC)"),
            new ColumnsHeaderClass("CustomerID", "ID (ลูกค้า)"),
            new ColumnsHeaderClass("Ccode", "รหัสลูกค้า"),
            new ColumnsHeaderClass("CustomerName", "ชื่อลูกค้า"),
            new ColumnsHeaderClass("IssueDate", "วันที่ออก"),
            new ColumnsHeaderClass("InvoiceDate", "วันที่ออก (ORVC)"),
            new ColumnsHeaderClass("TotalAmount", "จำนวนเงิน (ORNC)"),
            new ColumnsHeaderClass("OutstandingDebt", "ยอดหนี้ค้างชำระ (ORVC)"),
            new ColumnsHeaderClass("Details", "รายละเอียด"),
            new ColumnsHeaderClass("ExternalBillName", "เลขที่บิล (ORVC)"),
            new ColumnsHeaderClass("CreatedByName", "พนักงาน"),
            new ColumnsHeaderClass("CreatedDate", "วันที่ทำธุรกรรม"),
            new ColumnsHeaderClass("DiscountAmount", "ส่วนลด (ORVC)"),
            new ColumnsHeaderClass("TotalDiscountUsed", "จ่ายส่วนลดแล้ว"),
            new ColumnsHeaderClass("DiscountTransactionDate", "วันที่จ่ายส่วนลด"),
            new ColumnsHeaderClass("DiscountStatus", "สถานะส่วนลด"),
            new ColumnsHeaderClass("DepositAmount", "เงินฝาก (ORVC)"),
            new ColumnsHeaderClass("TotalDepositUsed", "ยอดรวมเงินฝาก"),
            new ColumnsHeaderClass("AmountToPay", "ยอดที่ต้องจ่าย"),
            new ColumnsHeaderClass("DepositTransactionDate", "วันที่จ่ายเงินฝาก"),
            new ColumnsHeaderClass("DepositStatus", "สถานะเงินฝาก"),
            new ColumnsHeaderClass("VoidStatus", "สถานะ"),
            new ColumnsHeaderClass("ReceiptNumber", "เลขที่ใบเสร็จ"),
            new ColumnsHeaderClass("TotalPayment", "ยอดชำระหนี้"),
            new ColumnsHeaderClass("RTotalAmount", "ยอดรวม (ORVC)")
        };

        public static void SetReceiptThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in ReceiptsColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }

        public static void SetCustomersThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in CustomersColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }

        public static void SetInvoicesThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in InvoicesColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }

        public static void SetTemporaryThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in TemporaryColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }

        public static void SetSummaryThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in SummaryColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }

        public static void SetNotPaidThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in NotPaidColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }
    }
}
