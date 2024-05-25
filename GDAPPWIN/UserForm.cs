using System;
using System.Windows.Forms;

namespace GDAPPWIN
{
    public partial class UserForm : Form
    {
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;

        public UserForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
            InitializeDataGridView(); // Add this line
            LoadUserData();
        }
        private void InitializeDataGridView()
        {
            // Define columns for the DataGridView
            dataGridViewListUser.Columns.Add("UserID", "UserID");
            dataGridViewListUser.Columns.Add("Name", "Name");
            dataGridViewListUser.Columns.Add("Username", "Username");
            dataGridViewListUser.Columns.Add("Password", "Password");
            dataGridViewListUser.Columns.Add("Role", "Role");
        }

        private void LoadUserData()
        {
            try
            {
                // Clear existing data in the DataGridView
                dataGridViewListUser.Rows.Clear();

                // Retrieve user data from the database
                var users = UserDataAccess.GetAllUsers();

                // Populate the DataGridView with the retrieved data
                foreach (var user in users)
                {
                    dataGridViewListUser.Rows.Add(user.UserID, user.Name, user.Username, user.Password, user.Role);
                }

                // Define Thai column headers
                Dictionary<string, string> columnHeaders = new Dictionary<string, string>
                {
                    { "UserID", "รหัสผู้ใช้" },
                    { "Name", "ชื่อ" },
                    { "Username", "ชื่อผู้ใช้" },
                    { "Password", "รหัสผ่าน" },
                    { "Role", "บทบาท" }
                };

                // Set Thai column headers
                dataGridViewListUser.SetColumnHeadersFromDictionary(columnHeaders);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            bool success = UserDataAccess.InsertUser(txtName.Text, txtUsername.Text, txtPassword.Text, txtRole.Text);
            if (success)
            {
                MessageBox.Show("User data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUserData(); // Reload user data after insert
            }
            else
            {
                MessageBox.Show("Failed to save user data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchUser_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearchUser.Text;
            List<UserData> matchingUsers = UserDataAccess.SearchUser(searchTerm);

            // Clear existing data in the DataGridView
            dataGridViewListUser.Rows.Clear();

            // Populate the DataGridView with the matching users
            foreach (UserData user in matchingUsers)
            {
                dataGridViewListUser.Rows.Add(user.UserID, user.Name, user.Username, user.Password, user.Role);
            }

            // Define Thai column headers
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "UserID", "รหัสผู้ใช้" },
                { "Name", "ชื่อ" },
                { "Username", "ชื่อผู้ใช้" },
                { "Password", "รหัสผ่าน" },
                { "Role", "บทบาท" }
            };

            // Set Thai column headers
            dataGridViewListUser.SetColumnHeadersFromDictionary(columnHeaders);
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dataGridViewListUser.CurrentCell.RowIndex;
            if (selectedRowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewListUser.Rows[selectedRowIndex];
                int userID = Convert.ToInt32(row.Cells["UserID"].Value); // Convert to int

                bool success = UserDataAccess.UpdateUser(userID, txtName.Text, txtUsername.Text, txtPassword.Text, txtRole.Text);
                if (success)
                {
                    MessageBox.Show("User data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserData(); // Reload user data after update
                }
                else
                {
                    MessageBox.Show("Failed to update user data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a user from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewListUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewListUser.Rows[e.RowIndex];
                label_UserID.Text = row.Cells["UserID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtRole.Text = row.Cells["Role"].Value.ToString();
            }
        }
       
    }
}
