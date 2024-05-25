using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
namespace GDAPPWIN
{
    internal class ColumnHeader
    {
        public string Name { get; }
        public string DisplayName { get; }

        public ColumnHeader(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }
    }
    internal class ReceiptDataAccess
    {
        // Properties
        public int ReceiptID { get; set; }
        public int CustomerID { get; set; }
        // Add more properties for other columns as needed
        public string? Ccode { get; set; }
        public string? CustomerName { get; set; }
        public string? ReceiptNumber { get; set; }
        public decimal NetCash { get; set; }
        public decimal WithHoldingTax { get; set; }
        public decimal Fee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal DepositPayment { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Remark { get; set; }
        public string? CreateByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? InvoiceType { get; set; }

        // Add more properties for other columns as needed
        public static readonly List<ColumnHeader> ColumnHeaders = new List<ColumnHeader>
        {
        new ColumnHeader("ReceiptID", "Receipt ID"),
        new ColumnHeader("CustomerID", "ID (ลูกค้า)"),
        // Add more column headers for additional properties
        new ColumnHeader("Ccode", "รหัสลูกค้า"),
        new ColumnHeader("CustomerName", "ชื่อลูกค้า"),
        new ColumnHeader("ReceiptNumber", "เลขที่ใบเสร็จ"),
        new ColumnHeader("NetCash", "เงินรับสุทธิ"),
        new ColumnHeader("WithHoldingTax", "หัก ณ ที่จ่าย"),
        new ColumnHeader("Fee", "ค่าธรรมเนียม"),
        new ColumnHeader("DiscountAmount", "ส่วนลด"),
        new ColumnHeader("DepositAmount", "ยอดรวม(เงินฝาก)"),
        new ColumnHeader("DepositPayment", "ยอดที่ต้องจ่าย(20%)"),
        new ColumnHeader("ReceiptDate", "วันที่"),
        new ColumnHeader("PaymentMethod", "วิธีจ่ายเงิน"),
        new ColumnHeader("Remark", "หมายเหตุ"),
        new ColumnHeader("CreateByUser", "พนักงาน"),
        new ColumnHeader("CreateUser", "พนักงาน" ),
        new ColumnHeader("CreatedDate", "วันที่ทำธุรกรรม"),
        new ColumnHeader("InvoiceType", "ประเภทบิล"),
        new ColumnHeader("IssueDate", "วันที่"),
        new ColumnHeader("TotalAmount", "จำนวนเงิน"),
        new ColumnHeader("Details", "รายละเอียด"),
        new ColumnHeader("Ornc_No", "เลขที่บิล (ORNC)"),
        new ColumnHeader("ExternalBillName", "เลขที่บิล (ORVC)"),
        new ColumnHeader("Status", "สถานะใบเสร็จ")
        // Add more column headers for additional properties
        };
        public static readonly List<string> ReceiptDecimalFormat = new List<string>
        {
            "TotalAmount", "DiscountAmount", "DepositAmount", "DepositPayment"
        };
        // Method to set column headers
        public static void SetDataThaiColumnHeaders(DataGridView dataGridView)
        {
            foreach (var columnHeader in ColumnHeaders)
            {
                if (dataGridView.Columns.Contains(columnHeader.Name))
                {
                    dataGridView.Columns[columnHeader.Name].HeaderText = columnHeader.DisplayName;
                }
            }
        }

        public static void SetDecimalFormatInDataGridViewColumns(DataGridView dataGridView)
        {
            List<string> decimalColumns = new List<string> { "Netcash", "WithHoldingTax", "DiscountAmount", "DepositAmount", "AmountToPay" };
            foreach (string columnName in decimalColumns)
            {
                if (dataGridView.Columns.Contains(columnName))
                {
                    dataGridView.Columns[columnName].DefaultCellStyle.Format = "#,##0.00";
                }
            }
        }
        public static DataTable SearchReceiptsData(string searchText)
        {
            DataTable dataTable = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = @"
            SELECT 
                R.ReceiptID, 
                R.CustomerID,
                C.Ccode,
                C.Cname AS CustomerName,
                R.ReceiptNumber, 
                R.NetCash, 
                R.WithHoldingTax,
                R.Fee,
                R.DiscountAmount,
                R.DepositAmount,
                (R.DepositAmount * 0.8) AS AmountToPay,
                R.ReceiptDate, 
                R.PaymentMethod, 
                R.Remark,
                U.Ucode AS CreateUser, 
                R.CreatedDate, 
                CASE 
                    WHEN R.Status IS NULL OR R.Status = 0 THEN 'Waiting' 
                    WHEN R.Status = 1 THEN 'Approve' 
                    ELSE R.Status 
                END AS Status,
                CASE 
                    WHEN R.IsVoided = 1 THEN 'Voided' 
                    ELSE 'Normal' 
                END AS VoidedStatus 
            FROM 
                receipts R
            INNER JOIN 
                customers C ON R.CustomerID = C.CustomerID
            LEFT JOIN 
                users U ON R.CreateByUser = U.UserID
            WHERE 
                C.Cname LIKE @SearchText OR 
                R.ReceiptNumber LIKE @SearchText OR
                C.Ccode LIKE @SearchText
        ";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dataTable);

                conn.CloseConnection();
            }

            return dataTable;
        }

        public static DataTable LoadReceiptsData(string searchText)
        {
            DataTable dataTable = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = @"SELECT R.ReceiptID, 
                                R.CustomerID, 
                                R.ReceiptNumber, 
                                R.NetCash, 
                                R.WithHoldingTax, 
                                R.DiscountAmount, 
                                R.ReceiptDate, 
                                R.PaymentMethod, 
                                R.Remark, 
                                R.CreateByUser, 
                                R.CreatedDate, 
                                CASE 
                                    WHEN R.Status IS NULL OR R.Status = 0 THEN 'Waiting' 
                                    WHEN R.Status = 1 THEN 'Approved' 
                                    ELSE R.Status 
                                END AS Status
                            FROM 
                                receipts R
                            INNER JOIN 
                                customers C ON R.CustomerID = C.CustomerID
                            WHERE 
                                C.Cname LIKE @SearchText OR R.ReceiptNumber LIKE @SearchText";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dataTable);

                conn.CloseConnection();
            }

            return dataTable;
        }
        public static DataTable GetAllReceipts()
        {
            DataTable dataTable = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = @"SELECT 
            R.ReceiptID, 
            R.CustomerID,
            C.Ccode,
            C.Cname AS CustomerName,
            R.ReceiptNumber, 
            R.NetCash, 
            R.WithHoldingTax,
            R.Fee,
            R.DiscountAmount,
            R.DepositAmount,
            (R.DepositAmount * 0.8) AS AmountToPay,
            R.ReceiptDate, 
            R.PaymentMethod, 
            R.Remark,
            U.Ucode AS CreateUser, 
            R.CreatedDate, 
            CASE 
                WHEN R.Status IS NULL OR R.Status = 0 THEN 'Waiting' 
                WHEN R.Status = 1 THEN 'Approve' 
                ELSE R.Status 
            END AS Status,
            CASE 
                WHEN R.IsVoided = 1 THEN 'Voided' 
                ELSE 'Normal' 
            END AS VoidedStatus
        FROM 
            receipts R
        INNER JOIN 
            customers C ON R.CustomerID = C.CustomerID
        LEFT JOIN 
            users U ON R.CreateByUser = U.UserID;";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                conn.CloseConnection();
            }

            return dataTable;
        }

        public static DataTable GetUnapprovedReceipts()
        {
            DataTable dataTable = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = @"SELECT 
                            R.ReceiptID, 
                            R.CustomerID,
                            C.Cname AS CustomerName,
                            R.ReceiptNumber, 
                            R.NetCash, 
                            R.WithHoldingTax,
                            R.Fee,
                            R.DiscountAmount,
                            R.DepositAmount,
                            (R.DepositAmount * 0.8) AS DepositPayment,  -- คำนวณค่า DepositPayment โดยลบด้วย 20%
                            R.ReceiptDate, 
                            R.PaymentMethod, 
                            R.Remark, 
                            R.CreateByUser, 
                            R.CreatedDate, 
                            CASE 
                                WHEN R.Status IS NULL OR R.Status = 0 THEN 'Waiting' 
                                WHEN R.Status = 1 THEN 'Approve' 
                                ELSE R.Status 
                            END AS Status
                        FROM 
                            receipts R
                        INNER JOIN 
                            customers C ON R.CustomerID = C.CustomerID
                        WHERE 
                            R.Status IS NULL OR R.Status = 0";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                conn.CloseConnection();
            }

            return dataTable;
        }
        public static DataTable LoadUnapprovedReceiptsData(string searchText)
        {
            DataTable dataTable = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = @"SELECT 
                        R.ReceiptID, 
                        R.CustomerID,
                        C.Cname AS CustomerName,
                        R.ReceiptNumber, 
                        R.NetCash, 
                        R.WithHoldingTax, 
                        R.DiscountAmount,
                        R.DepositAmount,
                        (R.DepositAmount * 0.8) AS DepositPayment,  -- คำนวณค่า DepositPayment โดยลบด้วย 20%
                        R.ReceiptDate, 
                        R.PaymentMethod, 
                        R.Remark, 
                        R.CreateByUser, 
                        R.CreatedDate, 
                        CASE 
                            WHEN R.Status IS NULL OR R.Status = 0 THEN 'Waiting' 
                            WHEN R.Status = 1 THEN 'Approved' 
                            ELSE R.Status 
                        END AS Status
                    FROM 
                        receipts R
                    INNER JOIN 
                        customers C ON R.CustomerID = C.CustomerID
                    WHERE 
                        (C.Cname LIKE @SearchText OR R.ReceiptNumber LIKE @SearchText)
                        AND (R.Status IS NULL OR R.Status = 0)";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dataTable);

                conn.CloseConnection();
            }

            return dataTable;
        }
        public static DataTable LoadReceiptsDataReport(string status)
        {
            DataTable dataTable = new DataTable();

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = string.Empty;

                if (status == "paid")
                {
                    query = @"SELECT 
                        r.ReceiptID, 
                        c.Cname AS CustomerName, 
                        r.ReceiptNumber, 
                        r.NetCash, 
                        r.WithHoldingTax, 
                        r.DiscountAmount AS ReceiptDiscountAmount, 
                        r.ReceiptDate, 
                        r.PaymentMethod, 
                        r.Remark, 
                        r.CreateByUser, 
                        r.CreatedDate, 
                        r.Status,
                        COALESCE(SUM(dr.DiscountAmount), 0) AS TotalDiscountPaid
                    FROM 
                        receipts r 
                    INNER JOIN 
                        customers c ON r.CustomerID = c.CustomerID 
                    LEFT JOIN 
                        discounts_receipts dr ON r.ReceiptID = dr.ReceiptID
                    GROUP BY 
                        r.ReceiptID, 
                        CustomerName, 
                        r.ReceiptNumber, 
                        r.NetCash, 
                        r.WithHoldingTax, 
                        r.DiscountAmount, 
                        r.ReceiptDate, 
                        r.PaymentMethod, 
                        r.Remark, 
                        r.CreateByUser, 
                        r.CreatedDate, 
                        r.Status
                    HAVING 
                        r.DiscountAmount = COALESCE(SUM(dr.DiscountAmount), 0)";
                }
                else if (status == "unpaid")
                {
                    query = @"SELECT 
                        r.ReceiptID, 
                        c.Cname AS CustomerName, 
                        r.ReceiptNumber, 
                        r.NetCash, 
                        r.WithHoldingTax, 
                        r.DiscountAmount AS ReceiptDiscountAmount, 
                        r.ReceiptDate, 
                        r.PaymentMethod, 
                        r.Remark, 
                        r.CreateByUser, 
                        r.CreatedDate, 
                        r.Status
                    FROM 
                        receipts r 
                    INNER JOIN 
                        customers c ON r.CustomerID = c.CustomerID 
                    LEFT JOIN 
                        discounts_receipts dr ON r.ReceiptID = dr.ReceiptID
                    WHERE 
                        dr.ReceiptID IS NULL";
                }
                else if (status == "partial")
                {
                    query = @"SELECT 
                        r.ReceiptID, 
                        c.Cname AS CustomerName, 
                        r.ReceiptNumber, 
                        r.NetCash, 
                        r.WithHoldingTax, 
                        r.DiscountAmount AS ReceiptDiscountAmount, 
                        r.ReceiptDate, 
                        r.PaymentMethod, 
                        r.Remark, 
                        r.CreateByUser, 
                        r.CreatedDate, 
                        r.Status,
                        COALESCE(SUM(dr.DiscountAmount), 0) AS TotalDiscountPaid
                    FROM 
                        receipts r 
                    INNER JOIN 
                        customers c ON r.CustomerID = c.CustomerID 
                    LEFT JOIN 
                        discounts_receipts dr ON r.ReceiptID = dr.ReceiptID
                    GROUP BY 
                        r.ReceiptID, 
                        c.Cname, 
                        r.ReceiptNumber, 
                        r.NetCash, 
                        r.WithHoldingTax, 
                        r.DiscountAmount, 
                        r.ReceiptDate, 
                        r.PaymentMethod, 
                        r.Remark, 
                        r.CreateByUser, 
                        r.CreatedDate, 
                        r.Status
                    HAVING 
                        COALESCE(SUM(dr.DiscountAmount), 0) < r.DiscountAmount";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dataTable);

                conn.CloseConnection();
            }

            return dataTable;
        }
        
        public static bool CheckRealInvoiceExistence(int realInvoiceID)
        {
            try
            {
                using (DBconnect connection = new DBconnect())
                {
                    connection.OpenConnection();

                    // สร้างคำสั่ง SQL สำหรับตรวจสอบการมีข้อมูล RealInvoiceID ในตาราง receipts_realinvoices
                    string query = "SELECT COUNT(*) FROM receipts_realinvoices WHERE RealInvoiceID = @RealInvoiceID";
                    MySqlCommand cmd = new MySqlCommand(query, connection.GetConnection);
                    cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                    // ExecuteScalar ในกรณีที่มีการเลือก COUNT(*), จะคืนค่าเป็นจำนวนแถวที่ตรงเงื่อนไข
                    int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                    // ถ้ามีแถวที่ตรงเงื่อนไข (มี RealInvoiceID ที่ระบุ) ในตาราง receipts_realinvoices
                    // ให้คืนค่า true
                    // ไม่มี ให้คืนค่า false
                    return rowCount > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in CheckRealInvoiceExistence: {ex.Message}");
                return false;
            }
        }
        public static bool UpdateReceiptStatus(int receiptID)
        {
            try
            {
                using (DBconnect conn = new DBconnect())
                {
                    conn.OpenConnection();
                    string query = @"UPDATE receipts SET Status = 1 WHERE ReceiptID = @ReceiptID";
                    MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                    cmd.Parameters.AddWithValue("@ReceiptID", receiptID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.CloseConnection();

                    // Check if the update was successful
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating receipt status: {ex.Message}");
                return false;
            }
        }
        public static bool IsRealInvoiceAssociated(int realInvoiceID)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                string query = "SELECT COUNT(*) FROM receipts_realinvoices WHERE RealInvoiceID = @RealInvoiceID";

                try
                {
                    if (dbConnect.OpenConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            dbConnect.CloseConnection();
                            if (count > 0)
                            {
                                return true;
                            }
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
        public static (bool canPayAgain, decimal totalAmountPaid) CanPayRealInvoiceAgain(int realInvoiceID)
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                string query = @"
            SELECT ri.TotalAmount, 
                   COALESCE(SUM(r.NetCash + r.WithHoldingTax + r.Fee), 0) AS TotalAmountPaid
            FROM realinvoices ri
            LEFT JOIN receipts_realinvoices rri ON ri.RealInvoiceID = rri.RealInvoiceID
            LEFT JOIN receipts r ON rri.ReceiptID = r.ReceiptID
            WHERE ri.RealInvoiceID = @RealInvoiceID";

                // Initialize variables
                bool canPayAgain = false;
                decimal totalAmountPaid = 0;
                decimal totalAmount = 0;
                try
                {
                    if (dbConnect.OpenConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            cmd.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalAmount = reader.GetDecimal("TotalAmount");
                                    totalAmountPaid = reader.GetDecimal("TotalAmountPaid");

                                    // Check if totalAmountPaid is greater than or equal to totalAmount
                                    canPayAgain = totalAmountPaid >= totalAmount;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in CanPayRealInvoiceAgain: {ex.Message}");
                    // Handle exceptions here if necessary
                }

                // Return the tuple containing canPayAgain and totalAmountPaid
                return (canPayAgain, totalAmountPaid);
            }
        }

        public static bool IsReceiptNumberExists(string receiptNumber)
        {
            bool exists = false;

            string query = "SELECT COUNT(*) FROM receipts WHERE ReceiptNumber = @ReceiptNumber";

            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();

                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@ReceiptNumber", receiptNumber);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        exists = (count > 0);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log or display error message)
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return exists;
        }

        public static bool UpdateReceiptIsVoided(int receiptID)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();
                string query = "UPDATE receipts SET IsVoided = 1 WHERE ReceiptID = @ReceiptID";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                int rowsAffected = cmd.ExecuteNonQuery();

                conn.CloseConnection();

                return rowsAffected > 0;
            }
        }
        public static void UpdateReceipt(int receiptID, string receiptNumber, decimal netCash, decimal withholdingTax, decimal fee, DateTime receiptDate, string paymentMethod, string remark)
        {
            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"UPDATE receipts 
                        SET ReceiptNumber = @ReceiptNumber, 
                            NetCash = @NetCash, 
                            WithHoldingTax = @WithHoldingTax, 
                            Fee = @Fee, 
                            ReceiptDate = @ReceiptDate, 
                            PaymentMethod = @PaymentMethod, 
                            Remark = @Remark
                        WHERE ReceiptID = @ReceiptID";

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);
                cmd.Parameters.AddWithValue("@ReceiptNumber", receiptNumber);
                cmd.Parameters.AddWithValue("@NetCash", netCash);
                cmd.Parameters.AddWithValue("@WithHoldingTax", withholdingTax);
                cmd.Parameters.AddWithValue("@Fee", fee);
                cmd.Parameters.AddWithValue("@ReceiptDate", receiptDate);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                cmd.Parameters.AddWithValue("@Remark", remark);

                cmd.ExecuteNonQuery();

                conn.CloseConnection();
            }
        }
        public static bool IsVoidedReceipt(int receiptID)
        {
            bool isVoided = false;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"
            SELECT COUNT(*) AS VoidedCount
            FROM receipts
            WHERE ReceiptID = @ReceiptID
            AND IsVoided = 1;
        ";

                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);

                // Execute the query and retrieve the count
                object result = cmd.ExecuteScalar();

                // Check if result is not null and convert it to int
                if (result != null && int.TryParse(result.ToString(), out int voidedCount))
                {
                    // If the count is greater than 0, the receipt is voided
                    isVoided = voidedCount > 0;
                }

                conn.CloseConnection();
            }

            return isVoided;
        }

    }
}


