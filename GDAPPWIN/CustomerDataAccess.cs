using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    public class CustomerDataAccess
    {
        // Properties for customer data
        public string? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerCode { get; set; }

        // Dictionary to store column headers for translation
        private Dictionary<string, string> columnHeaders = new Dictionary<string, string>
        {
            { "CustomerID", "รหัสลูกค้า" },
            { "Ccode", "รหัสลูกค้า" },
            { "Cname", "ชื่อลูกค้า" },
            // Add other columns as needed
        };

        // Method to set column headers in Thai language for a DataGridView
        public void SetThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var column in columnHeaders)
            {
                if (dataGridView.Columns.Contains(column.Key))
                {
                    dataGridView.Columns[column.Key].HeaderText = column.Value;
                }
            }
        }

        // Method to retrieve customer data from the database
        public static DataTable GetCustomerList()
        {
            using (DBconnect dBconnect = new DBconnect())
            {
                using (MySqlCommand command = new MySqlCommand("SELECT `CustomerID`, `Cname`, `Ccode` FROM `customers`", dBconnect.GetConnection))
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

        // Method to search for customers based on the search term
        public static DataTable SearchCustomers(string searchTerm)
        {
            using (DBconnect dBconnect = new DBconnect())
            {
                string query = "SELECT `CustomerID`, `Cname`, `Ccode` FROM `customers` WHERE `Cname` LIKE @searchTerm OR `Ccode` LIKE @searchTerm";
                using (MySqlCommand command = new MySqlCommand(query, dBconnect.GetConnection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
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
