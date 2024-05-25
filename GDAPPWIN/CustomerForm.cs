using MySql.Data.MySqlClient;
using System.Data;

namespace GDAPPWIN
{
    public partial class CustomerForm : Form
    {
        // Properties เพื่อเข้าถึงข้อมูลที่ Login
        public int LoggedInUserID
        {
            get { return loggedInUserID; }
        }

        public string LoggedInUsername
        {
            get { return loggedInUsername; }
        }
        public string LoggedInUserRole
        {
            get { return loggedInUserRole; }
        }
        private int loggedInUserID;
        private string loggedInUsername;
        private string loggedInUserRole;

        CustomerClass customer = new CustomerClass();
        public CustomerForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUsername = username;
            this.loggedInUserRole = userRole;
        }
        bool Customerverify()
        {
            if (string.IsNullOrEmpty(textBox_CID.Text) || string.IsNullOrEmpty(textBox_Cname.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "เพิ่มลูกค้า", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check if the Ccode already exists
            string ccode = textBox_CID.Text;
            if (customer.IsCcodeExists(ccode))
            {
                MessageBox.Show("Ccode ซ้ำกันอยู่ในระบบ", "เพิ่มลูกค้า", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void button_AddCustomer_Click(object sender, EventArgs e)
        {
            string cname = textBox_Cname.Text;
            string ccode = textBox_CID.Text;
            if (Customerverify())
            {
                try
                {
                    if (customer.InsertCustomer(ccode, cname))
                    {
                        ShowTable();
                        clearCustomerForm();
                        MessageBox.Show("เพิ่มลูกค้าสำเร็จ", "เพิ่มลูกค้า", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มลูกค้า", "เพิ่มลูกค้า", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // To show customer list in datagridview
        public void ShowTable()
        {
            dataGridView_Customer.DataSource = customer.GetCustomerList();
            dataGridView_Customer.RowTemplate.Height = 23;

            // กำหนดชื่อหัวคอลัมน์ให้เป็นภาษาไทย
            dataGridView_Customer.Columns["CustomerID"].HeaderText = "รหัสลูกค้า";
            dataGridView_Customer.Columns["Ccode"].HeaderText = "รหัสลูกค้า";
            dataGridView_Customer.Columns["Cname"].HeaderText = "ชื่อลูกค้า";
            // ดำเนินการเช่นนี้ต่อไปตามคอลัมน์อื่น ๆ ที่มีใน DataGridView
        }
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            ShowTable();
        }
        private void button_ClearCustomer_Click(object sender, EventArgs e)
        {
            clearCustomerForm();
        }
        private void clearCustomerForm()
        {
            textBox_CID.Clear();
            textBox_Cname.Clear();
        }

    }
}
