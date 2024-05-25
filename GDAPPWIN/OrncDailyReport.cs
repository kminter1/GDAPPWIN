using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GDAPPWIN
{
    public class OrncDailyReport
    {
        public static void GenerateDailyReport(DateTime? date)
        {
            // ดึงรายการบิลของวันที่ที่กำหนดจาก OrncClass
            List<OrncBill> orncBills = OrncClass.GetOrncInvoiceByDate(date);

            // กำหนดชื่อไฟล์ PDF
            string fileName = $"Daily_Report_{date.Value.ToString("yyyyMMdd")}.pdf";

            // สร้าง SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.FileName = fileName;

            // ถ้าผู้ใช้เลือกที่จะบันทึกไฟล์
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string outputPath = saveFileDialog.FileName;

                // คำนวณผลรวมจำนวนในแต่ละวัน
                decimal totalAmount = 0;
                foreach (var bill in orncBills)
                {
                    totalAmount += bill.TotalAmount;
                }

                // คำนวณจำนวนเงินทั้งหมดในแต่ละวัน
                decimal totalOrncAmount = OrncClass.GetTotalOrncAmountByDate(date);

                // สร้างเอกสาร PDF
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // เพิ่มหัวข้อของรายงาน
                    Paragraph header = new Paragraph($"Daily Report - {date.Value.ToShortDateString()}", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD));
                    header.Alignment = Element.ALIGN_CENTER;
                    document.Add(header);
                    document.Add(new Paragraph("\n"));

                    // สร้างตาราง
                    PdfPTable table = new PdfPTable(3); // 3 คือจำนวนคอลัมน์

                    // เพิ่มหัวตาราง
                    table.AddCell(new PdfPCell(new Phrase("Temporary ID", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Temporary No", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Amount", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD))));

                    // เพิ่มข้อมูลในตาราง
                    foreach (var bill in orncBills)
                    {
                        table.AddCell(new PdfPCell(new Phrase(bill.TemporaryInvoiceID.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12))));
                        table.AddCell(new PdfPCell(new Phrase(bill.Ornc_No.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12))));
                        table.AddCell(new PdfPCell(new Phrase(bill.TotalAmount.ToString("C"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12))));
                    }

                    // เพิ่มตารางลงในเอกสาร
                    document.Add(table);

                    document.Add(new Paragraph("\n"));
                    // เพิ่มผลรวมจำนวนทั้งหมดในแต่ละวัน
                    Paragraph totalAmountParagraph = new Paragraph($"Total Amount From Table list: {totalAmount:C}");
                    document.Add(totalAmountParagraph);
                    document.Add(new Paragraph("\n"));
                    // เพิ่มจำนวนเงินทั้งหมดที่ได้ในแต่ละวัน
                    Paragraph totalOrncAmountParagraph = new Paragraph($"Total Amount From DataBase: {totalOrncAmount:C}");
                    document.Add(totalOrncAmountParagraph);

                    document.Close();
                }

                // แสดงข้อความบน MessageBox เมื่อสร้างเอกสาร PDF เสร็จสิ้น
                MessageBox.Show($"Daily report for {date.Value.ToShortDateString()} has been generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



    }

    public partial class OrncAddForm : Form
    {
        private void btn_PrintReport_Click(object sender, EventArgs e)
        {
            // ดึงวันที่ที่ผู้ใช้เลือกจาก DateTimePicker
            DateTime? selectedDate = dateTimePickerOrncSearch.Checked ? dateTimePickerOrncSearch.Value.Date : (DateTime?)null;

            if (selectedDate.HasValue)
            {
                // สร้างรายงานประจำวัน
                OrncDailyReport.GenerateDailyReport(selectedDate);
            }
            else
            {
                MessageBox.Show("Please select a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
