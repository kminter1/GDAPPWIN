using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPPWIN
{
    internal class DepositReceiptDataAccess
    {
        public static bool AddDepositReceiptLink(int depositID, int receiptID, decimal depositAmount, DateTime transactionDate)
        {
            bool success = false;

            using (DBconnect conn = new DBconnect())
            {
                conn.OpenConnection();

                string query = @"INSERT INTO deposits_receipts (ReceiptID, DepositID, DepositAmount, TransactionDate) 
                             VALUES (@ReceiptID, @DepositID, @DepositAmount, @TransactionDate)";
                MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection);
                cmd.Parameters.AddWithValue("@ReceiptID", receiptID);
                cmd.Parameters.AddWithValue("@DepositID", depositID);
                cmd.Parameters.AddWithValue("@DepositAmount", depositAmount);
                cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    success = true;
                }

                conn.CloseConnection();
            }

            return success;
        }
    }
}
