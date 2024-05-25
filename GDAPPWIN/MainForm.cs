using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GDAPPWIN
{
    public partial class MainForm : Form
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

        public MainForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            customizeDesign();
            this.loggedInUserID = userID;
            this.loggedInUserRole = userRole;

            // เรียก method SetLoggedInUsername เพื่อกำหนดค่า username
            SetLoggedInUsername(username);

        }
        // Method เพื่อรับค่า username
        public void SetLoggedInUsername(string username)
        {
            this.loggedInUsername = username;
            // เรียก method สำหรับการปรับปรุงข้อมูลหน้า MainForm
            UpdateMainForm();
        }

        // Method สำหรับการปรับปรุงข้อมูลหน้า MainForm
        private void UpdateMainForm()
        {
            // นำข้อมูลไปใส่ใน label หรือ control ที่ต้องการ
            lblUserInfo.Text = $"User ID: {loggedInUserID}, Username: {loggedInUsername}";
            lblUserRole.Text = $"Role: {loggedInUserRole}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel4.AutoScroll = true;
            UpdateMainForm();
            CheckUserRoleLevel();
            DisplayTotalOrnc();
            DisplayTotalDeposit();
            // Load data from temporaryinvoice
            Dictionary<int, decimal> totalAmountByCustomer = InvoiceHelper.GetTotalAmountByCustomer();

            // Check if the dictionary is empty or null
            if (totalAmountByCustomer != null && totalAmountByCustomer.Count > 0)
            {
                // Create and customize the chart (passing chart1 as a parameter)
                CreateBarChart2(totalAmountByCustomer);
            }
            else
            {
                // Handle the case when totalAmountByCustomer is empty or null
                MessageBox.Show("No data available to create the chart.");
            }

        }
        private void CheckUserRoleLevel()
        {
            // ตรวจสอบ role และทำการปรับปรุง UI หรือทำสิ่งที่ต้องการต่อไป
            if (loggedInUserRole == "Admin")
            {
                // ในกรณีที่เป็น Admin
                // ทำสิ่งที่ต้องการสำหรับ Admin
                btnSubApprove.Enabled = true; // เปิดใช้งานปุ่มสำหรับ Admin
            }
            else if (loggedInUserRole == "User")
            {
                // ในกรณีที่เป็น User
                // ทำสิ่งที่ต้องการสำหรับ User
                btnSubApprove.Enabled = false; // ปิดใช้งานปุ่มสำหรับ User
                Btn_subAddUser.Enabled = false;
            }
            else
            {
                // กรณีอื่น ๆ ที่ไม่ตรงกับ Admin หรือ User
                // ทำสิ่งที่ต้องการสำหรับ User
                btnSubApprove.Enabled = false; // ปิดใช้งานปุ่มสำหรับ User
                Btn_subAddUser.Enabled = false;
            }
        }

        private void DisplayTotalOrnc()
        {
            // คำนวณยอดรวมของรายการขายทั้งหมด
            decimal totalOrnc = OrncClass.GetTotalOrncAmountByDate(null); // ส่งค่า null เพื่อคำนวณยอดรวมทั้งหมด

            // แสดงยอดขายทั้งหมดใน label
            lbl_MainTotalOrnc.Text = $"ยอดขายทั้งหมด: {totalOrnc.ToString("#,##0.00")} บาท";
        }
        private void DisplayTotalDeposit()
        {
            // คำนวณยอดรวมของรายการขายทั้งหมด
            decimal totalDeposit = InvoiceClass.GetTotalDepositAmount(); // ส่งค่า null เพื่อคำนวณยอดรวมทั้งหมด

            // แสดงยอดขายทั้งหมดใน label
            label_TotalDeposit.Text = $"ยอดเงินฝากทั้งหมด: {totalDeposit.ToString("#,##0.00")} บาท";
        }
        private void customizeDesign()
        {
            panel_submenuCreditOut.Visible = false;
            panel_submenuCreditIn.Visible = false;
            panel_submenuPayBack.Visible = false;
            panel_submenuAddUser.Visible = false;
        }

        private void hideSubmenu()
        {
            if (panel_submenuCreditOut.Visible == true)
                panel_submenuCreditOut.Visible = false;
            if (panel_submenuCreditIn.Visible == true)
                panel_submenuCreditIn.Visible = false;
            if (panel_submenuPayBack.Visible == true) 
                panel_submenuPayBack.Visible = false;
            if (panel_submenuAddUser.Visible == true)
                panel_submenuAddUser.Visible = false;
        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private void button_CreditOutMain_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_submenuCreditOut);
        }
        private void button_CreditInMain_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_submenuCreditIn);
        }
        private void button_PayBack_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_submenuPayBack);
        }
        private void Btn_AddUser_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_submenuAddUser);
        }
        #region SubmenuCreditOut
        private void button_subAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(customerForm);
            hideSubmenu();
        }

        private void button_subAddBillCredit_Click(object sender, EventArgs e)
        {
            OrncAddForm ornc = new OrncAddForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(ornc);
            hideSubmenu();
        }

        private void button_subAddRealBill_Click(object sender, EventArgs e)
        {
            InvoiceAddForm invoiceAddForm = new InvoiceAddForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(invoiceAddForm);
            hideSubmenu();
        }
        private void button_subOrvcAdd2_Click(object sender, EventArgs e)
        {
            OrvcAddFormA OrvcAddForm = new OrvcAddFormA(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(OrvcAddForm);
            hideSubmenu();
        }
        private void button_subReport_Click(object sender, EventArgs e)
        {
            SummaryForm summaryForm = new SummaryForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(summaryForm);
            hideSubmenu();
        }
        private void btn_SubApprove_Click(object sender, EventArgs e)
        {
            ApproveForm summaryForm = new ApproveForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(summaryForm);
            hideSubmenu();
        }
        private void btnsubCashIN_Click(object sender, EventArgs e)
        {
            ReceiptsForm cashIn = new ReceiptsForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(cashIn);
            hideSubmenu();
        }
        private void BtnSub_Expense_Click(object sender, EventArgs e)
        {
            CashInForm cashIn = new CashInForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(cashIn);
            hideSubmenu();
        }
        private void BtnSub_AddOrcc_Click(object sender, EventArgs e)
        {
            CashInForm cashIn = new CashInForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(cashIn);
            hideSubmenu();
        }
        private void BtnSub_EditOrcc_Click(object sender, EventArgs e)
        {
            OrccForm cashIn = new OrccForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(cashIn);
            hideSubmenu();
        }
        #endregion SubmenuCreditOut
        private void Btn_subAddUser_Click(object sender, EventArgs e)
        {
            UserForm cashIn = new UserForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(cashIn);
            hideSubmenu();
        }
        // to show Add customer form in mainform

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btn_DpReport_Click(object sender, EventArgs e)
        {
            DepositReportForm depReport = new DepositReportForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(depReport);
            hideSubmenu();
        }
        private void Btn_NotPaid_Click(object sender, EventArgs e)
        {
            // Open the NotPaidForm passing logged in user details
            NotPaidForm NotPaidReport = new NotPaidForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            openChildForm(NotPaidReport);
            hideSubmenu(); // Hide any open submenu
        }

        private void CreateBarChart1(Dictionary<int, decimal> data)
        {
            // Clear existing series and chart areas
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Create a new chart area
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // Create a new series and bind it to the data
            Series series = new Series();
            series.ChartType = SeriesChartType.StackedBar;
            series.Points.DataBindXY(data.Keys, data.Values);
            MessageBox.Show(data.Keys.ToString());
            // Add the series to the chart
            chart1.Series.Add(series);

            // Customize the chart appearance
            chart1.ChartAreas[0].AxisX.Title = "Customers ID";
            chart1.ChartAreas[0].AxisY.Title = "Total Amount";
        }

        private void CreateBarChart2(Dictionary<int, decimal> totalAmountByCustomer)
        {
            // Clear existing series and chart areas
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Create a new chart area
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.LabelStyle.Font = new Font("Arial", 8f); // Set font size for X-axis labels
            chart1.ChartAreas.Add(chartArea);

            // Enable scrolling for X-axis
            chartArea.AxisX.ScrollBar.Enabled = true;

            // Create a new series and bind it to the data
            Series series = new Series();
            series.ChartType = SeriesChartType.Bar;
            foreach (var entry in totalAmountByCustomer)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(entry.Key, entry.Value);
                dataPoint.AxisLabel = InvoiceHelper.GetCustomerNameByID(entry.Key); // Assuming GetCustomerNameByID is a method that gets the customer name by ID

                // Set the tooltip to display the customer name
                dataPoint.ToolTip = InvoiceHelper.GetCustomerNameByID(entry.Key);

                series.Points.Add(dataPoint);
            }

            // Add the series to the chart
            chart1.Series.Add(series);

            // Customize the chart appearance
            chart1.ChartAreas[0].AxisX.Title = "Customers ID";
            chart1.ChartAreas[0].AxisY.Title = "Total Amount";

            // Add data labels to each data point
            foreach (DataPoint dataPoint in series.Points)
            {
                dataPoint.Label = string.Format("{0:C}", dataPoint.YValues[0]);
            }

            // Disable major grid lines on both X and Y axes
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            // Optionally, set the color of the grid lines (if you want)
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;

            // Set the size of the chart automatically
            SetBarChartWidth(totalAmountByCustomer.Count, 0.6, 0.6); // Adjust the second and third parameters as needed
        }

        // เมทอดสำหรับกำหนดขนาดและระยะห่างของแท่งในแผนภูมิ
        private void SetBarChartWidth(int numDataPoints, double barWidthPercentage, double spacingPercentage)
        {
            int chartWidth = chart1.Width; // ความกว้างของแผนภูมิ

            // คำนวณขนาดและระยะห่างของแต่ละแท่ง
            double barWidth = (chartWidth / numDataPoints) * barWidthPercentage;
            double spacingWidth = (chartWidth / numDataPoints) * spacingPercentage;

            // กำหนดขนาดและระยะห่างของแท่งในแผนภูมิ
            chart1.Series[0]["PointWidth"] = barWidth.ToString();
            chart1.Series[0]["PixelPointWidth"] = "2"; // ค่าที่เพิ่มขึ้นเรียบร้อยแล้ว
            chart1.Series[0]["PixelPointGap"] = "2"; // ระยะห่างระหว่างแท่ง

            // ตั้งค่าการแยกแต่ละแท่ง
            for (int i = 0; i < numDataPoints; i++)
            {
                chart1.Series[0].Points[i]["PixelPointWidth"] = barWidth.ToString();
                chart1.Series[0].Points[i]["PixelPointGap"] = spacingWidth.ToString();
            }
        }
        private void panel5_Resize(object sender, EventArgs e)
        {
            // เมื่อขนาดของ Panel ถูกเปลี่ยนแปลง ปรับขนาดและตำแหน่งของแผนภูมิ
            SetChartSize();
        }

        private void SetChartSize()
        {
            // ตั้งค่าขนาดของแผนภูมิให้เหมาะสมกับขนาดของ Panel
            chart1.Size = new Size(panel4.Width - 20, panel4.Height - 20); // ลบค่าขอบเขตเพื่อให้แผนภูมิไม่แน่นเกินไป
        }

        private void CreateBarChart(Dictionary<int, decimal> data)
        {
            // Clear existing series and chart areas
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Create a new chart area
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // Create a new series and bind it to the data
            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.Points.DataBindXY(data.Keys.Select(key => InvoiceHelper.GetCustomerNameByID(key)).ToArray(), data.Values);

            // Add the series to the chart
            chart1.Series.Add(series);

            // Customize the chart appearance
            chart1.ChartAreas[0].AxisX.Title = "Customers";
            chart1.ChartAreas[0].AxisY.Title = "Total Amount";
        }

        private void label1_LOGO_Click(object sender, EventArgs e)
        {
            // ปิดหน้าต่างปัจจุบัน
            Form activeForm = ActiveForm;
            activeForm.Close();

            // เปิดหน้า MainForm
            MainForm mainForm = new MainForm(loggedInUserID, loggedInUsername, loggedInUserRole);
            mainForm.Show();
        }
        private void Btn_UpdateGDAPP_Click(object sender, EventArgs e)
        {
            try
            {
                // URL ของไฟล์อัพเดท
                string updateUrl = "http://192.168.10.198/update/update.zip";

                // ชื่อไฟล์ที่จะดาวน์โหลด
                string updateFileName = "update.zip";

                // ตำแหน่งที่จะบันทึกไฟล์อัพเดท
                string updateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, updateFileName);

                // ดาวน์โหลดไฟล์อัพเดท
                WebClient webClient = new WebClient();
                webClient.DownloadFile(updateUrl, updateFilePath);

                // หลังจากดาวน์โหลดสำเร็จ ทำการเรียกโปรแกรมติดตั้ง
                Process.Start(updateFilePath);

                MessageBox.Show("โปรแกรมอัพเดทดาวน์โหลดและติดตั้งสำเร็จ", "อัพเดทสำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
