using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    public static class CustomerHelper
    {
        public static DataTable SearchCustomers(string searchTerm)
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, Ccode, Cname FROM customers WHERE Ccode LIKE @searchTerm OR Cname LIKE @searchTerm", connect.GetConnection))
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
                    return null;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }

        public static DataTable GetCustomerList()
        {
            using (DBconnect connect = new DBconnect())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM `customers` LIMIT 50", connect.GetConnection))
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
                    return null;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }
        // เมทอดสำหรับดึงข้อมูลลูกค้าและยอดเงินทั้งหมดของแต่ละลูกค้า
        public static Dictionary<int, decimal> GetTotalAmountByCustomer()
        {
            Dictionary<int, decimal> totalAmountByCustomer = new Dictionary<int, decimal>();

            // เชื่อมต่อกับฐานข้อมูล MySQL
            using (var connection = new MySqlConnection("connection_string_here"))
            {
                // เปิดการเชื่อมต่อ
                connection.Open();

                // สร้างคำสั่ง SQL สำหรับการค้นหายอดเงินทั้งหมดของแต่ละลูกค้า
                string query = "SELECT CustomerID, SUM(TotalAmount) AS TotalAmount FROM Invoices GROUP BY CustomerID";

                // สร้างและประมวลผลคำสั่ง SQL
                using (var command = new MySqlCommand(query, connection))
                {
                    // อ่านข้อมูลผลลัพธ์
                    using (var reader = command.ExecuteReader())
                    {
                        // วนลูปผลลัพธ์เพื่อนับยอดเงินของแต่ละลูกค้า
                        while (reader.Read())
                        {
                            int customerID = reader.GetInt32(0);
                            decimal totalAmount = reader.GetDecimal(1);

                            // เพิ่มข้อมูลลูกค้าและยอดเงินใน Dictionary
                            totalAmountByCustomer.Add(customerID, totalAmount);
                        }
                    }
                }
            }

            return totalAmountByCustomer;
        }

    }
}

