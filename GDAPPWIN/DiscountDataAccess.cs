using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class DiscountDataAccess
    {
        public Dictionary<string, string> DiscountHeaders { get; } = new Dictionary<string, string>
            {
                { "DiscountID", "รหัสส่วนลด" },
                { "CustomerID", "รหัสลูกค้า" },
                { "CustomerName", "ชื่อลูกค้า" },
                { "DiscountName", "เลขที่(ORCC)" },
                { "DiscountAmount", "ส่วนลด" },
                { "CreateDate", "วันที่" },
                { "CreateByUser", "พนักงาน" }
            };

        public static int AddDiscount1(int customerID, string discountName, decimal discountAmount)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = "INSERT INTO discounts (CustomerID, DiscountName, DiscountAmount, DiscountDate) VALUES (@CustomerID, @DiscountName, @DiscountAmount, @DiscountDate)";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@DiscountName", discountName);
                cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                cmd.Parameters.AddWithValue("@DiscountDate", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return (int)cmd.LastInsertedId;
                }
                else
                {
                    return -1; // หรือจะเก็บค่า null ก็ได้ตามความเหมาะสม
                }
            }
        }

        public static int AddDiscount(int customerID, string discountName, decimal discountAmount, DateTime createDate, int createByUser)
        {
            int discountID = -1;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"INSERT INTO discounts (CustomerID, DiscountName, DiscountAmount, CreateDate, CreateByUser) 
                             VALUES (@CustomerID, @DiscountName, @DiscountAmount, @CreateDate, @CreateByUser);
                             SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@DiscountName", discountName);
                cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                cmd.Parameters.AddWithValue("@CreateDate", createDate);
                cmd.Parameters.AddWithValue("@CreateByUser", createByUser);

                discountID = Convert.ToInt32(cmd.ExecuteScalar());

                conn.CloseConnection();
            }

            return discountID;
        }
        public static decimal SumDiscountAmountByDate(DateTime date)
        {
            decimal sum = 0;
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = "SELECT SUM(`DiscountAmount`) FROM `discounts` WHERE DATE(`CreateDate`) = @Date";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        sum = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in SumDiscountAmountByDate: {ex.Message}");
            }
            return sum;
        }
        public static DataTable LoadDiscountsByDate(DateTime date)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = @"SELECT d.DiscountID, d.CustomerID, c.Cname AS CustomerName, d.DiscountName, d.DiscountAmount, d.CreateDate, d.CreateByUser
                FROM discounts AS d
                JOIN customers AS c ON d.CustomerID = c.CustomerID
                WHERE DATE(d.CreateDate) = @Date";


                    MySqlCommand cmd = new MySqlCommand(query, connection.GetConnection);
                    cmd.Parameters.AddWithValue("@Date", date.Date);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);

                    connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadDiscountsByDate: {ex.Message}");
            }

            return dataTable;
        }
        public static DataTable GetAllDiscounts()
        {
            DataTable discountsTable = new DataTable();

            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    dbConnect.OpenConnection();

                    string query = @"SELECT d.DiscountID, d.CustomerID, c.Cname AS CustomerName, d.DiscountName, d.DiscountAmount, d.CreateDate, d.CreateByUser
                             FROM discounts d 
                             INNER JOIN customers c ON d.CustomerID = c.CustomerID";

                    MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(discountsTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return discountsTable;
        }
        public static DataTable LoadDiscountsBetweenDates(DateTime startDate, DateTime endDate)
        {
            DataTable discountsTable = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = @"SELECT d.DiscountID, d.CustomerID, c.Cname AS CustomerName, d.DiscountName, d.DiscountAmount, d.CreateDate, d.CreateByUser 
                             FROM discounts AS d
                             INNER JOIN customers AS c ON d.CustomerID = c.CustomerID
                             WHERE d.CreateDate BETWEEN @StartDate AND @EndDate";

                    MySqlCommand command = new MySqlCommand(query, connection.GetConnection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(discountsTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return discountsTable;
        }
        public static DataTable LoadDiscountDataByCustomerName(string customerName)
        {
            DataTable discountData = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = @"SELECT d.DiscountID, d.CustomerID, c.Cname AS CustomerName, d.DiscountName, d.DiscountAmount, d.CreateDate, d.CreateByUser 
                                     FROM discounts AS d
                                     INNER JOIN customers AS c ON d.CustomerID = c.CustomerID
                                     WHERE c.Cname LIKE @CustomerName";

                    MySqlCommand command = new MySqlCommand(query, connection.GetConnection);
                    command.Parameters.AddWithValue("@CustomerName", $"%{customerName}%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(discountData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return discountData;
        }
    }
}
