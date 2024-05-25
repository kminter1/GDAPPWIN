namespace GDAPPWIN
{
    public class OrncBill
    {
        public int TemporaryInvoiceID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime IssueDate { get; set; }
        public string Details { get; set; }
        public string Ornc_No { get; set; }
        public decimal TotalAmount { get; set; }
        public string LinkStatus { get; set; }
        public string ExternalBillName { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DepositAmount { get; set;}  
    }
}
