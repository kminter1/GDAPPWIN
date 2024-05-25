using System;
using MySql.Data.MySqlClient;

namespace GDAPPWIN
{
    public partial class FormLogin : Form
    {
        private DBconnect dbConnect;
        public FormLogin()
        {
            InitializeComponent();
            dbConnect = new DBconnect();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลจากช่องใส่ข้อมูล
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // ตรวจสอบการเข้าสู่ระบบ
            if (dbConnect.VerifyLogin(username, password))
            {
                // เข้าสู่ระบบสำเร็จ
                var result = dbConnect.GetRoleAndUserID(username);
                int userID = result.Item1;
                string role = result.Item2;

                // ตรวจสอบบทบาทของผู้ใช้
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Admin")
                    {
                        // เปิดหน้าจอหลักสำหรับผู้ดูแลระบบ
                        MainForm mainForm = new MainForm(userID, username, role);
                        mainForm.Show();
                    }
                    else if (role == "User")
                    {
                        // เปิดหน้าจอหลักสำหรับผู้ใช้
                        MainForm mainForm = new MainForm(userID, username, role);
                        mainForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid role");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to retrieve user role");
                }

                // ซ่อนหน้าต่างการเข้าสู่ระบบ
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty; txtPassword.Text = string.Empty;
        }

        // Method to test database connection
        private bool TestDatabaseConnection(string server, string port, string database, string user, string password)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(port) ||
                string.IsNullOrEmpty(database) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All connection parameters are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void Btn_testconn_Click(object sender, EventArgs e)
        {
            string server = textBoxIP.Text;
            string port = textBoxPort.Text;
            string database = textBoxDatabase.Text;
            string user = textBoxUser.Text;
            string password = textBoxPWD.Text;

            if (TestDatabaseConnection(server, port, database, user, password))
            {
                MessageBox.Show("Database connection successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Database connection failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
