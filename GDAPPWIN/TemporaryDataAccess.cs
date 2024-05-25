using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    internal class TemporaryDataAccess
    { 
        public List<string > StringList { get; set; }
        public TemporaryDataAccess() 
        {
            StringList = new List<string> { "TotalAmount", "DiscountAmount", "TotalDiscountUsed", "DepositAmount", "TotalDepositUsed", "OutstandingDebt", "AmountToPay", "TotalPayment"};
        }
        public static DataTable LoadTemporaryInvoices(string searchTerm = "", DateTime? selectedDate = null, int? selectedMonth = null, int? selectedYear = null)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    string query = @"
            SELECT 
                TI.TemporaryInvoiceID,
                TI.CustomerID,
                C.Ccode,
                C.Cname AS CustomerName,
                TI.IssueDate,
                TI.TotalAmount,
                TI.Details,
                TI.Ornc_No,
                COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
                SUM(DR.DiscountAmount) AS TotalDiscountUsed,
                CASE
                    WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
                    WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                    ELSE 'Not Paid'
                END AS DiscountStatus,
                COALESCE(TI.DepositAmount, 0) AS DepositAmount,
                SUM(DepS.DepositAmount) AS TotalDepositUsed,
                SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
                CASE
                    WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
                    WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                    ELSE 'Not Paid'
                END AS DepositStatus,
                RI.ExternalBillName,
                R.ReceiptNumber,
                COALESCE(R.NetCash, 0) + COALESCE(R.WithHoldingTax, 0) + COALESCE(R.Fee, 0) AS TotalPayment,
                CASE
                    WHEN TI.IsVoided = 1 THEN 'Voided'
                    ELSE 'Normal'
                END AS Status,
                U.Ucode,
                RI.OutstandingDebt
            FROM 
                temporaryinvoices TI
            LEFT JOIN 
                realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
            LEFT JOIN 
                realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
            LEFT JOIN 
                receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
            LEFT JOIN 
                receipts R ON RR.ReceiptID = R.ReceiptID
            LEFT JOIN 
                discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
            LEFT JOIN 
                deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
            LEFT JOIN
                customers C ON TI.CustomerID = C.CustomerID
            LEFT JOIN
                users U ON TI.CreateByUser = U.UserID
            WHERE 
                (C.Ccode LIKE @SearchTerm OR C.Cname LIKE @SearchTerm OR TI.Ornc_No LIKE @SearchTerm)";

                    // Add filters for date, month, and year
                    if (selectedDate != null)
                    {
                        query += " AND DATE(TI.IssueDate) = @SelectedDate";
                    }
                    else if (selectedMonth != null && selectedYear != null)
                    {
                        query += " AND MONTH(TI.IssueDate) = @SelectedMonth AND YEAR(TI.IssueDate) = @SelectedYear";
                    }

                    query += @"
            GROUP BY 
                TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, 
                TI.DiscountAmount, TI.DepositAmount, RI.ExternalBillName, R.ReceiptNumber, TI.IsVoided, U.Ucode, 
                RI.OutstandingDebt;";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    // Add parameters for selectedDate, selectedMonth, and selectedYear
                    if (selectedDate != null)
                    {
                        cmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Value.ToString("yyyy-MM-dd"));
                    }
                    else if (selectedMonth != null && selectedYear != null)
                    {
                        cmd.Parameters.AddWithValue("@SelectedMonth", selectedMonth.Value);
                        cmd.Parameters.AddWithValue("@SelectedYear", selectedYear.Value);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable temporaryInvoicesWithStatus = new DataTable();
                    adapter.Fill(temporaryInvoicesWithStatus);

                    conn.CloseConnection();
                    return temporaryInvoicesWithStatus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in LoadTemporaryInvoices: {ex.Message}");
                throw;
            }
        }

        public static bool UpdateTemporaryInvoice(OrncBill bill)
        {
            try
            {
                using (DBconnect db = new DBconnect())
                {
                    // สร้างคำสั่ง SQL สำหรับการอัปเดตข้อมูลในตาราง temporaryinvoices
                    string query = "UPDATE temporaryinvoices SET IssueDate = @IssueDate, Details = @Details, Ornc_No = @Ornc_No, TotalAmount = @TotalAmount, " +
                                   "DepositAmount = @DepositAmount, DiscountAmount = @DiscountAmount " +
                                   "WHERE TemporaryInvoiceID = @TemporaryInvoiceID";

                    Console.WriteLine($"Executing SQL Query: {query}"); // แสดงคำสั่ง SQL ใน Console

                    // สร้าง MySqlCommand เพื่อทำการอัปเดตข้อมูล
                    using (MySqlCommand cmd = new MySqlCommand(query, db.GetConnection))
                    {
                        // กำหนดค่าพารามิเตอร์ของคำสั่ง SQL
                        cmd.Parameters.AddWithValue("@CustomerID", bill.CustomerID);
                        cmd.Parameters.AddWithValue("@IssueDate", bill.IssueDate);
                        cmd.Parameters.AddWithValue("@Details", bill.Details);
                        cmd.Parameters.AddWithValue("@Ornc_No", bill.Ornc_No);
                        cmd.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
                        cmd.Parameters.AddWithValue("@DepositAmount", bill.DepositAmount);
                        cmd.Parameters.AddWithValue("@DiscountAmount", bill.DiscountAmount);
                        cmd.Parameters.AddWithValue("@TemporaryInvoiceID", bill.TemporaryInvoiceID);

                        Console.WriteLine("Parameters Set."); // แสดงข้อความเมื่อพารามิเตอร์ถูกตั้งค่าเรียบร้อยแล้วใน Console

                        // เปิดการเชื่อมต่อกับฐานข้อมูล
                        if (db.OpenConnection())
                        {
                            Console.WriteLine("Database Connection Opened."); // แสดงข้อความเมื่อการเชื่อมต่อฐานข้อมูลถูกเปิดใน Console

                            // ทำการอัปเดตข้อมูล
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // ปิดการเชื่อมต่อ
                            db.CloseConnection();

                            // ตรวจสอบว่ามีแถวที่ถูกอัปเดตหรือไม่
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Update Successful."); // แสดงข้อความเมื่ออัปเดตข้อมูลเสร็จสิ้นใน Console
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to Open Database Connection."); // แสดงข้อความเมื่อเชื่อมต่อฐานข้อมูลไม่สำเร็จใน Console
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }

            return false;
        }

        public static DataTable GetTemporaryInvoicePaymentStatus()
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                        SELECT 
    TI.TemporaryInvoiceID,
    TI.CustomerID,
    C.Ccode,
    C.Cname AS CustomerName,
    TI.IssueDate,
    TI.TotalAmount,
    TI.Details,
    TI.Ornc_No,
    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
    SUM(DR.DiscountAmount) AS TotalDiscountUsed,
    DR.TransactionDate AS DiscountTransactionDate,
    CASE
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DiscountStatus,
    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
    SUM(DepS.DepositAmount) AS TotalDepositUsed,
    SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
    DepS.TransactionDate AS DepositTransactionDate,
    CASE
        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DepositStatus,
    RI.ExternalBillName,
    R.ReceiptNumber
FROM 
    temporaryinvoices TI
LEFT JOIN 
    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
LEFT JOIN 
    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
LEFT JOIN 
    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
LEFT JOIN 
    receipts R ON RR.ReceiptID = R.ReceiptID
LEFT JOIN 
    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
LEFT JOIN 
    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
LEFT JOIN
    customers C ON TI.CustomerID = C.CustomerID
GROUP BY 
    TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, TI.DiscountAmount, TI.DepositAmount, RI.ExternalBillName, R.ReceiptNumber;
";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable temporaryInvoicePaymentStatus = new DataTable();
                    adapter.Fill(temporaryInvoicePaymentStatus);

                    conn.CloseConnection();
                    return temporaryInvoicePaymentStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable SearchGetAllInvoicePaymentStatus(string searchTerm)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    // คำสั่ง SQL สำหรับค้นหาข้อมูลใบแจ้งหนี้จริง (Real Invoice) พร้อมกับยอดส่วนลดที่ใช้แล้ว
                    string query = @"
                    SELECT 
    TI.TemporaryInvoiceID,
    TI.CustomerID,
    C.Ccode,
    C.Cname AS CustomerName,
    TI.IssueDate,
    TI.TotalAmount,
    TI.Details,
    TI.Ornc_No,
    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
    SUM(DR.DiscountAmount) AS TotalDiscountUsed,
    DR.TransactionDate AS DiscountTransactionDate,
    CASE
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DiscountStatus,
    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
    SUM(DepS.DepositAmount) AS TotalDepositUsed,
    SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
    DepS.TransactionDate AS DepositTransactionDate,
    CASE
        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DepositStatus,
    RI.ExternalBillName,
    R.ReceiptNumber
FROM 
    temporaryinvoices TI
LEFT JOIN 
    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
LEFT JOIN 
    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
LEFT JOIN 
    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
LEFT JOIN 
    receipts R ON RR.ReceiptID = R.ReceiptID
LEFT JOIN 
    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
LEFT JOIN 
    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
LEFT JOIN
    customers C ON TI.CustomerID = C.CustomerID
WHERE 
    C.Cname LIKE @SearchTerm OR RI.ExternalBillName LIKE @SearchTerm OR TI.Ornc_No LIKE @SearchTerm OR R.ReceiptNumber LIKE @SearchTerm
GROUP BY 
    TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, TI.DiscountAmount, TI.DepositAmount, RI.ExternalBillName, R.ReceiptNumber;
";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable temporaryInvoicesWithStatus = new DataTable();
                    adapter.Fill(temporaryInvoicesWithStatus);

                    conn.CloseConnection();
                    return temporaryInvoicesWithStatus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }

        }
        public static DataTable GetInvoicePaymentStatus(string searchTerm)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                    SELECT 
                        TI.TemporaryInvoiceID,
                        RI.RealInvoiceID,
                        RI.CustomerID,
                        C.Ccode,
                        C.Cname AS CustomerName,
                        TI.IssueDate AS TemporaryInvoiceDate,
                        TI.TotalAmount AS TemporaryTotalAmount,
                        TI.Details AS TemporaryDetails,
                        RI.InvoiceDate AS RealInvoiceDate,
                        RI.TotalAmount AS RealTotalAmount,
                        RI.Details AS RealDetails,
                        U.Name AS CreatedByName,
                        COALESCE(RI.DiscountAmount, 0) AS RealDiscountAmount, 
                        COALESCE(TI.DiscountAmount, 0) AS TemporaryDiscountAmount, 
                        SUM(DR.DiscountAmount) AS TotalDiscountUsed,
                        DR.TransactionDate AS DiscountTransactionDate,
                        CASE
                            WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= RI.DiscountAmount THEN 'Fully Paid'
                            WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                            ELSE 'Not Paid'
                        END AS DiscountStatus,
                        COALESCE(RI.DepositAmount, 0) AS RealDepositAmount, 
                        COALESCE(TI.DepositAmount, 0) AS TemporaryDepositAmount, 
                        SUM(DepS.DepositAmount) AS TotalDepositUsed,
                        SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
                        DepS.TransactionDate AS DepositTransactionDate,
                        CASE
                            WHEN SUM(DepS.DepositAmount) >= RI.DepositAmount THEN 'Fully Paid'
                            WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                            ELSE 'Not Paid'
                        END AS DepositStatus,
                        CASE
                            WHEN RI.IsVoided = 1 THEN 'Voided'
                            ELSE 'Normal'
                        END AS VoidStatus,
                        R.ReceiptNumber,
                        R.CreatedDate, -- Include the CreatedDate from the receipts table
                        COALESCE(R.NetCash, 0) + COALESCE(R.WithHoldingTax, 0) + COALESCE(R.Fee, 0) AS TotalPayment,
                        RI.OutstandingDebt
                    FROM 
                        temporaryinvoices TI
                    LEFT JOIN 
                        realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
                    LEFT JOIN 
                        realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
                    LEFT JOIN 
                        receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
                    LEFT JOIN 
                        receipts R ON RR.ReceiptID = R.ReceiptID
                    LEFT JOIN 
                        discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                    LEFT JOIN 
                        deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                    LEFT JOIN
                        customers C ON TI.CustomerID = C.CustomerID
                    LEFT JOIN
                        users U ON TI.CreateByUser = U.UserID
                    WHERE 
                        C.Ccode LIKE @SearchTerm OR 
                        C.Cname LIKE @SearchTerm OR 
                        RI.ExternalBillName LIKE @SearchTerm OR 
                        RI.Details LIKE @SearchTerm OR
                        TI.Details LIKE @SearchTerm
                    GROUP BY 
                        TI.TemporaryInvoiceID, RI.RealInvoiceID, RI.CustomerID, C.Cname, 
                        TI.IssueDate, RI.InvoiceDate, 
                        TI.TotalAmount, RI.TotalAmount, 
                        TI.Details, RI.Details, 
                        R.CreatedDate, RI.DiscountAmount, TI.DiscountAmount, 
                        RI.DepositAmount, TI.DepositAmount, 
                        U.Name, RI.IsVoided, R.ReceiptNumber, 
                        R.NetCash, R.WithHoldingTax, R.Fee, RI.OutstandingDebt;
                ";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable invoiceStatus = new DataTable();
                    adapter.Fill(invoiceStatus);

                    conn.CloseConnection();
                    return invoiceStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static (DataTable notPaidTemporaryInvoices, decimal totalAmountSum, decimal discountAmountSum, decimal depositAmountSum) GetNotPaidTemporaryInvoices(string searchTerm, DateTime? startDate = null, DateTime? endDate = null)
        {
            DataTable notPaidTemporaryInvoices = new DataTable();
            decimal totalAmountSum = 0;
            decimal discountAmountSum = 0;
            decimal depositAmountSum = 0;

            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"
                SELECT 
                    TI.TemporaryInvoiceID,
                    TI.CustomerID,
                    C.Ccode,
                    C.Cname AS CustomerName,
                    TI.IssueDate,
                    TI.TotalAmount,
                    TI.Details,
                    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
                    COALESCE(SUM(DR.DiscountAmount), 0) AS TotalDiscountUsed,
                    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
                    COALESCE(SUM(DepS.DepositAmount), 0) AS TotalDepositUsed,
                    CASE
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DiscountStatus,
                    CASE
                        WHEN COALESCE(SUM(DepS.DepositAmount), 0) >= TI.DepositAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DepS.DepositAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DepositStatus,
                    CASE
                        WHEN TI.IsVoided = 1 THEN 'Voided'
                        ELSE 'Normal'
                    END AS VoidStatus,
                    R.ReceiptNumber,
                    COALESCE(R.NetCash, 0) + COALESCE(R.WithHoldingTax, 0) + COALESCE(R.Fee, 0) AS TotalPayment,
                    R.CreatedDate
                FROM 
                    temporaryinvoices TI
                LEFT JOIN 
                    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
                LEFT JOIN 
                    receipts_realinvoices RR ON RTI.RealInvoiceID = RR.RealInvoiceID
                LEFT JOIN 
                    receipts R ON RR.ReceiptID = R.ReceiptID
                LEFT JOIN 
                    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                LEFT JOIN 
                    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                LEFT JOIN
                    customers C ON TI.CustomerID = C.CustomerID
                WHERE 
                    (C.Ccode LIKE @SearchTerm OR 
                    C.Cname LIKE @SearchTerm OR 
                    TI.Details LIKE @SearchTerm)
                    AND RTI.RealInvoiceID IS NULL
                    AND TI.IsVoided = 0
            ";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND TI.IssueDate BETWEEN @StartDate AND @EndDate";
                    }

                    query += @"
                GROUP BY 
                    TI.TemporaryInvoiceID, TI.CustomerID, C.Ccode, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, 
                    TI.DiscountAmount, TI.DepositAmount, TI.IsVoided, R.ReceiptNumber, R.NetCash, R.WithHoldingTax, R.Fee, R.CreatedDate
                HAVING 
                    (COALESCE(SUM(DR.DiscountAmount), 0) < TI.DiscountAmount 
                    OR COALESCE(SUM(DepS.DepositAmount), 0) < TI.DepositAmount)
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(notPaidTemporaryInvoices);

                    // Calculate sums
                    foreach (DataRow row in notPaidTemporaryInvoices.Rows)
                    {
                        totalAmountSum += row.Field<decimal>("TotalAmount");
                        discountAmountSum += row.Field<decimal>("DiscountAmount");
                        depositAmountSum += row.Field<decimal>("DepositAmount");
                    }

                    conn.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (notPaidTemporaryInvoices, totalAmountSum, discountAmountSum, depositAmountSum);
        }

        public static DataTable GetNotPaidTemporaryRealInvoices()
        {
            DataTable notPaidInvoices = new DataTable();

            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    string query = @"
                SELECT 
                    'Temporary' AS InvoiceType,
                    C.Ccode,
                    C.Cname AS CustomerName,
                    TI.IssueDate AS InvoiceDate,
                    TI.TotalAmount,
                    TI.Details,
                    TI.Ornc_No,
                    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
                    SUM(DR.DiscountAmount) AS TotalDiscountUsed,
                    DR.TransactionDate AS DiscountTransactionDate,
                    CASE
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DiscountStatus,
                    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
                    SUM(DepS.DepositAmount) AS TotalDepositUsed,
                    SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
                    DepS.TransactionDate AS DepositTransactionDate,
                    CASE
                        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
                        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DepositStatus,
                    RI.ExternalBillName,
                    R.ReceiptNumber  
                FROM 
                    temporaryinvoices TI
                LEFT JOIN 
                    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
                LEFT JOIN 
                    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
                LEFT JOIN 
                    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
                LEFT JOIN 
                    receipts R ON RR.ReceiptID = R.ReceiptID
                LEFT JOIN 
                    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                LEFT JOIN 
                    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                LEFT JOIN
                    customers C ON TI.CustomerID = C.CustomerID
                GROUP BY 
                    TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, TI.DiscountAmount, TI.DepositAmount, RI.ExternalBillName, R.ReceiptNumber
                HAVING 
                    DepositStatus = 'Not Paid' AND DiscountStatus = 'Not Paid'

                UNION

                SELECT 
                    'Real' AS InvoiceType,
                    C.Ccode,
                    C.Cname AS CustomerName,
                    RI.InvoiceDate AS InvoiceDate,
                    RI.TotalAmount,
                    RI.Details,
                    NULL AS Ornc_No,
                    COALESCE(RI.DiscountAmount, 0) AS DiscountAmount,
                    SUM(DR.DiscountAmount) AS TotalDiscountUsed,
                    DR.TransactionDate AS DiscountTransactionDate,
                    CASE
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= RI.DiscountAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DiscountStatus,
                    COALESCE(RI.DepositAmount, 0) AS DepositAmount,
                    SUM(DepS.DepositAmount) AS TotalDepositUsed,
                    SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
                    DepS.TransactionDate AS DepositTransactionDate,
                    CASE
                        WHEN SUM(DepS.DepositAmount) >= RI.DepositAmount THEN 'Fully Paid'
                        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DepositStatus,
                    RI.ExternalBillName,
                    R.ReceiptNumber
                FROM 
                    realinvoices RI
                LEFT JOIN 
                    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
                LEFT JOIN 
                    receipts R ON RR.ReceiptID = R.ReceiptID
                LEFT JOIN 
                    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                LEFT JOIN 
                    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                LEFT JOIN
                    customers C ON RI.CustomerID = C.CustomerID
                GROUP BY 
                    RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.DiscountAmount, RI.DepositAmount  
                HAVING 
                    DiscountStatus = 'Not Paid' AND DepositStatus = 'Not Paid';
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(notPaidInvoices);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return notPaidInvoices;
        }
        public static DataTable SearchNotPaidTemporaryRealInvoices(string searchTerm)
        {
            DataTable notPaidInvoices = new DataTable();

            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    string query = @"
                    SELECT 
    'Temporary' AS InvoiceType,
    TI.TemporaryInvoiceID AS InvoiceID,
    TI.CustomerID,
    C.Ccode,
    C.Cname AS CustomerName,
    TI.IssueDate AS InvoiceDate,
    TI.TotalAmount,
    TI.Details,
    TI.Ornc_No,
    RI.ExternalBillName,
    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
    SUM(DR.DiscountAmount) AS TotalDiscountUsed,
    DR.TransactionDate AS DiscountTransactionDate,
    CASE
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DiscountStatus,
    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
    SUM(DepS.DepositAmount) AS TotalDepositUsed,
    SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
    DepS.TransactionDate AS DepositTransactionDate,
    CASE
        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DepositStatus,  
    R.ReceiptNumber  
FROM 
    temporaryinvoices TI
LEFT JOIN 
    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
LEFT JOIN 
    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
LEFT JOIN 
    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
LEFT JOIN 
    receipts R ON RR.ReceiptID = R.ReceiptID
LEFT JOIN 
    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
LEFT JOIN 
    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
LEFT JOIN
    customers C ON TI.CustomerID = C.CustomerID
WHERE 
    (C.Ccode LIKE @SearchTerm OR C.Cname LIKE @SearchTerm)
GROUP BY 
    TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, RI.ExternalBillName, R.ReceiptNumber
HAVING 
    DepositStatus = 'Not Paid' AND DiscountStatus = 'Not Paid'

UNION

SELECT 
    'Real' AS InvoiceType,
    RI.RealInvoiceID AS InvoiceID,
    RI.CustomerID,
    C.Ccode,
    C.Cname AS CustomerName,
    RI.InvoiceDate AS InvoiceDate,
    RI.TotalAmount,
    RI.Details,
    NULL AS Ornc_No,
    RI.ExternalBillName,
    COALESCE(RI.DiscountAmount, 0) AS DiscountAmount,
    SUM(DR.DiscountAmount) AS TotalDiscountUsed,
    DR.TransactionDate AS DiscountTransactionDate,
    CASE
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= RI.DiscountAmount THEN 'Fully Paid'
        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DiscountStatus,
    COALESCE(RI.DepositAmount, 0) AS DepositAmount,
    SUM(DepS.DepositAmount) AS TotalDepositUsed,
    SUM(DepS.DepositAmount) * 0.8 AS AmountToPay,
    DepS.TransactionDate AS DepositTransactionDate,
    CASE
        WHEN SUM(DepS.DepositAmount) >= RI.DepositAmount THEN 'Fully Paid'
        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
        ELSE 'Not Paid'
    END AS DepositStatus,
    R.ReceiptNumber
FROM 
    realinvoices RI
LEFT JOIN 
    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
LEFT JOIN 
    receipts R ON RR.ReceiptID = R.ReceiptID
LEFT JOIN 
    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
LEFT JOIN 
    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
LEFT JOIN
    customers C ON RI.CustomerID = C.CustomerID
WHERE 
    (C.Ccode LIKE @SearchTerm OR C.Cname LIKE @SearchTerm)
GROUP BY 
    RI.RealInvoiceID, RI.CustomerID, C.Cname, RI.InvoiceDate, RI.TotalAmount, RI.ExternalBillName, RI.DiscountAmount, RI.DepositAmount  
HAVING 
    DiscountStatus = 'Not Paid' AND DepositStatus = 'Not Paid';
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(notPaidInvoices);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return notPaidInvoices;
        }

        public void VoidTemporaryInvoice(int temporaryInvoiceID)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    // Update the status of the temporary invoice to voided
                    string query = "UPDATE temporaryinvoices SET IsVoided = 1 WHERE TemporaryInvoiceID = @TemporaryInvoiceID";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);
                    cmd.ExecuteNonQuery();

                    conn.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsVoidedTemporary(int temporaryID)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                string query = "SELECT COUNT(*) FROM temporaryinvoices WHERE TemporaryInvoiceID = @TemporaryID AND IsVoided = 1";

                try
                {
                    if (dbConnect.OpenConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            cmd.Parameters.AddWithValue("@TemporaryID", temporaryID);
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            dbConnect.CloseConnection();

                            // Check if the count is greater than 0
                            return count > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return false;
            }
        }
        public static bool IsPaidFully(int temporaryID)
        {
            using (DBconnect dbconn = new DBconnect())
            {
                try
                {
                    dbconn.OpenConnection();

                    string query = @"
                SELECT
                    CASE
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DiscountStatus,
                    CASE
                        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
                        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DepositStatus
                FROM 
                    temporaryinvoices TI
                LEFT JOIN 
                    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID
                LEFT JOIN 
                    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
                LEFT JOIN 
                    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
                LEFT JOIN 
                    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                LEFT JOIN 
                    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                WHERE 
                    TI.TemporaryInvoiceID = @TemporaryID";

                    MySqlCommand cmd = new MySqlCommand(query, dbconn.GetConnection);
                    cmd.Parameters.AddWithValue("@TemporaryID", temporaryID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string discountStatus = reader.GetString("DiscountStatus");
                            string depositStatus = reader.GetString("DepositStatus");

                            // Debugging output
                            //Console.WriteLine($"DiscountStatus: {discountStatus}, DepositStatus: {depositStatus}");

                            // Check if both DiscountStatus and DepositStatus are 'Fully Paid'
                            return discountStatus == "Fully Paid" || depositStatus == "Fully Paid";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
            }

            return false; // Default to false if any error occurs
        }

        public static DataTable GetNotPaidTemporaryInvoicesByDate(DateTime selectedDate)
        {
            DataTable notPaidInvoices = new DataTable();

            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    string query = @"
                SELECT 
                    'ORNC' AS InvoiceType,
                    C.Ccode,
                    C.Cname AS CustomerName,
                    TI.IssueDate,
                    TI.TotalAmount,
                    TI.Details,
                    TI.Ornc_No,
                    RI.ExternalBillName, 
                    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
                    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
                    CASE
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DiscountStatus,
                    CASE
                        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
                        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DepositStatus
                FROM 
                    temporaryinvoices TI
                LEFT JOIN 
                    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID    
                LEFT JOIN 
                    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
                LEFT JOIN 
                    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
                LEFT JOIN 
                    receipts R ON RR.ReceiptID = R.ReceiptID
                LEFT JOIN 
                    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                LEFT JOIN 
                    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                LEFT JOIN
                    customers C ON TI.CustomerID = C.CustomerID
                WHERE
                    TI.IssueDate = @SelectedDate
                    AND R.ReceiptID IS NULL -- Exclude rows with non-null ReceiptID
                    AND RTI.RealInvoiceID IS NULL -- Exclude rows that can be joined with realinvoices
                    AND TI.IsVoided != 1 -- Exclude voided rows
                GROUP BY 
                    TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, TI.DiscountAmount, TI.DepositAmount, RI.ExternalBillName;
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(notPaidInvoices);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return notPaidInvoices;
        }
        public static DataTable GetNotPaidTemporaryInvoicesByMonth(int selectedMonth)
        {
            DataTable notPaidInvoices = new DataTable();

            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();

                    string query = @"
                SELECT 
                    'ORNC' AS InvoiceType,
                    C.Ccode,
                    C.Cname AS CustomerName,
                    TI.IssueDate,
                    TI.TotalAmount,
                    TI.Details,
                    TI.Ornc_No,
                    RI.ExternalBillName, 
                    COALESCE(TI.DiscountAmount, 0) AS DiscountAmount,
                    COALESCE(TI.DepositAmount, 0) AS DepositAmount,
                    CASE
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) >= TI.DiscountAmount THEN 'Fully Paid'
                        WHEN COALESCE(SUM(DR.DiscountAmount), 0) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DiscountStatus,
                    CASE
                        WHEN SUM(DepS.DepositAmount) >= TI.DepositAmount THEN 'Fully Paid'
                        WHEN SUM(DepS.DepositAmount) > 0 THEN 'Partially Paid'
                        ELSE 'Not Paid'
                    END AS DepositStatus
                FROM 
                    temporaryinvoices TI
                LEFT JOIN 
                    realinvoices_temporaryinvoices RTI ON TI.TemporaryInvoiceID = RTI.TemporaryInvoiceID    
                LEFT JOIN 
                    realinvoices RI ON RTI.RealInvoiceID = RI.RealInvoiceID
                LEFT JOIN 
                    receipts_realinvoices RR ON RI.RealInvoiceID = RR.RealInvoiceID
                LEFT JOIN 
                    receipts R ON RR.ReceiptID = R.ReceiptID
                LEFT JOIN 
                    discounts_receipts DR ON RR.ReceiptID = DR.ReceiptID
                LEFT JOIN 
                    deposits_receipts DepS ON RR.ReceiptID = DepS.ReceiptID
                LEFT JOIN
                    customers C ON TI.CustomerID = C.CustomerID
                WHERE 
                    MONTH(TI.IssueDate) = @SelectedMonth  -- Filter by selected month
                    AND R.ReceiptID IS NULL  -- Exclude rows with non-null ReceiptID
                    AND RTI.RealInvoiceID IS NULL  -- Exclude rows that can be joined with realinvoices
                    AND TI.IsVoided != 1  -- Exclude voided rows
                GROUP BY 
                    TI.TemporaryInvoiceID, TI.CustomerID, C.Cname, TI.IssueDate, TI.TotalAmount, TI.Details, TI.Ornc_No, TI.DiscountAmount, TI.DepositAmount, RI.ExternalBillName;
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@SelectedMonth", selectedMonth);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(notPaidInvoices);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return notPaidInvoices;
        }

    }
}

