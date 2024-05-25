using MySql.Data.MySqlClient;

namespace GDAPPWIN
{
    internal class RealInvoiceTemporaryInvoiceClass
    {
        public int RealInvoiceID { get; set; }
        public int? TemporaryInvoiceID { get; set; }

        public void InsertToDatabase()
        {
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();

                    // สร้างคำสั่ง SQL INSERT โดยใช้ RealInvoiceID และ TemporaryInvoiceID
                    string query = "INSERT INTO realinvoices_temporaryinvoices (RealInvoiceID, TemporaryInvoiceID) VALUES (@RealInvoiceID, @TemporaryInvoiceID)";

                    // สร้าง MySqlCommand และกำหนดค่าพารามิเตอร์
                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@RealInvoiceID", RealInvoiceID);
                        command.Parameters.AddWithValue("@TemporaryInvoiceID", TemporaryInvoiceID);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
        public static bool IsTemporaryInvoiceLinkedToRealInvoice(int temporaryInvoiceID)
        {
            bool isLinked = false;

            // ตรวจสอบว่า TemporaryInvoiceID ถูกผูกกับ RealInvoice หรือไม่
            using (DBconnect dbConnect = new DBconnect())
            {
                try
                {
                    dbConnect.OpenConnection();

                    // เขียนคำสั่ง SQL SELECT เพื่อตรวจสอบว่า TemporaryInvoiceID ถูกผูกกับ RealInvoice หรือไม่
                    string query = "SELECT COUNT(*) FROM realinvoices_temporaryinvoices WHERE TemporaryInvoiceID = @TemporaryInvoiceID";

                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@TemporaryInvoiceID", temporaryInvoiceID);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // หากพบว่ามี TemporaryInvoiceID ถูกผูกกับ RealInvoice ให้กำหนดค่า isLinked เป็น true
                        if (count > 0)
                        {
                            isLinked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return isLinked;
        }
        public static bool IsPaymentMadeWithNullTemporaryInvoice(int realInvoiceID)
        {
            bool paymentMade = false;

            using (DBconnect dbConnect = new DBconnect())
            {
                string query = @"
            SELECT 
                COUNT(*)
            FROM 
                realinvoices_temporaryinvoices
            WHERE 
                RealInvoiceID = @RealInvoiceID AND
                TemporaryInvoiceID IS NULL;";

                try
                {
                    dbConnect.OpenConnection();

                    using (MySqlCommand command = new MySqlCommand(query, dbConnect.GetConnection))
                    {
                        command.Parameters.AddWithValue("@RealInvoiceID", realInvoiceID);

                        int paymentCount = Convert.ToInt32(command.ExecuteScalar());

                        if (paymentCount > 0)
                        {
                            paymentMade = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return paymentMade;
        }


    }
}
