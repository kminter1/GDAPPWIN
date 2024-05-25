using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
namespace GDAPPWIN
{
    internal class InvoiceClass
    {
        public List<string> InvoicesDecimalFormat { get; set; }
        public Dictionary<string,string> InvoicesHeaderFormat { get; set; }
        public int RealInvoiceID { get; set; }
        public int CreateByUser { get; set; }
        public int CustomerID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal OutstandingDebt { get; set; }
        public bool IsPaid { get; set; }
        public string? Details { get; set; }
        public string? ExternalBillName { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? CustomerName { get; set; }
        public InvoiceClass()
        {
            InvoicesDecimalFormat = new List<string> { "TotalAmount", "DiscountAmount", "TotalDiscountUsed", "DepositAmount", "TotalDepositUsed", "AmountToPay" };
            InvoicesHeaderFormat = new Dictionary<string, string> {
            { "RealInvoiceID", "ID (ORVC)" },
            { "CustomerID", "ID (ลูกค้า)" },
            { "Ccode", "รหัสลูกค้า" },
            { "InvoiceDate", "วันที่ออก" },
            { "CustomerName", "ชื่อลูกค้า" },
            { "TotalAmount", "จำนวนเงิน" },
            { "Details", "รายละเอียด" },
            { "ExternalBillName", "เลขที่(ORVC)" },
            { "createdByName", "พนักงาน" },
            { "DiscountAmount", "ส่วนลด" },
            { "TotalDiscountUsed", "จ่ายส่วนลดแล้ว" },
            { "DiscountTransactionDate", "วันที่จ่ายส่วนลด" },
            { "DiscountStatus", "สถานะส่วนลด" },
            { "DepositAmount", "จำนวนเงินฝาก" },
            { "TotalDepositUsed", "ยอดรวม(เงินฝาก)" },
            { "AmountToPay", "ยอดที่ต้องจ่าย(20%)" },
            { "DepositTransactionDate", "วันที่จ่ายเงินฝาก" },
            { "DepositStatus", "สถานะเงินฝาก" },
            { "OutstandingDebt", "ยอดหนี้คงค้าง" }
            };
        }
        public static int InsertRealInvoiceToDatabase(InvoiceClass invoice)
        {
            int insertedID = -1; // Store the generated RealInvoiceID

            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();

                    string query = @"
            INSERT INTO realinvoices 
            (CustomerID, InvoiceDate, TotalAmount, ExternalBillName, Details, CreateByUser, DiscountAmount, DepositAmount, OutstandingDebt) 
            VALUES 
            (@CustomerID, @InvoiceDate, @TotalAmount, @ExternalBillName, @Details, @CreateByUser, @DiscountAmount, @DepositAmount, @OutstandingDebt);
            SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", invoice.CustomerID);
                        command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                        command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                        command.Parameters.AddWithValue("@DepositAmount", invoice.DepositAmount);
                        command.Parameters.AddWithValue("@ExternalBillName", invoice.ExternalBillName);
                        command.Parameters.AddWithValue("@Details", invoice.Details);
                        command.Parameters.AddWithValue("@CreateByUser", invoice.CreateByUser);
                        command.Parameters.AddWithValue("@DiscountAmount", invoice.DiscountAmount);
                        command.Parameters.AddWithValue("@OutstandingDebt", invoice.OutstandingDebt);

                        insertedID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    // Log the error for troubleshooting
                }
            }

            return insertedID;
        }
        public static void SaveDataToTransactionTables(List<int> selectedTemporaryInvoiceIDs)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();
                    using (MySqlTransaction transaction = dbConnect.GetConnection.BeginTransaction())
                    {
                        try
                        {
                            foreach (int temporaryInvoiceID in selectedTemporaryInvoiceIDs)
                            {
                                InvoiceClass currentInvoice = new InvoiceClass();
                                currentInvoice.RealInvoiceID = 0; // ตั้งค่า InvoiceID เป็น 0 เพื่อให้ Insert เมื่อ Save

                                // ดึงข้อมูล TemporaryInvoice ที่ถูกเลือก
                                string selectTemporaryInvoiceQuery = "SELECT CustomerID, InvoiceDate, TotalAmount FROM temporaryinvoices WHERE TemporaryInvoiceID = @TemporaryInvoiceID";
                                using (MySqlCommand selectCommand = new MySqlCommand(selectTemporaryInvoiceQuery, dbConnect.GetConnection, transaction))
                                {
                                    selectCommand.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);

                                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            // กำหนดค่าให้กับ currentInvoice
                                            currentInvoice.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                                            currentInvoice.InvoiceDate = Convert.ToDateTime(reader["IssueDate"]);
                                            currentInvoice.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                            currentInvoice.IsPaid = false; // ตั้งค่า IsPaid เป็น false (ให้ปรับตามความเหมาะสม)

                                            // บันทึกข้อมูลลงใน Transaction Tables (ตัวอย่างเท่านั้น)
                                            InsertRealInvoiceToDatabase(currentInvoice);
                                        }
                                    }
                                }
                            }

                            // Commit การทำ Transaction เมื่อบันทึกสำเร็จ
                            transaction.Commit();
                            MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว");
                        }
                        catch (Exception ex)
                        {
                            // Rollback การทำ Transaction ในกรณีเกิดข้อผิดพลาด
                            transaction.Rollback();
                            MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    dbConnect.CloseConnection();
                }
            }
        }
        public static DataTable GetRealList()
        {
            using (DBconnect dbcon = new DBconnect())
            {
                try
                {
                    dbcon.OpenConnection();
                    string sql = "SELECT ri.RealInvoiceID, c.CName, ri.InvoiceDate, ri.Details, ri.ExternalBillName, ri.TotalAmount, ri.IsPaid " +
                           "FROM realinvoices ri " +
                           "JOIN customers c ON ri.CustomerID = c.CustomerID";
                    using (MySqlDataAdapter dt = new MySqlDataAdapter(sql, dbcon.GetConnection))
                    {
                        DataTable dataTable = new DataTable();
                        dt.Fill(dataTable);
                        return dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return null;
                }
                finally { dbcon.CloseConnection(); }
            }
        }
        public static decimal GetTotalDepositAmount()
        {
            decimal totalDepositAmount = 0;

            using (DBconnect conn = new DBconnect())
            {
                try
                {
                    conn.OpenConnection();

                    string query = "SELECT SUM(DepositAmount) AS TotalDepositAmount FROM realinvoices";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);

                    // ดึงผลรวมจากฐานข้อมูล
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalDepositAmount = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    // การจัดการข้อผิดพลาด
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return totalDepositAmount;
        }
        public static bool IsExternalBillNameExists(string orvcbillName)
        {
            string query = "SELECT COUNT(*) FROM realinvoices WHERE ExternalBillName = @externalBillName";

            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();

                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@externalBillName", orvcbillName);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking ExternalBillName existence: {ex.Message}");
                    return false;
                }
            }
        }
        public static bool IsPaymentMade(int realInvoiceID)
        {
            bool paymentMade = false;

            using (DBconnect dbConnect = new DBconnect())
            {
                string query = @"
            SELECT 
                COUNT(*)
            FROM 
                receipts_realinvoices ri
            LEFT JOIN 
                deposits_receipts dr ON ri.ReceiptID = dr.ReceiptID
            LEFT JOIN 
                discounts_receipts dis ON ri.ReceiptID = dis.ReceiptID
            WHERE 
                ri.RealInvoiceID = @RealInvoiceID AND
                (dr.DepositAmount > 0 OR dis.DiscountAmount > 0);";

                try
                {
                    dbConnect.OpenConnection();

                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                        int paymentCount = Convert.ToInt32(command.ExecuteScalar());

                        if (paymentCount > 0)
                        {
                            paymentMade = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return paymentMade;
        }

        public void VoidInvoice(int invoiceID)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                string query = "UPDATE realinvoices SET IsVoided = 1 WHERE RealInvoiceID = @InvoiceID";

                try
                {
                    if (dbConnect.OpenConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                            cmd.ExecuteNonQuery();
                        }

                        dbConnect.CloseConnection();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static bool IsVoidedMade(int realInvoiceID)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                string query = "SELECT COUNT(*) FROM realinvoices WHERE RealInvoiceID = @RealInvoiceID AND IsVoided = 1";

                try
                {
                    if (dbConnect.OpenConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);
                            int voidedCount = Convert.ToInt32(cmd.ExecuteScalar());
                            dbConnect.CloseConnection();

                            // If the count is greater than 0, it means the invoice is voided
                            return voidedCount > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return false;
            }
        }




    }
}
