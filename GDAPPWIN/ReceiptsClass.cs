using MySql.Data.MySqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GDAPPWIN
{
    internal class ReceiptsClass
    {
        // Properties
        public int CustomerID { get; set; }
        public string? ReceiptNumber { get; set; }
        public decimal NetCash { get; set; }
        public decimal WithHoldingTax { get; set; }
        public decimal Fee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Remark { get; set; }
        public int CreatedByUser { get; set; }

        public static int AddReceiptAndGetLastID(ReceiptsClass receipts)
        {
            int lastInsertedID = -1;
            try
            {
                using (DBconnect connection = new DBconnect()) // Using DBconnect
                {
                    connection.OpenConnection();

                    // Insert into Receipts table and get the last inserted ID
                    string insertQuery = @"
                INSERT INTO Receipts (CustomerID, ReceiptNumber, NetCash, WithHoldingTax, Fee, DiscountAmount, DepositAmount, ReceiptDate, PaymentMethod, Remark, CreateByUser)
                VALUES (@CustomerID, @ReceiptNumber, @NetCash, @WithHoldingTax, @Fee, @DiscountAmount, @DepositAmount, @ReceiptDate, @PaymentMethod, @Remark, @CreateByUser);
                SELECT LAST_INSERT_ID();";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection.GetConnection);
                    cmd.Parameters.AddWithValue("@CustomerID", receipts.CustomerID);
                    cmd.Parameters.AddWithValue("@ReceiptNumber", receipts.ReceiptNumber);
                    cmd.Parameters.AddWithValue("@NetCash", receipts.NetCash);
                    cmd.Parameters.AddWithValue("@WithHoldingTax", receipts.WithHoldingTax);
                    cmd.Parameters.AddWithValue("@Fee", receipts.Fee);
                    cmd.Parameters.AddWithValue("@DiscountAmount", receipts.DiscountAmount);
                    cmd.Parameters.AddWithValue("@DepositAmount", receipts.DepositAmount);
                    cmd.Parameters.AddWithValue("@ReceiptDate", receipts.ReceiptDate);
                    cmd.Parameters.AddWithValue("@PaymentMethod", receipts.PaymentMethod);
                    cmd.Parameters.AddWithValue("@Remark", receipts.Remark);
                    cmd.Parameters.AddWithValue("@CreateByUser", receipts.CreatedByUser);

                    lastInsertedID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return lastInsertedID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in AddReceiptAndGetLastID: {ex.Message}");
                return lastInsertedID; // Return a default value or handle the error accordingly
            }
        }

        public static bool AddReceiptsRealInvoices(int receiptID, int realInvoiceID)
        {
            try
            {
                using (DBconnect connection = new DBconnect()) // ใช้งาน DBconnect
                {
                    connection.OpenConnection();

                    // Insert into receipts_realinvoices table
                    string insertQuery = "INSERT INTO receipts_realinvoices (ReceiptID, RealInvoiceID) " +
                                         "VALUES (@ReceiptID, @RealInvoiceID)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection.GetConnection);
                    cmd.Parameters.AddWithValue("@ReceiptID", receiptID);
                    cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in AddReceiptsRealInvoices: {ex.Message}");
                return false;
            }
        }
        public static int GetLastInsertedReceiptID()
        {
            try
            {
                using (DBconnect connection = new DBconnect()) // ใช้งาน DBconnect
                {
                    connection.OpenConnection();

                    // Get the last inserted ReceiptID
                    string selectQuery = "SELECT LAST_INSERT_ID()";
                    MySqlCommand cmd = new MySqlCommand(selectQuery, connection.GetConnection);
                    int lastInsertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    return lastInsertedID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetLastInsertedReceiptID: {ex.Message}");
                return -1; // Return a default value or handle the error accordingly
            }
        }
        public static DataTable GetReceiptsData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DBconnect dbconn = new DBconnect())
                {
                    // Open connection
                    dbconn.OpenConnection();

                    // Query to retrieve data from Receipts table
                    string query = "SELECT * FROM Receipts";

                    // Create DataAdapter
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbconn.GetConnection))
                    {
                        // Fill the DataTable with data from DataAdapter
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetReceiptsData: {ex.Message}");
            }

            return dataTable;
        }
        public static List<InvoiceClass> GetRealInvoicesByReceiptID(int receiptID)
        {
            List<InvoiceClass> realInvoices = new List<InvoiceClass>();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string selectQuery = "SELECT * FROM RealInvoices " +
                                         "WHERE RealInvoiceID IN (SELECT RealInvoiceID FROM receipts_realinvoices WHERE ReceiptID = @ReceiptID)";
                    MySqlCommand cmd = new MySqlCommand(selectQuery, connection.GetConnection);
                    cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // สร้าง RealInvoice จากข้อมูลที่ได้จากฐานข้อมูล
                            InvoiceClass realInvoice = new InvoiceClass
                            {
                                // กำหนดค่า properties ต่าง ๆ ของ RealInvoice จาก reader
                                RealInvoiceID = Convert.ToInt32(reader["RealInvoiceID"]),
                                CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                InvoiceDate = Convert.IsDBNull(reader["InvoiceDate"]) ? DateTime.MinValue : Convert.ToDateTime(reader["InvoiceDate"]),
                                IsPaid = Convert.IsDBNull(reader["IsPaid"]) ? false : Convert.ToBoolean(reader["IsPaid"]),
                                ExternalBillName = reader["ExternalBillName"].ToString(),
                                Details = reader["Details"].ToString(),
                                CreateByUser = Convert.ToInt32(reader["CreateByUser"]),
                                DiscountAmount = Convert.IsDBNull(reader["DiscountAmount"]) ? 0 : Convert.ToDecimal(reader["DiscountAmount"]),
                                DepositAmount = Convert.IsDBNull(reader["DepositAmount"]) ? 0 : Convert.ToDecimal(reader["DepositAmount"]),
                                // กำหนด Properties อื่น ๆ ตามตาราง
                            };

                            // เพิ่ม RealInvoice เข้า List
                            realInvoices.Add(realInvoice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetRealInvoicesByReceiptID: {ex.Message}");
            }

            return realInvoices;
        }
        public static void CalculateValuesInDatabase(int receiptID, out decimal netCash, out decimal withHoldingTax, out decimal fee, out decimal discountAmount, out decimal depositAmount)
        {
            // Initialize the output variables
            netCash = 0;
            withHoldingTax = 0;
            fee = 0;
            discountAmount = 0;
            depositAmount = 0;

            // Connect to the database and retrieve values based on the receiptID
            using (var connection = new DBconnect().GetConnection)
            {
                connection.Open();
                string selectQuery = "SELECT NetCash, WithHoldingTax, Fee, DiscountAmount, DepositAmount FROM receipts WHERE ReceiptID = @ReceiptID";
                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ReceiptID", receiptID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assign retrieved values to output variables
                            netCash = Convert.ToDecimal(reader["NetCash"]);
                            withHoldingTax = Convert.ToDecimal(reader["WithHoldingTax"]);
                            fee = Convert.ToDecimal(reader["Fee"]);
                            discountAmount = Convert.ToDecimal(reader["DiscountAmount"]);
                            depositAmount = Convert.ToDecimal(reader["DepositAmount"]);
                        }
                    }
                }
            }
        }
        public static (decimal, decimal) GetOutstandingDebtAndTotalAmount(int receiptID)
        {
            decimal totalOutstandingDebt = 0;
            decimal totalTotalAmount = 0;

            using (var connection = new DBconnect().GetConnection)
            {
                connection.Open();

                // Fetch RealInvoiceIDs associated with the given ReceiptID
                List<int> realInvoiceIDs = GetRealInvoiceIDsForReceipt(receiptID, connection);

                foreach (int realInvoiceID in realInvoiceIDs)
                {
                    // Query to fetch OutstandingDebt and TotalAmount from realinvoices table based on RealInvoiceID
                    string query = "SELECT OutstandingDebt, TotalAmount FROM realinvoices WHERE RealInvoiceID = @RealInvoiceID";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Accumulate the values for all real invoices associated with the receipt
                                totalOutstandingDebt += Convert.ToDecimal(reader["OutstandingDebt"]);
                                totalTotalAmount += Convert.ToDecimal(reader["TotalAmount"]);
                            }
                        }
                    }
                }
            }

            return (totalOutstandingDebt, totalTotalAmount);
        }

        private static List<int> GetRealInvoiceIDsForReceipt(int receiptID, MySqlConnection connection)
        {
            List<int> realInvoiceIDs = new List<int>();

            // Query to fetch RealInvoiceIDs associated with the given ReceiptID
            string query = "SELECT RealInvoiceID FROM receipts_realinvoices WHERE ReceiptID = @ReceiptID";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ReceiptID", receiptID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        realInvoiceIDs.Add(Convert.ToInt32(reader["RealInvoiceID"]));
                    }
                }
            }

            return realInvoiceIDs;
        }
    }
}
