using MySql.Data.MySqlClient;
using System.Configuration;

namespace GDAPPWIN
{
    internal class DBconnect : IDisposable
    {
        private MySqlConnection connect;

        public DBconnect()
        {
            // Fetch the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            connect = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection
        {
            get { return connect; }
        }

        public bool OpenConnection()
        {
            try
            {
                if (connect.State == System.Data.ConnectionState.Closed)
                {
                    connect.Open();
                    return true; // คืนค่า true เมื่อการเชื่อมต่อถูกเปิด
                }
            }
            catch (MySqlException ex)
            {
                // จัดการข้อผิดพลาดเมื่อเกิดข้อผิดพลาดในการเปิดการเชื่อมต่อ
                Console.WriteLine("Error opening connection: " + ex.Message);
            }

            return false; // คืนค่า false เมื่อเกิดข้อผิดพลาดหรือการเชื่อมต่อมีสถานะอื่นที่ไม่ใช่ Closed
        }

        public bool CloseConnection()
        {
            try
            {
                if (connect.State == System.Data.ConnectionState.Open)
                {
                    connect.Close();
                    return true; // คืนค่า true เมื่อการเชื่อมต่อถูกปิด
                }
            }
            catch (MySqlException ex)
            {
                // จัดการข้อผิดพลาดเมื่อเกิดข้อผิดพลาดในการปิดการเชื่อมต่อ
                Console.WriteLine("Error closing connection: " + ex.Message);
            }

            return false; // คืนค่า false เมื่อเกิดข้อผิดพลาดหรือการเชื่อมต่อมีสถานะอื่นที่ไม่ใช่ Open
        }

        public bool VerifyLogin(string username, string password)
        {
            string query = $"SELECT * FROM users WHERE Username='{username}' AND Password='{password}'";

            try
            {
                if (OpenConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                // Login successful
                                CloseConnection();
                                return true;
                            }
                        }
                    }

                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Login failed
            return false;
        }

        public (int, string) GetRoleAndUserID(string username)
        {
            var userID = -1;
            var role = string.Empty;

            string query = $"SELECT UserID, Role FROM users WHERE Username='{username}'";

            try
            {
                if (OpenConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                dataReader.Read();
                                userID = Convert.ToInt32(dataReader["UserID"]);
                                role = dataReader["Role"].ToString();
                            }
                        }
                    }

                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return (userID, role);
        }

        // Implement IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // คลุมทรัพยากรที่ต้องการกำจัด เช่น Dispose ของ Object
                if (connect != null)
                {
                    connect.Dispose();
                    connect = null;
                }
            }
        }
    }
}
