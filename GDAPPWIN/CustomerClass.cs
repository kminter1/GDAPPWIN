using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    public class CustomerClass
    {

        DBconnect connect = new DBconnect();

        public bool InsertCustomer(string ccode, string cname)
        {
            // Check if Ccode already exists
            if (IsCcodeExists(ccode))
            {
                return false;
            }

            // Insert new customer
            using (MySqlCommand command = new MySqlCommand("INSERT INTO `customers`(`Ccode`, `Cname`) VALUES(@cc, @cn)", connect.GetConnection))
            {
                command.Parameters.Add("@cc", MySqlDbType.VarChar).Value = ccode;
                command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cname;

                try
                {
                    connect.OpenConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error inserting customer: " + ex.Message);
                }
                finally
                {
                    connect.CloseConnection();
                }

                return false;
            }
        }


        public bool IsCcodeExists(string ccode)
        {
            string query = "SELECT COUNT(*) FROM customers WHERE Ccode = @ccode";

            using (MySqlCommand cmd = new MySqlCommand(query, connect.GetConnection))
            {
                cmd.Parameters.AddWithValue("@ccode", ccode);

                try
                {
                    connect.OpenConnection();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการตรวจสอบ Ccode: " + ex.Message, "เพิ่มลูกค้า", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }

        // To get customer table
        public DataTable GetCustomerList()
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

    }
}
