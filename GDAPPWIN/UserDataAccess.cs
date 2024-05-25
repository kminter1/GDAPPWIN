using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace GDAPPWIN
{
    public class UserData
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public partial class UserDataAccess
    {
        public static bool InsertUser(string name, string username, string password, string role)
        {
            // Check if the username already exists
            if (UsernameExists(username))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = $"INSERT INTO users (Name, UserName, Password, Role) VALUES ('{name}', '{username}', '{password}', '{role}')";

            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    if (dbConnect.OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private static bool UsernameExists(string username)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE UserName = '{username}'";

            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    if (dbConnect.OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public static bool UpdateUser(int userID, string name, string username, string password, string role)
        {
            string query = $"UPDATE users SET Name = '{name}', UserName = '{username}', Password = '{password}', Role = '{role}' WHERE UserID = {userID}";

            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    if (dbConnect.OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public static List<UserData> GetAllUsers()
        {
            List<UserData> userList = new List<UserData>();
            string query = "SELECT * FROM users";

            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    if (dbConnect.OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection);
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                UserData user = new UserData
                                {
                                    UserID = Convert.ToInt32(dataReader["UserID"]),
                                    Name = dataReader["Name"].ToString(),
                                    Username = dataReader["Username"].ToString(),
                                    Password = dataReader["Password"].ToString(),
                                    Role = dataReader["Role"].ToString()
                                };
                                userList.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return userList;
        }

        public static List<UserData> SearchUser(string searchTerm)
        {
            string query = @"
        SELECT * 
        FROM users 
        WHERE Name LIKE @SearchTerm 
        OR Username LIKE @SearchTerm";

            List<UserData> matchingUsers = new List<UserData>();

            try
            {
                using (DBconnect dbConnect = new DBconnect())
                {
                    if (dbConnect.OpenConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.GetConnection))
                        {
                            // Using parameterized query to prevent SQL injection
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    UserData user = new UserData
                                    {
                                        UserID = Convert.ToInt32(dataReader["UserID"]),
                                        Name = dataReader["Name"].ToString(),
                                        Username = dataReader["Username"].ToString(),
                                        Password = dataReader["Password"].ToString(),
                                        Role = dataReader["Role"].ToString()
                                    };
                                    matchingUsers.Add(user);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return matchingUsers;
        }


    }
}
