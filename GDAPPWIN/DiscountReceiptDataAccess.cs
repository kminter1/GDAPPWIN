using MySql.Data.MySqlClient;


namespace GDAPPWIN
{
    internal class DiscountReceiptDataAccess
    {
        public static bool AddDiscountReceiptLink1(int discountID, int receiptID)
        {
            bool success = false;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = "INSERT INTO discounts_receipts (DiscountID, ReceiptID) VALUES (@DiscountID, @ReceiptID)";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@DiscountID", discountID);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    success = true;
                }
            }

            return success;
        }
        public static bool AddDiscountReceiptLink(int discountID, int receiptID, decimal discountAmount, DateTime transactionDate)
        {
            bool success = false;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"INSERT INTO discounts_receipts (ReceiptID, DiscountID, DiscountAmount, TransactionDate) 
                             VALUES (@ReceiptID, @DiscountID, @DiscountAmount, @TransactionDate)";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);
                cmd.Parameters.AddWithValue("@DiscountID", discountID);
                cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    success = true;
                }

                conn.CloseConnection();
            }

            return success;
        }
        public static decimal GetTotalDiscountPaid(int receiptID)
        {
            decimal totalPaid = 0;
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = "SELECT SUM(DiscountAmount) FROM discounts_receipts WHERE ReceiptID = @ReceiptID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    totalPaid = Convert.ToDecimal(result);
                }

                conn.CloseConnection();
            }

            return totalPaid;
        }

        public static bool IsReceiptPaid(int receiptID)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = "SELECT COUNT(*) FROM discounts_receipts WHERE ReceiptID = @ReceiptID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                conn.CloseConnection();

                return count > 0;
            }
        }
        public static bool? CheckPaymentStatus(string invoiceID)
        {
            // ตรวจสอบว่ามีการธุรกรรมบิลในตาราง receipts_realinvoices หรือไม่
            bool hasTransaction = CheckTransactionExistence(invoiceID);

            if (!hasTransaction)
            {
                // ถ้าไม่มีการธุรกรรมบิลในตาราง receipts_realinvoices คืนค่าเป็น false
                return false;
            }

            // ดึง ReceiptID จากตาราง receipts_realinvoices
            int receiptID = GetReceiptID(invoiceID);

            // ตรวจสอบค่าสถานะในตาราง receipts
            bool? status = GetReceiptStatus(receiptID);

            // คืนค่าสถานะ
            return status;
        }

        // เช็คว่ามีการธุรกรรมบิลหรือไม่ในตาราง receipts_realinvoices
        private static bool CheckTransactionExistence(string invoiceID)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = "SELECT COUNT(*) FROM receipts_realinvoices WHERE RealInvoiceID = @RealInvoiceID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", invoiceID);
                int transactionCount = Convert.ToInt32(cmd.ExecuteScalar());
                conn.CloseConnection();
                return transactionCount > 0;
            }
        }

        // ดึง ReceiptID จากตาราง receipts_realinvoices
        private static int GetReceiptID(string invoiceID)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = "SELECT ReceiptID FROM receipts_realinvoices WHERE RealInvoiceID = @RealInvoiceID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", invoiceID);
                int receiptID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.CloseConnection();
                return receiptID;
            }
        }

        // ดึงค่าสถานะในตาราง receipts
        private static bool? GetReceiptStatus(int receiptID)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = "SELECT Status FROM receipts WHERE ReceiptID = @ReceiptID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                // ดึงค่า Status จากฐานข้อมูล
                object statusObj = cmd.ExecuteScalar();

                // ตรวจสอบว่าค่า Status เป็น NULL หรือไม่
                if (statusObj == DBNull.Value)
                {
                    // ถ้าเป็น NULL คืนค่า null
                    return null;
                }
                else
                {
                    // แปลงค่า Status เป็น bool และคืนค่าออกไป
                    return Convert.ToBoolean(statusObj);
                }
            }
        }

    }
}

