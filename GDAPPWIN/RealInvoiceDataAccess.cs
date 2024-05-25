using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class RealInvoiceDataAccess
    {
        public static DataTable LoadRealInvoices()
        {
            DataTable realInvoices = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"SELECT ri.RealInvoiceID, c.Cname AS CustomerName, ri.InvoiceDate, ri.TotalAmount, ri.DepositAmount, 
                                        ri.IsPaid, ri.ExternalBillName, ri.Details, ri.CreateByUser, ri.DiscountAmount         
                                 FROM realinvoices ri
                                 INNER JOIN customers c ON ri.CustomerID = c.CustomerID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(realInvoices);
                }

                conn.CloseConnection();
            }

            return realInvoices;
        }
        public static DataTable SearchRealInvoices(string searchTerm)
        {
            DataTable searchResult = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"SELECT ri.RealInvoiceID, c.Cname AS CustomerName, ri.InvoiceDate, ri.TotalAmount, ri.DepositAmount, 
                                ri.IsPaid, ri.ExternalBillName, ri.Details, ri.CreateByUser, ri.DiscountAmount         
                         FROM realinvoices ri
                         INNER JOIN customers c ON ri.CustomerID = c.CustomerID
                         WHERE c.Cname LIKE @SearchTerm OR ri.ExternalBillName LIKE @SearchTerm";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(searchResult);
                }

                conn.CloseConnection();
            }

            return searchResult;
        }
        public static DataTable SearchRealInvoicesWithStatus(string searchTerm)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                SELECT 
                RI.RealInvoiceID,
                RI.CustomerID,
                C.Ccode,
                C.Cname AS CustomerName,
                RI.InvoiceDate,
                RI.TotalAmount,
                RI.ExternalBillName,
                RI.Details,
                U.Name AS CreatedByName, -- Include the user's name
                COALESCE(RI.DiscountAmount, 0) AS DiscountAmount, 
                SUM(DR.DiscountAmount) AS TotalDiscountUsed,
                DR.TransactionDate AS DiscountTransactionDate,
                CASE
                    WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= RI.DiscountAmount THEN 'Fully Paid'
                    WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                    ELSE 'Not Paid'
                END AS DiscountStatus,
                COALESCE(RI.DepositAmount, 0) AS DepositAmount, 
                SUM(DepS.DepositAmount) AS TotalDepositUsed,
                SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
                DepS.TransactionDate AS DepositTransactionDate,
                CASE
                    WHEN SUM(DepS.DepositAmount) >= RI.DepositAmount THEN 'Fully Paid'
                    WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                    ELSE 'Not Paid'
                END AS DepositStatus,
                CASE
                    WHEN RI.IsVoided = 1 THEN 'Voided'
                    ELSE 'Normal'
                END AS VoidStatus,
                R.ReceiptNumber,
                R.CreatedDate,
                COALESCE(R.NetCash, 0) + COALESCE(R.WithHoldingTax, 0) + COALESCE(R.Fee, 0) AS TotalPayment, -- Calculate TotalPayment
                RI.OutstandingDebt
            FROM 
                realinvoices RI
            LEFT JOIN 
                receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
            LEFT JOIN 
                receipts R ON RR.ReceiptID = R.ReceiptID -- Join receipts table to get ReceiptNumber and payment details
            LEFT JOIN 
                discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
            LEFT JOIN 
                deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
            LEFT JOIN
                customers C ON RI.CustomerID = C.CustomerID
            LEFT JOIN
                users U ON RI.createByUser = U.UserID -- Join users table to get the user's name
            WHERE 
                C.Ccode LIKE @SearchTerm OR 
                C.Cname LIKE @SearchTerm OR 
                RI.ExternalBillName LIKE @SearchTerm OR 
                RI.Details LIKE @SearchTerm
            GROUP BY 
                RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.Details, R.CreatedDate, RI.DiscountAmount, RI.DepositAmount, U.Name, RI.IsVoided, R.ReceiptNumber, R.NetCash, R.WithHoldingTax, R.Fee, RI.OutstandingDebt;
            ";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable realInvoicesWithStatus = new DataTable();
                    adapter.Fill(realInvoicesWithStatus);

                    conn.CloseConnection();
                    return realInvoicesWithStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal GetTotalDiscountAmount(int realInvoiceID)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = "SELECT COALESCE(SUM(DiscountAmount), 0) FROM discounts_realinvoices WHERE RealInvoiceID = @RealInvoiceID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                object result = cmd.ExecuteScalar();
                conn.CloseConnection();

                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    return 0;
                }
            }
        }
        public static int AddRealInvoiceDiscount(int realInvoiceID, int discountID, decimal discountAmount, int customerID, int createdByUser)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"INSERT INTO discounts_realinvoices (RealInvoiceID, DiscountID, DiscountAmount, TransactionDate, CustomerID, CreatedByUser) 
                        VALUES (@RealInvoiceID, @DiscountID, @DiscountAmount, @TransactionDate, @CustomerID, @CreatedByUser);";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);
                cmd.Parameters.AddWithValue("@DiscountID", discountID);
                cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                cmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@CreatedByUser", createdByUser);

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.CloseConnection();
                return rowsAffected > 0 ? 1 : -1;
            }
        }
        public static DataTable LoadRealInvoicesWithStatus()
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                SELECT 
                RI.RealInvoiceID,
                RI.CustomerID,
                C.Ccode,
                C.Cname AS CustomerName,
                RI.InvoiceDate,
                RI.TotalAmount,
                RI.ExternalBillName,
                RI.Details,
                U.Name AS CreatedByName, -- Include the user's name
                COALESCE(RI.DiscountAmount, 0) AS DiscountAmount, 
                SUM(DR.DiscountAmount) AS TotalDiscountUsed,
                DR.TransactionDate AS DiscountTransactionDate,
                CASE
                    WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= RI.DiscountAmount THEN 'Fully Paid'
                    WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                    ELSE 'Not Paid'
                END AS DiscountStatus,
                COALESCE(RI.DepositAmount, 0) AS DepositAmount, 
                SUM(DepS.DepositAmount) AS TotalDepositUsed,
                SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
                DepS.TransactionDate AS DepositTransactionDate,
                CASE
                    WHEN SUM(DepS.DepositAmount) >= RI.DepositAmount THEN 'Fully Paid'
                    WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                    ELSE 'Not Paid'
                END AS DepositStatus,
                CASE
                    WHEN RI.IsVoided = 1 THEN 'Voided'
                    ELSE 'Normal'
                END AS VoidStatus,
                R.ReceiptNumber,
                R.CreatedDate,
                COALESCE(R.NetCash, 0) + COALESCE(R.WithHoldingTax, 0) + COALESCE(R.Fee, 0) AS TotalPayment, -- Calculate TotalPayment
                RI.OutstandingDebt
            FROM 
                realinvoices RI
            LEFT JOIN 
                receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
            LEFT JOIN 
                receipts R ON RR.ReceiptID = R.ReceiptID -- Join receipts table to get ReceiptNumber and payment details
            LEFT JOIN 
                discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
            LEFT JOIN 
                deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
            LEFT JOIN
                customers C ON RI.CustomerID = C.CustomerID
            LEFT JOIN
                users U ON RI.createByUser = U.UserID -- Join users table to get the user's name
            GROUP BY 
                RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.Details, R.CreatedDate, RI.DiscountAmount, RI.DepositAmount, U.Name, RI.IsVoided, R.ReceiptNumber, R.NetCash, R.WithHoldingTax, R.Fee, RI.OutstandingDebt;
            ";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable realInvoicesWithStatus = new DataTable();
                    adapter.Fill(realInvoicesWithStatus);

                    conn.CloseConnection();
                    return realInvoicesWithStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Method to update a RealInvoice in the database
        public static bool UpdateRealInvoice(InvoiceClass invoice)
        {
            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    // Open the database connection
                    if (dbConnect.OpenConnection())
                    {
                        // SQL query to update the RealInvoice
                        string query = "UPDATE RealInvoices SET InvoiceDate = @InvoiceDate, TotalAmount = @TotalAmount, ExternalBillName = @ExternalBillName, Details = @Details, DiscountAmount = @DiscountAmount, DepositAmount = @DepositAmount WHERE RealInvoiceID = @RealInvoiceID";

                        // Create a MySqlCommand object
                        using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            // Set the parameters
                            command.Parameters.AddWithValue("@ExternalBillName", invoice.ExternalBillName);
                            command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                            command.Parameters.AddWithValue("@DiscountAmount", invoice.DiscountAmount);
                            command.Parameters.AddWithValue("@DepositAmount", invoice.DepositAmount);
                            command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                            command.Parameters.AddWithValue("@Details", invoice.Details);
                            command.Parameters.AddWithValue("@RealInvoiceID", invoice.RealInvoiceID);

                            // Execute the update query
                            int rowsAffected = command.ExecuteNonQuery();

                            // Close the database connection
                            dbConnect.CloseConnection();

                            // If rows affected is greater than zero, the update was successful
                            return rowsAffected > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return false to indicate failure
                Console.WriteLine("Error updating RealInvoice: " + ex.Message);
            }

            return false;
        }
        public static InvoiceClass GetRealInvoiceByID(int realInvoiceID)
        {
            InvoiceClass realInvoice = null;

            // SQL query to retrieve RealInvoice data by ID
            string query = "SELECT * FROM RealInvoices WHERE RealInvoiceID = @RealInvoiceID";

            // Create a MySqlConnection object using the DBconnect class
            using (MySqlConnection connection = new DBconnect().GetConnection)
            {
                try
                {
                    // Open the database connection
                    connection.Open();

                    // Create a MySqlCommand object with the SQL query and connection
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Add parameter for RealInvoiceID to prevent SQL injection
                        cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                        // Execute the command and read the result
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if data exists
                            if (reader.Read())
                            {
                                // Create a new RealInvoice object and populate its properties from the reader
                                realInvoice = new InvoiceClass
                                {
                                    RealInvoiceID = reader.GetInt32("RealInvoiceID"),
                                    ExternalBillName = reader.GetString("ExternalBillName"),
                                    TotalAmount = reader.GetDecimal("TotalAmount"),
                                    DiscountAmount = reader.GetDecimal("DiscountAmount"),
                                    DepositAmount = reader.GetDecimal("DepositAmount"),
                                    InvoiceDate = reader.GetDateTime("InvoiceDate"),
                                    Details = reader.GetString("Details"),
                                    CustomerID = reader.GetInt32("CustomerID")
                                };
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error executing SQL query: " + ex.Message);
                }
            }

            return realInvoice;
        }
        // Method to search real invoices based on the search text
        public List<InvoiceClass> SearchRealInvoice(string searchText)
        {
            List<InvoiceClass> result = new List<InvoiceClass>();

            string query = "SELECT * FROM realinvoices WHERE ExternalBillName LIKE @searchText OR Details LIKE @searchText";
            using (MySqlConnection connection = new DBconnect().GetConnection)
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InvoiceClass invoice = new InvoiceClass
                                {
                                    RealInvoiceID = Convert.ToInt32(reader["RealInvoiceID"]),
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                    DepositAmount = Convert.ToDecimal(reader["DepositAmount"]),
                                    InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]),
                                    IsPaid = Convert.ToBoolean(reader["IsPaid"]),
                                    ExternalBillName = reader["ExternalBillName"].ToString(),
                                    Details = reader["Details"].ToString()
                                };
                                result.Add(invoice);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error searching real invoices: {ex.Message}");
                    }
                }
            }

            return result;
        }
        public static (DataTable notPaidRealInvoices, decimal outstandingDebtSum, decimal discountAmountSum, decimal depositAmountSum) GetNotPaidRealInvoices(string searchTerm, DateTime? startDate = null, DateTime? endDate = null)
        {
            DataTable notPaidRealInvoices = new DataTable();
            decimal outstandingDebtSum = 0;
            decimal discountAmountSum = 0;
            decimal depositAmountSum = 0;

            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
        SELECT DISTINCT
            RI.RealInvoiceID,
            RI.CustomerID,
            RTI.TemporaryInvoiceID,
            C.Ccode,
            C.Cname AS CustomerName,
            RI.InvoiceDate,
            COALESCE(RI.TotalAmount, 0) AS RTotalAmount,
            RI.ExternalBillName,
            RI.Details,
            R.CreatedDate,
            COALESCE(RI.DiscountAmount, 0) AS DiscountAmount, 
            COALESCE(SUM(DR.DiscountAmount), 0) AS TotalDiscountUsed,
            COALESCE(RI.DepositAmount, 0) AS DepositAmount, 
            COALESCE(SUM(DepS.DepositAmount), 0) AS TotalDepositUsed,
            CASE
                WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= RI.DiscountAmount THEN 'Fully Paid'
                WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                ELSE 'Not Paid'
            END AS DiscountStatus,
            CASE
                WHEN COALESCE(SUM(DepS.DepositAmount), 0) >= RI.DepositAmount THEN 'Fully Paid'
                WHEN COALESCE(SUM(DepS.DepositAmount), 0) > 0 THEN 'Partially Paid'
                ELSE 'Not Paid'
            END AS DepositStatus,
            CASE
                WHEN RI.IsVoided = 1 THEN 'Voided'
                ELSE 'Normal'
            END AS VoidStatus,
            R.ReceiptNumber,
            COALESCE(R.NetCash, 0) + COALESCE(R.WithHoldingTax, 0) + COALESCE(R.Fee, 0) AS TotalPayment,
            RI.OutstandingDebt
        FROM 
            realinvoices RI
        LEFT JOIN 
            realinvoices_temporaryinvoices RTI ON RI.RealInvoiceID = RTI.RealInvoiceID
        LEFT JOIN 
            receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
        LEFT JOIN 
            receipts R ON RR.ReceiptID = R.ReceiptID
        LEFT JOIN 
            discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
        LEFT JOIN 
            deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
        LEFT JOIN
            customers C ON RI.CustomerID = C.CustomerID
        WHERE 
            (C.Ccode LIKE @SearchTerm OR 
            C.Cname LIKE @SearchTerm OR 
            RI.ExternalBillName LIKE @SearchTerm OR 
            RI.Details LIKE @SearchTerm)
            AND RI.OutstandingDebt > 0
            AND RI.IsVoided = 0
    ";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND RI.InvoiceDate BETWEEN @StartDate AND @EndDate";
                    }

                    query += @"
        GROUP BY 
            RI.RealInvoiceID, RI.CustomerID, RTI.TemporaryInvoiceID, C.Ccode, C.Cname, RI.InvoiceDate, 
            RI.TotalAmount, RI.ExternalBillName, RI.Details, R.CreatedDate, RI.DiscountAmount, RI.DepositAmount, 
            RI.IsVoided, R.ReceiptNumber, R.NetCash, R.WithHoldingTax, R.Fee, RI.OutstandingDebt
        HAVING 
            (COALESCE(SUM(DR.DiscountAmount), 0) < RI.DiscountAmount 
            OR COALESCE(SUM(DepS.DepositAmount), 0) < RI.DepositAmount)
    ";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(notPaidRealInvoices);

                    // Calculate sums
                    foreach (DataRow row in notPaidRealInvoices.Rows)
                    {
                        outstandingDebtSum += row.Field<decimal>("OutstandingDebt");
                        discountAmountSum += row.Field<decimal>("DiscountAmount") - row.Field<decimal>("TotalDiscountUsed");
                        depositAmountSum += row.Field<decimal>("DepositAmount") - row.Field<decimal>("TotalDepositUsed");
                    }

                    conn.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (notPaidRealInvoices, outstandingDebtSum, discountAmountSum, depositAmountSum);
        }

        public static bool UpdateOutstandingDebt(int receiptID)
        {
            try
            {
                using (var connection = new DBconnect().GetConnection)
                {
                    connection.Open();
                    string updateQuery = @"
                UPDATE realinvoices 
                SET OutstandingDebt = GREATEST(0, OutstandingDebt - (SELECT NetCash + WithHoldingTax + Fee 
                                                              FROM receipts 
                                                              INNER JOIN receipts_realinvoices ON receipts.ReceiptID = receipts_realinvoices.ReceiptID
                                                              WHERE receipts.ReceiptID = @ReceiptID)) 
                WHERE RealInvoiceID IN (SELECT RealInvoiceID FROM receipts_realinvoices WHERE ReceiptID = @ReceiptID)";

                    using (var command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptID", receiptID);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
