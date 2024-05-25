using MySql.Data.MySqlClient;

namespace GDAPPWIN
{
    internal class RealInvoiceTransactionClass
    {
        public int RealInvoiceID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public int CustomerID { get; set; }
        public int CreateByUser { get; set; }
        public bool isPaid { get; set; }

        public void InsertToDatabase()
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();

                    // สร้างคำสั่ง SQL INSERT โดยใช้ RealInvoiceID และข้อมูล transaction
                    string query = "INSERT INTO realinvs_temporaryinvstransaction (RealInvoiceID, TransactionDate, DiscountAmount, DepositAmount, CustomerID, CreateByUser) VALUES (@RealInvoiceID, @TransactionDate, @DiscountAmount, @DepositAmount, @CustomerID, @CreateByUser)";

                    // สร้าง MySqlCommand และกำหนดค่าพารามิเตอร์
                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@RealInvoiceID", RealInvoiceID);
                        command.Parameters.AddWithValue("@TransactionDate", TransactionDate);
                        command.Parameters.AddWithValue("@DiscountAmount", DiscountAmount);
                        command.Parameters.AddWithValue("@DepositAmount", DepositAmount);
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@CreateByUser", CreateByUser);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }

}
