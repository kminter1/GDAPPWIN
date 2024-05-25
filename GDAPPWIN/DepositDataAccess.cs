using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class DepositDataAccess
    {
        public int DepositID { get; set; }
        public int CustomerID { get; set; }
        public string DepositName { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateByUser { get; set; }

        public Dictionary<string, string> DepositHeaders { get; } = new Dictionary<string, string>
            {
                { "DepositID", "รหัสเงินฝาก" },
                { "CustomerID", "รหัสลูกค้า" },
                { "CustomerName", "ชื่อลูกค้า" },
                { "DepositName", "เลขที่(ORCC)" },
                { "DepositAmount", "เงินฝาก" },
                { "CreateDate", "วันที่" },
                { "CreateByUser", "พนักงาน" }
            };

        public static int AddDeposit(int customerID, string depositName, decimal depositAmount, DateTime createDate, int createByUser)
        {
            int depositID = -1; // ให้มีค่าเริ่มต้นเป็น -1 เพื่อบ่งบอกว่ามีข้อผิดพลาดในการเพิ่มข้อมูล

            using (DBconnect conn = new DBconnect())
            {
                try
                {
                    conn.OpenConnection();

                    string query = @"INSERT INTO deposits (CustomerID, DepositName, DepositAmount, CreateDate, CreateByUser) 
                         VALUES (@CustomerID, @DepositName, @DepositAmount, @CreateDate, @CreateByUser);
                         SELECT LAST_INSERT_ID();"; // เลือกค่า ID ล่าสุดที่ถูกสร้าง

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@DepositName", depositName);
                    cmd.Parameters.AddWithValue("@DepositAmount", depositAmount);
                    cmd.Parameters.AddWithValue("@CreateDate", createDate);
                    cmd.Parameters.AddWithValue("@CreateByUser", createByUser);

                    // รันคำสั่ง SQL และรับค่า ID ที่เพิ่มเข้าไป
                    if (int.TryParse(cmd.ExecuteScalar()?.ToString(), out depositID))
                    {
                        // ค่าที่รับคืนมาเป็น int และถูกแปลงเป็น depositID ได้
                        return depositID;
                    }
                    else
                    {
                        // ไม่สามารถแปลงค่าที่รับคืนมาเป็น int ได้
                        return -1; // หรือค่าใดค่าหนึ่งที่เหมาะสมตามระบบ
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return -1; // หรือค่าใดค่าหนึ่งที่เหมาะสมตามระบบ
                }
                finally
                {
                    conn.CloseConnection(); // ปิดการเชื่อมต่อไม่ว่าจะเกิดข้อผิดพลาดหรือไม่ก็ตาม
                }
            }
        }
        public static decimal SumDepositAmountByDate(DateTime date)
        {
            decimal sum = 0;
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = "SELECT SUM(`DepositAmount`) FROM `deposits` WHERE `CreateDate` = @Date";
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
                MessageBox.Show($"Error in SumDepositAmountByDate: {ex.Message}");
            }
            return sum;
        }
        public static DataTable LoadDepositsByDate(DateTime date)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = "SELECT d.`DepositID`, d.`CustomerID`, c.`Cname` As CustomerName, d.`DepositName`, d.`DepositAmount`, d.`CreateDate`, d.`CreateByUser` " +
                                   "FROM `deposits` AS d " +
                                   "JOIN `customers` AS c ON d.`CustomerID` = c.`CustomerID` " +
                                   "WHERE DATE(d.`CreateDate`) = @Date";

                    MySqlCommand cmd = new MySqlCommand(query, connection.GetConnection);
                    cmd.Parameters.AddWithValue("@Date", date);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);

                    connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadDepositsByDate: {ex.Message}");
            }

            return dataTable;
        }
        public static DataTable GetAllDeposits()
        {
            DataTable depositsTable = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = @"SELECT d.DepositID, d.CustomerID, c.Cname AS CustomerName, d.DepositName, d.DepositAmount, d.CreateDate, d.CreateByUser 
                             FROM deposits AS d
                             INNER JOIN customers AS c ON d.CustomerID = c.CustomerID";

                    MySqlCommand command = new MySqlCommand(query, connection.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(depositsTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return depositsTable;
        }
        public static DataTable LoadDepositsBetweenDates(DateTime startDate, DateTime endDate)
        {
            DataTable depositsTable = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = @"SELECT d.DepositID, d.CustomerID, c.Cname AS CustomerName, d.DepositName, d.DepositAmount, d.CreateDate, d.CreateByUser 
                             FROM deposits AS d
                             INNER JOIN customers AS c ON d.CustomerID = c.CustomerID
                             WHERE d.CreateDate BETWEEN @StartDate AND @EndDate";

                    MySqlCommand command = new MySqlCommand(query, connection.GetConnection);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(depositsTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return depositsTable;
        }
        public static DataTable LoadDepositDataByCustomerName(string customerName)
        {
            DataTable depositData = new DataTable();

            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    string query = @"SELECT d.DepositID, d.CustomerID, c.Cname AS CustomerName, d.DepositName, d.DepositAmount, d.CreateDate, d.CreateByUser 
                             FROM deposits AS d
                             INNER JOIN customers AS c ON d.CustomerID = c.CustomerID
                             WHERE c.Cname LIKE @CustomerName";

                    MySqlCommand command = new MySqlCommand(query, connection.GetConnection);
                    command.Parameters.AddWithValue("@CustomerName", $"%{customerName}%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(depositData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return depositData;
        }



    }
}

