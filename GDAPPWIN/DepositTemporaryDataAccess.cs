using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class DepositTemporaryDataAccess
    {
        public static int AddDepositTemporaryInvoices(int temporaryInvoiceID, int depositID, decimal depositAmount, DateTime transactionDate, int customerID, int createdByUser)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    string query = @"INSERT INTO deposits_temporaryinvoices (TemporaryInvoiceID, DepositID, DepositAmount, TransactionDate, CustomerID, CreatedByUser) 
                             VALUES (@TemporaryInvoiceID, @DepositID, @DepositAmount, @TransactionDate, @CustomerID, @CreatedByUser);";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1;
            }
        }
        public static DataTable LoadDepositsTemporaryInvoicesWithStatus()
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                                SELECT 
                        TI.`TemporaryInvoiceID`, 
                        TI.`CustomerID`, 
                        C.`Cname` AS CustomerName,
                        TI.`IssueDate`, 
                        TI.`Details`, 
                        TI.`Ornc_No`, 
                        TI.`TotalAmount`, 
                        TI.`createByUser`, 
                        TI.`DiscountAmount`,
                        SUM(Distinct Disti.`DiscountAmount`) AS TotalDiscountUsed,
                        CASE
                            WHEN SUM(Disti.`DiscountAmount`) >= TI.`DiscountAmount` THEN 'Succeed'
                            WHEN SUM(Disti.`DiscountAmount`) > 0 THEN 'Partially Paid (Discount Partially)'
                            ELSE 'Unsuccessful'
                        END AS DiscountStatus,
                        TI.`DepositAmount`,
                        SUM(DTI.`DepositAmount`) AS TotalDepositUsed,
                        CASE
                            WHEN SUM(DTI.`DepositAmount`) >= TI.`DepositAmount` THEN 'Succeed'
                            WHEN SUM(DTI.`DepositAmount`) > 0 THEN 'Partially Paid (Deposit Partially)'
                            ELSE 'Unsuccessful'
                        END AS DepositStatus
                    FROM 
                        `temporaryinvoices` TI
                    LEFT JOIN 
                        `deposits_temporaryinvoices` DTI ON TI.`TemporaryInvoiceID` = DTI.`TemporaryInvoiceID`
                    LEFT JOIN 
                        `discounts_temporaryinvoices` Disti ON TI.`TemporaryInvoiceID` = Disti.`TemporaryInvoiceID`
                    LEFT JOIN
                        `customers` C ON TI.`CustomerID` = C.`CustomerID`
                    GROUP BY 
                        TI.`TemporaryInvoiceID`, TI.`CustomerID`, C.`Cname`, TI.`IssueDate`, TI.`Details`, TI.`Ornc_No`, TI.`TotalAmount`, TI.`createByUser`, TI.`DiscountAmount`, TI.`DepositAmount`";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable depositsTemporaryInvoicesWithStatus = new DataTable();
                    adapter.Fill(depositsTemporaryInvoicesWithStatus);

                    conn.CloseConnection();
                    return depositsTemporaryInvoicesWithStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable SearchDepositsTemporaryInvoicesWithStatus(string searchTerm)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                                SELECT 
                        TI.`TemporaryInvoiceID`,
                        C.`CustomerID`,
                        C.`Ccode`, 
                        C.`Cname` AS CustomerName,
                        TI.`IssueDate`, 
                        TI.`Details`, 
                        TI.`Ornc_No`, 
                        TI.`TotalAmount`, 
                        TI.`createByUser`, 
                        TI.`DiscountAmount`,
                        SUM(Distinct Disti.`DiscountAmount`) AS TotalDiscountUsed,
                        CASE
                            WHEN SUM(Disti.`DiscountAmount`) >= TI.`DiscountAmount` THEN 'Succeed'
                            WHEN SUM(Disti.`DiscountAmount`) > 0 THEN 'Partially Paid (Discount Partially)'
                            ELSE 'Unsuccessful'
                        END AS DiscountStatus,
                        TI.`DepositAmount`,
                        SUM(DTI.`DepositAmount`) AS TotalDepositUsed,
                        CASE
                            WHEN SUM(DTI.`DepositAmount`) >= TI.`DepositAmount` THEN 'Succeed'
                            WHEN SUM(DTI.`DepositAmount`) > 0 THEN 'Partially Paid (Deposit Partially)'
                            ELSE 'Unsuccessful'
                        END AS DepositStatus
                    FROM 
                        `temporaryinvoices` TI
                    LEFT JOIN 
                        `deposits_temporaryinvoices` DTI ON TI.`TemporaryInvoiceID` = DTI.`TemporaryInvoiceID`
                    LEFT JOIN 
                        `discounts_temporaryinvoices` Disti ON TI.`TemporaryInvoiceID` = Disti.`TemporaryInvoiceID`
                    LEFT JOIN
                        `customers` C ON TI.`CustomerID` = C.`CustomerID`
                    WHERE
                    (C.`Cname` LIKE CONCAT('%', @SearchTerm, '%') OR TI.`Ornc_No` LIKE CONCAT('%', @SearchTerm, '%'))
                    GROUP BY 
                        TI.`TemporaryInvoiceID`, TI.`CustomerID`, C.`Cname`, TI.`IssueDate`, TI.`Details`, TI.`Ornc_No`, TI.`TotalAmount`, TI.`createByUser`, TI.`DiscountAmount`, TI.`DepositAmount`";


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
        public static bool CheckDepositAmountLimitAndGetTotalDepositsUsed(int temporaryInvoiceID, decimal newDepositAmount, out decimal totalDepositsUsed)
        {
            totalDepositsUsed = 0;

            // ดึงยอดฝากที่กำหนดไว้ใน temporaryinvoices (DepositAmount) จากฐานข้อมูล
            decimal depositAmountLimit = GetDepositAmountLimit(temporaryInvoiceID);

            // ดึงยอดฝากที่ใช้ไปแล้ว
            totalDepositsUsed = GetTotalDepositsUsed(temporaryInvoiceID);

            // ตรวจสอบว่ายอดฝากใหม่รวมกับยอดฝากที่ใช้ไปแล้วเกินยอดเงินฝากที่กำหนดหรือไม่
            if (newDepositAmount + totalDepositsUsed > depositAmountLimit)
            {
                return false; // เกินยอดเงินฝากที่กำหนด
            }

            return true; // ยอดฝากไม่เกินหรือเท่ากับยอดเงินฝากที่กำหนด
        }

        private static decimal GetDepositAmountLimit(int temporaryInvoiceID)
        {
            // เขียนโค้ดในการดึงยอดเงินฝากที่กำหนดไว้ใน temporaryinvoices (DepositAmount) จากฐานข้อมูล
            decimal depositAmountLimit = 0;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"SELECT DepositAmount 
                         FROM temporaryinvoices 
                         WHERE TemporaryInvoiceID = @TemporaryInvoiceID";

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    depositAmountLimit = Convert.ToDecimal(result);
                }

                conn.CloseConnection();
            }

            return depositAmountLimit;
        }

        private static decimal GetTotalDepositsUsed(int temporaryInvoiceID)
        {
            // เขียนโค้ดในการดึงยอดฝากที่ใช้ไปแล้วจากฐานข้อมูล
            decimal totalDepositsUsed = 0;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"SELECT COALESCE(SUM(DepositAmount), 0) AS TotalDepositsUsed 
                         FROM deposits_temporaryinvoices 
                         WHERE TemporaryInvoiceID = @TemporaryInvoiceID";

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    totalDepositsUsed = Convert.ToDecimal(result);
                }

                conn.CloseConnection();
            }

            return totalDepositsUsed;
        }

        public static DataTable LoadDepositsTemporaryInvoicesWithStatusByDate(DateTime? date)
        {
            using (DBconnect conn = new DBconnect())
            {
                try
                {
                    conn.OpenConnection();
                    string query = @"
                               SELECT 
                    TI.`TemporaryInvoiceID`, 
                    C.`Cname`,
                    TI.`IssueDate`, 
                    TI.`Details`, 
                    TI.`Ornc_No`, 
                    TI.`TotalAmount`, 
                    TI.`createByUser`, 
                    TI.`DiscountAmount`, 
                    TI.`DepositAmount`,
                    COALESCE(SUM(DT.`DepositAmount`), 0) AS TotalDepositUsed,
                    CASE
                        WHEN COALESCE(SUM(DT.`DepositAmount`), 0) >= TI.`DepositAmount` THEN 'Succeed'
                        WHEN COALESCE(SUM(DT.`DepositAmount`), 0) > 0 THEN 'Partially Paid (Deposit Partially)'
                        ELSE 'Unsuccessful'
                    END AS Status
                FROM 
                    `temporaryinvoices` TI
                LEFT JOIN 
                    `deposits_temporaryinvoices` DT ON TI.`TemporaryInvoiceID` = DT.`TemporaryInvoiceID`
                LEFT JOIN
                    `customers` C ON TI.`CustomerID` = C.`CustomerID`
                WHERE
                    TI.`IssueDate` = @Date
                GROUP BY 
                    TI.`TemporaryInvoiceID`, C.`Cname`, TI.`IssueDate`, TI.`Details`, TI.`Ornc_No`, TI.`TotalAmount`, TI.`createByUser`, TI.`DiscountAmount`, TI.`DepositAmount`";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@Date", date);

                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                    return table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return null;
                }
                finally
                {
                    conn.CloseConnection();
                }
            }
        }




    }
}
