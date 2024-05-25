using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class DepositRealInvoiceDataAccess
    {
        public static int AddDepositRealInvoice(int realInvoiceID, int depositID, decimal depositAmount, DateTime transactionDate, int customerID, int createdByUser)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"INSERT INTO deposits_realinvoices (RealInvoiceID, DepositID, DepositAmount, TransactionDate, CustomerID, CreatedByUser) 
                        VALUES (@RealInvoiceID, @DepositID, @DepositAmount, @TransactionDate, @CustomerID, @CreatedByUser);";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);
                cmd.Parameters.AddWithValue("@DepositID", depositID);
                cmd.Parameters.AddWithValue("@DepositAmount", depositAmount);
                cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@CreatedByUser", createdByUser);

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.CloseConnection();
                return rowsAffected > 0 ? 1 : -1;
            }
        }

        public static DataTable LoadDepositsRealInvoicesWithStatus()
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
                    C.Cname AS CustomerName, 
                    RI.InvoiceDate,
                    RI.TotalAmount,
                    RI.ExternalBillName,
                    RI.Details,
                    RI.CreateByUser,
                    RI.`DiscountAmount`,
                        COALESCE(SUM(Distinct DisR.`DiscountAmount`), 0) AS TotalDiscountUsed,
                        CASE
                            WHEN SUM(DisR.`DiscountAmount`) >= RI.`DiscountAmount` THEN 'Paid'
                            WHEN SUM(DisR.`DiscountAmount`) > 0 THEN 'Partially Paid (Discount Partially)'
                            ELSE 'Unpaid'
                        END AS DiscountStatus,
                    RI.DepositAmount,
                    COALESCE(SUM(DR.DepositAmount), 0) AS TotalDepositUsed,
                    CASE
                        WHEN COALESCE(SUM(DR.DepositAmount), 0) >= RI.DepositAmount THEN 'Paid'
                        WHEN COALESCE(SUM(DR.DepositAmount), 0) > 0 THEN 'Partially Paid (Deposit Partially)'
                        ELSE 'Unpaid'
                    END AS DepositStatus
                FROM 
                    realinvoices RI
                LEFT JOIN 
                    deposits_realinvoices DR ON RI.RealInvoiceID = DR.RealInvoiceID
                LEFT JOIN 
                    discounts_realinvoices DisR ON RI.RealInvoiceID = DisR.RealInvoiceID
                LEFT JOIN 
                    customers C ON RI.CustomerID = C.CustomerID
                GROUP BY 
                    RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.Details, RI.CreateByUser, RI.DiscountAmount, RI.DepositAmount";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable depositsRealInvoicesWithStatus = new DataTable();
                    adapter.Fill(depositsRealInvoicesWithStatus);

                    conn.CloseConnection();
                    return depositsRealInvoicesWithStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable SearchDepositsRealInvoicesWithStatus(string searchTerm)
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
                    C.Cname AS CustomerName, 
                    RI.InvoiceDate,
                    RI.TotalAmount,
                    RI.ExternalBillName,
                    RI.Details,
                    RI.CreateByUser,
                    RI.`DiscountAmount`,
                        SUM(Distinct DisR.`DiscountAmount`) AS TotalDiscountUsed,
                        CASE
                            WHEN SUM(DisR.`DiscountAmount`) >= RI.`DiscountAmount` THEN 'Paid'
                            WHEN SUM(DisR.`DiscountAmount`) > 0 THEN 'Partially Paid (Discount Partially)'
                            ELSE 'Unpaid'
                        END AS DiscountStatus,
                    RI.DepositAmount,
                    COALESCE(SUM(DR.DepositAmount), 0) AS TotalDepositUsed,
                    CASE
                        WHEN COALESCE(SUM(DR.DepositAmount), 0) >= RI.DepositAmount THEN 'Paid'
                        WHEN COALESCE(SUM(DR.DepositAmount), 0) > 0 THEN 'Partially Paid (Deposit Partially)'
                        ELSE 'Unpaid'
                    END AS DepositStatus
                FROM 
                    realinvoices RI
                LEFT JOIN 
                    deposits_realinvoices DR ON RI.RealInvoiceID = DR.RealInvoiceID
                LEFT JOIN 
                    discounts_realinvoices DisR ON RI.RealInvoiceID = DisR.RealInvoiceID
                LEFT JOIN 
                    customers C ON RI.CustomerID = C.CustomerID
                WHERE 
                    C.Cname LIKE @SearchTerm OR
                    RI.ExternalBillName LIKE @SearchTerm
                GROUP BY 
                    RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.Details, RI.CreateByUser, RI.DiscountAmount, RI.DepositAmount";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable searchResult = new DataTable();
                    adapter.Fill(searchResult);

                    conn.CloseConnection();

                    return searchResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // หรือจะคืน DataTable ว่างก็ได้ตามต้องการ
            }
        }
        public static DataTable SearchDepositsRealInvoicesWithStatusByDate(DateTime date)
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
                    C.Cname AS CustomerName, 
                    RI.InvoiceDate,
                    RI.TotalAmount,
                    RI.ExternalBillName,
                    RI.Details,
                    RI.CreateByUser,
                    RI.`DiscountAmount`,
                    SUM(Distinct DisR.`DiscountAmount`) AS TotalDiscountUsed,
                    CASE
                        WHEN SUM(DisR.`DiscountAmount`) >= RI.`DiscountAmount` THEN 'Paid'
                        WHEN SUM(DisR.`DiscountAmount`) > 0 THEN 'Partially Paid (Discount Partially)'
                        ELSE 'Unpaid'
                        END AS DiscountStatus,
                    RI.DepositAmount,
                    COALESCE(SUM(DR.DepositAmount), 0) AS TotalDepositUsed,
                    CASE
                        WHEN COALESCE(SUM(DR.DepositAmount), 0) >= RI.DepositAmount THEN 'Paid'
                        WHEN COALESCE(SUM(DR.DepositAmount), 0) > 0 THEN 'Partially Paid (Deposit Partially)'
                        ELSE 'Unpaid'
                    END AS DepositStatus
                FROM 
                    realinvoices RI
                LEFT JOIN 
                    deposits_realinvoices DR ON RI.RealInvoiceID = DR.RealInvoiceID
                 LEFT JOIN 
                    discounts_realinvoices DisR ON RI.RealInvoiceID = DisR.RealInvoiceID
                LEFT JOIN 
                    customers C ON RI.CustomerID = C.CustomerID
                WHERE 
                    DATE(RI.InvoiceDate) = @Date
                GROUP BY 
                    RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.Details, RI.CreateByUser, RI.DiscountAmount, RI.DepositAmount";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@Date", date);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable searchResult = new DataTable();
                    adapter.Fill(searchResult);

                    conn.CloseConnection();

                    return searchResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null; // หรือจะคืน DataTable ว่างก็ได้ตามต้องการ
            }
        }
        public static bool CheckDepositAmountLimitAndGetTotalDepositsUsed(int realInvoiceID, decimal newDepositAmount, out decimal totalDepositsUsed)
        {
            totalDepositsUsed = 0;

            // ดึงยอดฝากที่กำหนดไว้ใน realinvoices (DepositAmount) จากฐานข้อมูล
            decimal depositAmountLimit = GetDepositAmountLimit(realInvoiceID);

            // ดึงยอดฝากที่ใช้ไปแล้ว
            totalDepositsUsed = GetTotalDepositsUsed(realInvoiceID);

            // ตรวจสอบว่ายอดฝากใหม่รวมกับยอดฝากที่ใช้ไปแล้วเกินยอดเงินฝากที่กำหนดหรือไม่
            if (newDepositAmount + totalDepositsUsed > depositAmountLimit)
            {
                return false; // เกินยอดเงินฝากที่กำหนด
            }

            return true; // ยอดฝากไม่เกินหรือเท่ากับยอดเงินฝากที่กำหนด
        }
        private static decimal GetDepositAmountLimit(int realInvoiceID)
        {
            // เขียนโค้ดในการดึงยอดเงินฝากที่กำหนดไว้ใน realinvoices (DepositAmount) จากฐานข้อมูล
            decimal depositAmountLimit = 0;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"SELECT DepositAmount 
                         FROM realinvoices 
                         WHERE RealInvoiceID = @RealInvoiceID";

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    depositAmountLimit = Convert.ToDecimal(result);
                }

                conn.CloseConnection();
            }

            return depositAmountLimit;
            // คืนค่าเป็นยอดฝากที่กำหนด
            throw new NotImplementedException();
        }
        private static decimal GetTotalDepositsUsed(int realInvoiceID)
        {
            // เขียนโค้ดในการดึงยอดฝากที่ใช้ไปแล้วจากฐานข้อมูล
            decimal totalDepositsUsed = 0;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"SELECT COALESCE(SUM(DepositAmount), 0) AS TotalDepositsUsed 
                         FROM deposits_realinvoices 
                         WHERE RealInvoiceID = @RealInvoiceID";

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    totalDepositsUsed = Convert.ToDecimal(result);
                }

                conn.CloseConnection();
            }

            return totalDepositsUsed;
            // คืนค่าเป็นยอดฝากที่ใช้ไปแล้ว
            throw new NotImplementedException();
        }

    }
}

