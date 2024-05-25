using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Text;
using System.IO;

namespace GDAPPWIN
{
    public partial class DepositReportForm : Form
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
        public DepositReportForm(int userID, string username, string userRole)
        {
            InitializeComponent();
            this.loggedInUserID = userID;
            this.loggedInUserRole = username;
            this.loggedInUserRole = userRole;
        }

        private void DepositReportForm_Load(object sender, EventArgs e)
        {
            // เมื่อฟอร์มโหลด ให้โหลดข้อมูลการฝากเงินและส่วนลด
            LoadDataForReport(ReportMode.OnLoad);
        }
        private void btn_PrintReport2_Click(object sender, EventArgs e)
        {
            PrintReport2();
        }
        private void PrintReport2()
        {
            DateTime reportDate = dateTimePickerReport.Value.Date;

            // โหลดข้อมูลการฝากเงินและส่วนลดตามวันที่
            DataTable depositData = DepositDataAccess.LoadDepositsByDate(reportDate);

            DataTable discountData = DiscountDataAccess.LoadDiscountsByDate(reportDate);

            // สร้างชื่อไฟล์ PDF
            string fileName = $"Deposit_Discount_Report_{reportDate.ToString("yyyyMMdd")}.pdf";

            // สร้าง SaveFileDialog สำหรับบันทึกไฟล์ PDF
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string outputPath = saveFileDialog.FileName;

                // เริ่มกระบวนการสร้าง PDF
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document document = new Document(PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    // เรียกใช้งาน CustomEncodingProvider
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding encoding = Encoding.UTF8; // สร้าง Encoding จาก CustomEncodingProvider

                    // ระบุฟอนต์ภาษาไทยและให้ iTextSharp ใช้ฟอนต์นี้
                    string fontPath = @"Fonts\THSarabunNew.ttf"; // ตั้งค่าตำแหน่งไฟล์ฟอนต์
                    //string fontPath = Path.Combine(Application.StartupPath, @"C:\Windows\Fonts\arial.ttf");

                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12);

                    document.Open();

                    // เพิ่มหัวข้อของรายงาน
                    Paragraph header = new Paragraph($"รายงาน เงินฝาก/ส่วนลด - {reportDate.ToShortDateString()}", font); // ใช้ฟอนต์ที่กำหนด
                    header.Alignment = Element.ALIGN_CENTER;
                    document.Add(header);
                    document.Add(new Paragraph("\n"));

                    List<string> columnNames = new List<string> { "Deposit ID", "รหัสลูกค้า", "ชื่อลูกค้า", "ชื่อ/เลขที่", "เงินฝาก", "วันที่", "พนักงาน" };

                    // เพิ่มตารางข้อมูลการฝากเงินในเอกสาร PDF
                    PdfPTable depositTable = CreatePdfTable(depositData, font, columnNames);

                    document.Add(new Paragraph("ข้อมูลเงินฝาก:", font)); // ใช้ฟอนต์ที่กำหนด
                    document.Add(depositTable);
                    document.Add(new Paragraph("\n"));

                    List<string> columnNamesdis = new List<string> { "Discount ID", "รหัสลูกค้า", "ชื่อลูกค้า", "ชื่อ/เลขที่", "ส่วนลด", "วันที่", "พนักงาน" };
                    // เพิ่มตารางข้อมูลส่วนลดในเอกสาร PDF
                    PdfPTable discountTable = CreatePdfTable(discountData, font, columnNamesdis);
                    document.Add(new Paragraph("ข้อมูลส่วนลด", font)); // ใช้ฟอนต์ที่กำหนด
                    document.Add(discountTable);

                    decimal totalDepositAmount = DepositDataAccess.SumDepositAmountByDate(reportDate);
                    Paragraph totalDepositParagraph = new Paragraph($"จำนวนเงินฝาก {reportDate.ToShortDateString()}: {totalDepositAmount.ToString("#,##0.00")} บาท", font); // ใช้ฟอนต์ที่กำหนด
                    totalDepositParagraph.Alignment = Element.ALIGN_RIGHT;
                    document.Add(totalDepositParagraph);
                    document.Add(new Paragraph("\n"));

                    decimal totalDiscountAmountByDate = DiscountDataAccess.SumDiscountAmountByDate(reportDate);
                    Paragraph totalDiscountByDateParagraph = new Paragraph($"จำนวนเงินส่วนลด {reportDate.ToShortDateString()}: {totalDiscountAmountByDate.ToString("#,##0.00")} บาท", font); // ใช้ฟอนต์ที่กำหนด
                    totalDiscountByDateParagraph.Alignment = Element.ALIGN_RIGHT;
                    document.Add(totalDiscountByDateParagraph);
                    document.Add(new Paragraph("\n"));

                    document.Close();
                }

                MessageBox.Show($"Deposit & Discount report for {reportDate.ToShortDateString()} has been generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private PdfPTable CreatePdfTable(DataTable data, iTextSharp.text.Font font, List<string> columnNames)
        {
            PdfPTable table;

            if (data.Rows.Count > 0)
            {
                table = new PdfPTable(data.Columns.Count); // สร้างตารางที่มีจำนวนคอลัมน์เท่ากับจำนวนคอลัมน์ใน DataTable

                // เพิ่มหัวของตาราง
                foreach (string columnName in columnNames)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(columnName, font));
                    table.AddCell(cell);
                }

                // เพิ่มข้อมูลในตาราง
                foreach (DataRow row in data.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(item.ToString(), font));
                        table.AddCell(cell);
                    }
                }
            }
            else
            {
                table = new PdfPTable(1); // สร้างตารางที่มีคอลัมน์เพียงหนึ่งเพื่อแสดงหัวของตาราง
                PdfPCell cell = new PdfPCell(new Phrase("No data available", font));
                cell.Colspan = data.Columns.Count; // รวมเป็นคอลัมน์เดียวกัน
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER; // จัดตำแหน่งให้อยู่กึ่งกลาง
                table.AddCell(cell);
            }

            return table;
        }

        // เมื่อผู้ใช้คลิกที่ RadioButton
        private void radioButton_Click(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                if (radioButton == radioButtonAll)
                {
                    LoadDataForReport(ReportMode.All);
                }
                else if (radioButton == radioButtonByDate)
                {
                    LoadDataForReport(ReportMode.ByDate);
                }
                else if (radioButton == radioButtonBetweenDates)
                {
                    LoadDataForReport(ReportMode.BetweenDates);
                }
            }
        }
        private void btnSelectReport_Click(object sender, EventArgs e)
        {
            if (radioButtonAll.Checked)
            {
                LoadDataForReport(ReportMode.All);
            }
            else if (radioButtonByDate.Checked)
            {
                LoadDataForReport(ReportMode.ByDate);
            }
            else if (radioButtonBetweenDates.Checked)
            {
                LoadDataForReport(ReportMode.BetweenDates);
            }
        }

        public enum ReportMode
        {
            All,
            ByDate,
            BetweenDates,
            OnLoad
        }
        private void LoadDataForReport(ReportMode mode)
        {
            DateTime reportDate = DateTime.Today.Date; // ใช้วันที่ปัจจุบันเป็นค่าเริ่มต้น

            switch (mode)
            {
                case ReportMode.All:
                    LoadAllData();
                    break;
                case ReportMode.ByDate:
                    DateTime byDate = dateTimePickerByDate.Value.Date;
                    LoadDataByDate(byDate);
                    break;
                case ReportMode.BetweenDates:
                    // ตั้งค่าวันที่เริ่มต้นและสิ้นสุดของช่วงเวลาที่ต้องการ
                    DateTime startDate = dateTimePickerStartDate.Value.Date;
                    DateTime endDate = dateTimePickerEndDate.Value.Date;
                    LoadDataBetweenDates(startDate, endDate);
                    break;
                case ReportMode.OnLoad:
                    LoadDataByDate(reportDate);
                    break;
            }
        }

        private void LoadAllData()
        {
            DataTable discountData = DiscountDataAccess.GetAllDiscounts();
            DataTable depositData = DepositDataAccess.GetAllDeposits();

            dataGridViewDiscountList.DataSource = discountData;
            dataGridViewDepositList.DataSource = depositData;
            // สร้างอินสแตนซ์ของคลาส ReportColumnHeaders
            DepositDataAccess reportHeaders = new DepositDataAccess();
            DiscountDataAccess reportDisHeaders = new DiscountDataAccess();
            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานส่วนลดได้ดังนี้
            Dictionary<string, string> discountHeaders = reportDisHeaders.DiscountHeaders;

            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานเงินฝากได้ดังนี้
            Dictionary<string, string> depositHeaders = reportHeaders.DepositHeaders;
            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานส่วนลด
            SetDataGridViewHeaders(dataGridViewDiscountList, discountHeaders);

            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานเงินฝาก
            SetDataGridViewHeaders(dataGridViewDepositList, depositHeaders);


        }

        private void LoadDataByDate(DateTime date)
        {
            DataTable discountData = DiscountDataAccess.LoadDiscountsByDate(date);
            DataTable depositData = DepositDataAccess.LoadDepositsByDate(date);

            dataGridViewDiscountList.DataSource = discountData;
            dataGridViewDepositList.DataSource = depositData;
            // สร้างอินสแตนซ์ของคลาส ReportColumnHeaders
            DepositDataAccess reportHeaders = new DepositDataAccess();
            DiscountDataAccess reportDisHeaders = new DiscountDataAccess();
            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานส่วนลดได้ดังนี้
            Dictionary<string, string> discountHeaders = reportDisHeaders.DiscountHeaders;

            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานเงินฝากได้ดังนี้
            Dictionary<string, string> depositHeaders = reportHeaders.DepositHeaders;
            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานส่วนลด
            SetDataGridViewHeaders(dataGridViewDiscountList, discountHeaders);

            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานเงินฝาก
            SetDataGridViewHeaders(dataGridViewDepositList, depositHeaders);

        }

        private void LoadDataBetweenDates(DateTime startDate, DateTime endDate)
        {
            DataTable discountData = DiscountDataAccess.LoadDiscountsBetweenDates(startDate, endDate);
            DataTable depositData = DepositDataAccess.LoadDepositsBetweenDates(startDate, endDate);

            dataGridViewDiscountList.DataSource = discountData;
            dataGridViewDepositList.DataSource = depositData;
            // สร้างอินสแตนซ์ของคลาส ReportColumnHeaders
            DepositDataAccess reportHeaders = new DepositDataAccess();
            DiscountDataAccess reportDisHeaders = new DiscountDataAccess();
            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานส่วนลดได้ดังนี้
            Dictionary<string, string> discountHeaders = reportDisHeaders.DiscountHeaders;

            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานเงินฝากได้ดังนี้
            Dictionary<string, string> depositHeaders = reportHeaders.DepositHeaders;
            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานส่วนลด
            SetDataGridViewHeaders(dataGridViewDiscountList, discountHeaders);

            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานเงินฝาก
            SetDataGridViewHeaders(dataGridViewDepositList, depositHeaders);


        }

        private void SetDataGridViewHeaders(DataGridView dataGridView, Dictionary<string, string> columnHeaders)
        {
            foreach (KeyValuePair<string, string> entry in columnHeaders)
            {
                if (dataGridView.Columns.Contains(entry.Key))
                {
                    dataGridView.Columns[entry.Key].HeaderText = entry.Value;
                }
            }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();

            // เรียก LoadDepositDataByCustomerName และ LoadDiscountDataByCustomerName จาก DepositDataAccess และ DiscountDataAccess
            DataTable depositData = DepositDataAccess.LoadDepositDataByCustomerName(searchText);
            DataTable discountData = DiscountDataAccess.LoadDiscountDataByCustomerName(searchText);

            dataGridViewDepositList.DataSource = depositData;
            dataGridViewDiscountList.DataSource = discountData;
            // สร้างอินสแตนซ์ของคลาส ReportColumnHeaders
            DepositDataAccess reportHeaders = new DepositDataAccess();
            DiscountDataAccess reportDisHeaders = new DiscountDataAccess();
            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานส่วนลดได้ดังนี้
            Dictionary<string, string> discountHeaders = reportDisHeaders.DiscountHeaders;

            // สามารถเข้าถึง Dictionary ของคอลัมน์สำหรับรายงานเงินฝากได้ดังนี้
            Dictionary<string, string> depositHeaders = reportHeaders.DepositHeaders;
            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานส่วนลด
            SetDataGridViewHeaders(dataGridViewDiscountList, discountHeaders);

            // กำหนดหัวข้อคอลัมน์สำหรับ DataGridView สำหรับรายงานเงินฝาก
            SetDataGridViewHeaders(dataGridViewDepositList, depositHeaders);
        }

    }
}

