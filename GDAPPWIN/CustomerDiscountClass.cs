using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class CustomerDiscountClass : IDisposable
    {
        public static bool AddDiscount(int customerID, string discountName, decimal discountAmount, DateTime discountDate, int createbyUser)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    connect.OpenConnection();

                    string query = "INSERT INTO discounts (CustomerID, DiscountName, DiscountAmount, CreateDate, CreateByUser) VALUES (@CustomerID, @DiscountName, @DiscountAmount, @CreateDate, @createByUser)";
                    using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@DiscountName", discountName);
                        command.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                        command.Parameters.AddWithValue("@CreateDate", discountDate);
                        command.Parameters.AddWithValue("@CreateByUser", createbyUser);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }
        public static DataTable GetDiscountReport()
        {
            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = "SELECT d.DiscountID, d.DiscountName, d.DiscountAmount, c.Cname AS CustomerName " +
                           "FROM discounts d " +
                           "INNER JOIN customers c ON d.CustomerID = c.CustomerID";

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable discountTable = new DataTable();
                    adapter.Fill(discountTable);

                    return discountTable;
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public static DataTable GetDiscountByTerm(string searchTerm)
        {
            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = "SELECT d.DiscountID, d.DiscountName, d.DiscountAmount, c.Cname AS CustomerName " +
                                   "FROM discounts d " +
                                   "INNER JOIN customers c ON d.CustomerID = c.CustomerID " +
                                   "WHERE d.DiscountID = @SearchTerm OR d.DiscountName LIKE CONCAT('%', @SearchTerm, '%')";

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable discountTable = new DataTable();
                    adapter.Fill(discountTable);

                    return discountTable;
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public static DataTable GetTemporaryInvoicesByTerm(string searchTerm)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();
                    string query = "SELECT ti.TemporaryInvoiceID, c.Cname, ti.IssueDate, ti.Ornc_No, ti.TotalAmount FROM temporaryinvoices ti " +
                                   "JOIN customers c ON ti.CustomerID = c.CustomerID " +
                                   "WHERE ti.Ornc_No LIKE @searchTerm";
                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return null;
                }
                finally
                {
                    dbConnect.CloseConnection();
                }
            }
        }
        public static DataTable LoadAllCustomerDiscount()
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT customerdiscount.DiscountID, customers.Cname, customerdiscount.DiscountAmount, customerdiscount.DiscountDate, customerdiscount.RealInvoiceID, realinvoices.ExternalBillName FROM customerdiscount INNER JOIN customers ON customerdiscount.CustomerID = customers.CustomerID LEFT JOIN realinvoices ON customerdiscount.RealInvoiceID = realinvoices.RealInvoiceID", connect.GetConnection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally { connect.CloseConnection(); }
                return null;
            }
        }
        public static DataTable LoadTransactionDiscountData()
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT cd.DiscountID, c.Cname AS CustomerName, cd.DiscountAmount, cd.DiscountDate, r.ExternalBillName, c2.Cname FROM customerdiscount_realinvoices cdr INNER JOIN customerdiscount cd ON cdr.DiscountID = cd.DiscountID INNER JOIN customers c ON cd.CustomerID = c.CustomerID INNER JOIN realinvoices r ON cdr.RealInvoiceID = r.RealInvoiceID INNER JOIN customers c2 ON r.CustomerID = c2.CustomerID", connect.GetConnection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally { connect.CloseConnection(); }
                return null;
            }
        }

        public static bool AddCustomerDiscountRealInvoices(int discountID, int realInvoiceID)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO customerdiscount_realinvoices (DiscountID, RealInvoiceID) VALUES (@discountID, @realInvoiceID)", connect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@discountID", discountID);
                        command.Parameters.AddWithValue("@realInvoiceID", realInvoiceID);

                        connect.OpenConnection();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
                finally { connect.CloseConnection(); }
            }
        }

        public static bool AddTransactionDiscount(int discountID, decimal discountAmount, DateTime discountDate)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO transactiondiscount (DiscountID, Amount, DiscountDate) VALUES (@discountID, @amount, @transactionDate)", connect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@discountID", discountID);
                        command.Parameters.AddWithValue("@amount", discountAmount);
                        command.Parameters.AddWithValue("@transactionDate", discountDate);

                        connect.OpenConnection();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
                finally { connect.CloseConnection(); }
            }
        }

        public static bool AddTransaction(int customerID, int discountID, decimal amountUsed, DateTime transactionDate, int temporaryInvoiceID, int realInvoiceID)
        {
            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = @"INSERT INTO customerdiscounttransactions (CustomerID, RealInvoiceID, TemporaryInvoiceID, DiscountID, AmountUsed, TransactionDate)
                                     VALUES (@CustomerID, @RealInvoiceID, @TemporaryInvoiceID, @DiscountID, @AmountUsed, @TransactionDate)";

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);
                    cmd.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);
                    cmd.Parameters.AddWithValue("@DiscountID", discountID);
                    cmd.Parameters.AddWithValue("@AmountUsed", amountUsed);
                    cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                if (disposing)
                {
                    // ปิดการเชื่อมต่อกับฐานข้อมูล (หากมี)
                    if (dbConnect != null)
                    {
                        dbConnect.CloseConnection();
                        dbConnect.Dispose();
                    }
                }
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CustomerDiscountClass()
        {
            Dispose(false);
        }
    }
}
