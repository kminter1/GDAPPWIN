using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    public static class InvoiceHelper
    {
        public static int TotalInvoices { get; set; }
        public static decimal TotalAmount { get; set; }
        public static decimal CreditPaidAmount { get; set; }
        #region Merge
        public static DataTable LoadTemporaryInvoices()
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();
                    string query = "SELECT ti.TemporaryInvoiceID, c.Cname, ti.IssueDate, ti.Ornc_No, ti.TotalAmount FROM temporaryinvoices ti " +
                           "JOIN customers c ON ti.CustomerID = c.CustomerID";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbConnect.GetConnection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
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

        public static DataTable LoadAllRealInvoices()
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT ri.RealInvoiceID, c.Cname AS CustomerName, ri.ExternalBillName FROM realinvoices ri JOIN customers c ON ri.CustomerID = c.CustomerID", connect.GetConnection))
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
        // Add other shared methods here...
        public static DataTable LoadTemporaryInvoicesByTerm(string searchTerm)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();
                    string query = "SELECT ti.TemporaryInvoiceID, c.Cname, ti.IssueDate, ti.Ornc_No, ti.TotalAmount FROM temporaryinvoices ti " +
                           "JOIN customers c ON ti.CustomerID = c.CustomerID WHERE ti.Ornc_No LIKE @searchTerm";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, dbConnect.GetConnection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
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

        public static Dictionary<string, string> GetTransactionTypeDict()
        {
            return new Dictionary<string, string>
        {
            { "Deposit", "Deposit" },
            { "Withdrawal", "Withdrawal" },
            { "Credit", "Credit" }
        };
        }

        public static void PopulateComboBoxType(ComboBox comboBox)
        {
            comboBox.DataSource = new BindingSource(GetTransactionTypeDict(), null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
        }

        public static void SetComboBoxType(ComboBox comboBox, string transactionType)
        {
            var transactionTypeDict = GetTransactionTypeDict();
            foreach (var pair in transactionTypeDict)
            {
                if (pair.Value == transactionType)
                {
                    comboBox.SelectedValue = pair.Key;
                    break;
                }
            }
        }

        public static void UpdateTextBoxesFromDataGridView(DataGridView dataGridView, TextBox txtOrncExtBillName, TextBox txtOrncID, TextBox txtAmount)
        {
            if (dataGridView.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridView.CurrentRow;

                // Assuming these columns exist in your DataGridView
                string orncBillName = selectedRow.Cells["Ornc_No"].Value.ToString();
                string temporaryInvoiceID = selectedRow.Cells["TemporaryInvoiceID"].Value.ToString();
                decimal totalAmount = Convert.ToDecimal(selectedRow.Cells["TotalAmount"].Value);

                // Set values to TextBoxes
                txtOrncExtBillName.Text = orncBillName;
                txtOrncID.Text = temporaryInvoiceID;
                txtAmount.Text = totalAmount.ToString();
            }
        }

        public static decimal GetTotalAmountForTemporaryInvoice(int temporaryInvoiceID)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    connect.OpenConnection();

                    // Implement the logic to calculate the total amount for a specific temporary invoice
                    // For example, you can use a query to get the TotalAmount from the temporaryinvoices table
                    string query = "SELECT TotalAmount FROM temporaryinvoices WHERE TemporaryInvoiceID = @TemporaryInvoiceID";
                    using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);

                        // Execute the query and return the result
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToDecimal(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    connect.CloseConnection();
                }

                return 0; // Return 0 if an error occurs
            }
        }
        #endregion Merge

        public static Dictionary<int, decimal> GetTotalAmountByCustomer()
        {
            Dictionary<int, decimal> totalAmountByCustomer = new Dictionary<int, decimal>();

            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    connect.OpenConnection();

                    string query = "SELECT CustomerID, SUM(TotalAmount) AS TotalAmount FROM temporaryinvoices GROUP BY CustomerID";
                    using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int customerID = Convert.ToInt32(reader["CustomerID"]);
                                decimal totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                totalAmountByCustomer.Add(customerID, totalAmount);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    connect.CloseConnection();
                }
            }

            return totalAmountByCustomer;
        }

        public static string GetCustomerNameByID(int customerID)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    connect.OpenConnection();

                    string query = "SELECT Cname FROM customers WHERE CustomerID = @CustomerID";

                    using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return string.Empty;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }

        public static Dictionary<int, decimal> GetTotalAmountByCustomerWithNames()
        {
            Dictionary<int, decimal> totalAmountByCustomer = new Dictionary<int, decimal>();

            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    connect.OpenConnection();

                    string query = "SELECT c.CustomerID, c.Cname, SUM(ti.TotalAmount) AS TotalAmount " +
                                   "FROM customers c " +
                                   "RIGHT JOIN temporaryinvoices ti ON c.CustomerID = ti.CustomerID " +
                                   "GROUP BY c.CustomerID, c.Cname";

                    using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int customerID = Convert.ToInt32(reader["CustomerID"]);
                                decimal totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                totalAmountByCustomer.Add(customerID, totalAmount);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    connect.CloseConnection();
                }
            }

            return totalAmountByCustomer;
        }

        public static DataTable LoadReportRealInvoices()
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    connect.OpenConnection();
                    string query = "SELECT ri.RealInvoiceID, c.Cname AS CustomerName, ri.InvoiceDate, ri.TotalAmount, ri.IsPaid, ri.ExternalBillName, ri.Details " +
                                   "FROM realinvoices ri " +
                                   "JOIN customers c ON ri.CustomerID = c.CustomerID";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connect.GetConnection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return null;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }

        public static DataTable LoadAllRealInvoicesWithTemporaryInvoices()
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT ri.RealInvoiceID, c.Cname AS CustomerName, ri.ExternalBillName, ti.Ornc_No, ti.TotalAmount " +
                                                                   "FROM realinvoices ri " +
                                                                   "JOIN customers c ON ri.CustomerID = c.CustomerID " +
                                                                   "LEFT JOIN realinvoices_temporaryinvoices rti ON ri.RealInvoiceID = rti.RealInvoiceID " +
                                                                   "LEFT JOIN temporaryinvoices ti ON rti.TemporaryInvoiceID = ti.TemporaryInvoiceID", connect.GetConnection))
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
                finally
                {
                    connect.CloseConnection();
                }
                return null;
            }
        }

        public static DataTable SearchRealInvoicesWithTemporaryInvoices(string searchTerm)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT ri.RealInvoiceID, c.Cname AS CustomerName, ri.ExternalBillName, ti.Ornc_No, ti.TotalAmount " +
                                                                   "FROM realinvoices ri " +
                                                                   "JOIN customers c ON ri.CustomerID = c.CustomerID " +
                                                                   "LEFT JOIN realinvoices_temporaryinvoices rti ON ri.RealInvoiceID = rti.RealInvoiceID " +
                                                                   "LEFT JOIN temporaryinvoices ti ON rti.TemporaryInvoiceID = ti.TemporaryInvoiceID " +
                                                                   "WHERE ri.ExternalBillName LIKE @searchTerm", connect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

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
                finally
                {
                    connect.CloseConnection();
                }
                return null;
            }
        }

        public static DataTable GetCustomerByNameOrCode(string searchName)
        {
            using (DBconnect conn = new DBconnect())
            {
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM `customers` WHERE Cname LIKE @searchName OR Ccode LIKE @searchName", conn.GetConnection))
                {
                    command.Parameters.Add("@searchName", MySqlDbType.VarChar).Value = $"%{searchName}%";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }




    }
}
