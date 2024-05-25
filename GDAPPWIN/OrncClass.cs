using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;

namespace GDAPPWIN
{
    internal class OrncClass
    {
        DBconnect connect = new DBconnect();
        public bool IsOrncNoExists(string orncno)
        {
            string query = "SELECT COUNT(*) FROM temporaryinvoices WHERE Ornc_No = @orncno";

            using (MySqlCommand cmd = new MySqlCommand(query, connect.GetConnection))
            {
                cmd.Parameters.AddWithValue("@orncno", orncno);

                try
                {
                    connect.OpenConnection();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error checking OrncNo existence: " + ex.Message);
                    return false;
                }
                finally
                {
                    connect.CloseConnection();
                }
            }
        }

        public bool InsertOrnc(int ornccid, DateTime orncdate, string orncdetail, string orncno, decimal orncamount, int userID, decimal orncdiscount, decimal orncdeposit)
        {
            using (MySqlCommand command = new MySqlCommand("INSERT INTO `temporaryinvoices`(`CustomerID`, `IssueDate`, `Details`, `Ornc_No`, `TotalAmount`, `createByUser`, `DiscountAmount`, `DepositAmount`) VALUES(@ocid, @ord, @ordt, @orno, @oramt, @orbyuser, @orncDiscount, @orncDeposit)", connect.GetConnection))
            {
                command.Parameters.Add("@ocid", MySqlDbType.Int32).Value = ornccid;
                command.Parameters.Add("@ord", MySqlDbType.Date).Value = orncdate;
                command.Parameters.Add("@ordt", MySqlDbType.Text).Value = orncdetail;
                command.Parameters.Add("@orno", MySqlDbType.VarChar).Value = orncno;
                command.Parameters.Add("@oramt", MySqlDbType.Decimal).Value = orncamount;
                command.Parameters.Add("@orbyuser", MySqlDbType.Int32).Value = userID;
                command.Parameters.Add("@orncDiscount", MySqlDbType.Decimal).Value = orncdiscount;
                command.Parameters.Add("@orncDeposit", MySqlDbType.Decimal).Value = orncdeposit;
                connect.OpenConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public DataTable GetCustomerList()
        {
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM `customers`", connect.GetConnection))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public DataTable GetCustomerByName(string searchName)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM `customers` WHERE Cname LIKE @searchName OR Ccode LIKE @searchName", connect.GetConnection))
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

        public DataTable GetOrncList()
        {
            using (MySqlCommand command = new MySqlCommand("SELECT oi.`TemporaryInvoiceID`, c.`Cname`, oi.`IssueDate`, oi.`Details`, oi.`Ornc_No`, oi.`TotalAmount` FROM `temporaryinvoices` oi INNER JOIN `customers` c ON oi.`CustomerID` = c.`CustomerID`", connect.GetConnection))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        //เพิ่มตารางใหม่ด้านล่างสุด ทดสอบใช้งาน
        public static List<OrncBill> GetAllOrncBills()
        {
            List<OrncBill> Ornclists = new List<OrncBill>();
            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = "SELECT ti.TemporaryInvoiceID, c.Cname AS CustomerName, ti.IssueDate, ti.Details, ti.Ornc_No, ti.TotalAmount, " +
                                   "CASE WHEN rti.RealInvoiceID IS NOT NULL THEN 'Linked' ELSE 'Not Linked' END AS LinkStatus, " +
                                   "ri.ExternalBillName " +
                                   "FROM temporaryinvoices ti " +
                                   "INNER JOIN customers c ON ti.CustomerID = c.CustomerID " +
                                   "LEFT JOIN realinvoices_temporaryinvoices rti ON ti.TemporaryInvoiceID = rti.TemporaryInvoiceID " +
                                   "LEFT JOIN realinvoices ri ON rti.RealInvoiceID = ri.RealInvoiceID";

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrncBill orncBill = new OrncBill
                            {
                                TemporaryInvoiceID = Convert.ToInt32(reader["TemporaryInvoiceID"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                                Details = reader["Details"].ToString(),
                                Ornc_No = reader["Ornc_No"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                LinkStatus = reader["LinkStatus"].ToString(),
                                ExternalBillName = reader["ExternalBillName"].ToString()
                            };

                            Ornclists.Add(orncBill);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    Console.WriteLine(ex.Message);
                }
            }

            return Ornclists;
        }

        public static List<OrncBill> GetOrncInvoiceByTerm(string searchTerm)
        {
            List<OrncBill> orncBills = new List<OrncBill>();

            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    // สร้าง query ด้วยเงื่อนไขค้นหา Ornc_no, ExternalBillName, และ CustomerName
                    string query = "SELECT ti.TemporaryInvoiceID, c.Cname AS CustomerName, ti.IssueDate, ti.Details, ti.Ornc_No, ti.TotalAmount, " +
                                   "CASE WHEN rti.RealInvoiceID IS NOT NULL THEN 'Linked' ELSE 'Not Linked' END AS LinkStatus, " +
                                   "ri.ExternalBillName " +
                                   "FROM temporaryinvoices ti " +
                                   "INNER JOIN customers c ON ti.CustomerID = c.CustomerID " +
                                   "LEFT JOIN realinvoices_temporaryinvoices rti ON ti.TemporaryInvoiceID = rti.TemporaryInvoiceID " +
                                   "LEFT JOIN realinvoices ri ON rti.RealInvoiceID = ri.RealInvoiceID " +
                                   "WHERE ti.Ornc_No LIKE @SearchTerm OR ri.ExternalBillName LIKE @SearchTerm OR c.Cname LIKE @SearchTerm";

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);

                    // เพิ่มพารามิเตอร์สำหรับคำค้นหา
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrncBill orncBill = new OrncBill
                            {
                                TemporaryInvoiceID = Convert.ToInt32(reader["TemporaryInvoiceID"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                                Details = reader["Details"].ToString(),
                                Ornc_No = reader["Ornc_No"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                LinkStatus = reader["LinkStatus"].ToString(),
                                ExternalBillName = reader["ExternalBillName"].ToString()
                            };

                            orncBills.Add(orncBill);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    Console.WriteLine(ex.Message);
                }
            }

            return orncBills;
        }

        public static List<OrncBill> GetOrncInvoiceByDate(DateTime? searchDate)
        {
            List<OrncBill> orncByDate = new List<OrncBill>();

            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = @"
SELECT 
    ti.`TemporaryInvoiceID`, 
    ti.`CustomerID`, 
    c.`Cname` AS `CustomerName`,  
    ti.`IssueDate`, 
    ti.`Details`, 
    ti.`Ornc_No`, 
    ti.`TotalAmount`, 
    ti.`createByUser`, 
    ti.`DiscountAmount`, 
    ti.`DepositAmount`, 
    ti.`isPaid`,
    CASE 
        WHEN rti.`RealInvoiceID` IS NOT NULL THEN 'Linked' 
        ELSE 'Not Linked' 
    END AS LinkStatus,
    ri.`ExternalBillName`  -- Include the ExternalBillName from the realinvoices table
FROM 
    `temporaryinvoices` ti
LEFT JOIN 
    `realinvoices_temporaryinvoices` rti ON ti.`TemporaryInvoiceID` = rti.`TemporaryInvoiceID`
LEFT JOIN 
    `realinvoices` ri ON rti.`RealInvoiceID` = ri.`RealInvoiceID`  -- Join with realinvoices table
LEFT JOIN 
    `customers` c ON ti.`CustomerID` = c.`CustomerID`  
WHERE 
    DATE(ti.`IssueDate`) = @SearchDate;
";
                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);

                    // เพิ่มพารามิเตอร์สำหรับคำค้นหา
                    cmd.Parameters.AddWithValue("@SearchDate", searchDate);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrncBill orncBill = new OrncBill
                            {
                                TemporaryInvoiceID = Convert.ToInt32(reader["TemporaryInvoiceID"]),
                                CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                                Details = reader["Details"].ToString(),
                                Ornc_No = reader["Ornc_No"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                DepositAmount = Convert.ToDecimal(reader["DepositAmount"]),
                                LinkStatus = reader["LinkStatus"].ToString(),
                                ExternalBillName = reader["ExternalBillName"].ToString()
                            };

                            orncByDate.Add(orncBill);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    Console.WriteLine(ex.Message);
                }
            }

            return orncByDate;
        }
        public static decimal GetTotalOrncAmountByDate(DateTime? searchDate)
        {
            decimal totalAmount = 0;

            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = "SELECT SUM(ti.TotalAmount) AS TotalAmount " +
                                   "FROM temporaryinvoices ti " +
                                   "WHERE ti.IsVoided = 0";

                    if (searchDate.HasValue)
                    {
                        query += "WHERE DATE(ti.IssueDate) = @SearchDate";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);

                    if (searchDate.HasValue)
                    {
                        // เพิ่มพารามิเตอร์สำหรับคำค้นหา
                        cmd.Parameters.AddWithValue("@SearchDate", searchDate);
                    }

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalAmount = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    Console.WriteLine(ex.Message);
                }
            }

            return totalAmount;
        }
    }
}
