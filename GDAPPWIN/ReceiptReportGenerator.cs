using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
namespace GDAPPWIN
{
    public class ReceiptReportGenerator
    {
        public void GenerateReceiptReport(DataTable paidDiscountReceipts, DataTable unpaidDiscountReceipts, DataTable partialPaidDiscountReceipts)
        {
            // กำหนดชื่อไฟล์ PDF
            string fileName = "ReceiptReport.pdf";

            // สร้างเอกสาร PDF
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (Document document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // เพิ่มหัวเรื่องในเอกสาร PDF
                    Paragraph title = new Paragraph(new Chunk("Receipt Report"));
                    document.Add(title);

                    // เพิ่มข้อมูลใบเสร็จที่จ่ายส่วนลดครบแล้ว
                    AddReceiptData(document, "Paid Discount Receipts", paidDiscountReceipts);

                    // เพิ่มข้อมูลใบเสร็จที่ยังไม่ได้จ่ายส่วนลด
                    AddReceiptData(document, "Unpaid Discount Receipts", unpaidDiscountReceipts);

                    // เพิ่มข้อมูลใบเสร็จที่จ่ายส่วนลดแต่ยังไม่ครบ
                    AddReceiptData(document, "Partially Paid Discount Receipts", partialPaidDiscountReceipts);

                    document.Close();
                }
            }
        }

        private void AddReceiptData(Document document, string title, DataTable data)
        {
            if (data.Rows.Count > 0)
            {
                // เพิ่มหัวเรื่องในข้อมูลใบเสร็จ
                Paragraph header = new Paragraph(new Chunk(title));
                document.Add(header);

                // สร้างตารางข้อมูล
                PdfPTable table = new PdfPTable(data.Columns.Count);
                foreach (DataColumn column in data.Columns)
                {
                    table.AddCell(new PdfPCell(new Phrase(column.ColumnName)));
                }

                // เพิ่มข้อมูลในแต่ละแถวของตาราง
                foreach (DataRow row in data.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        table.AddCell(new PdfPCell(new Phrase(item.ToString())));
                    }
                }

                // เพิ่มตารางลงในเอกสาร PDF
                document.Add(table);
            }
        }


        public static void GenerateReport(DataTable data, string title)
        {
            if (data.Rows.Count > 0)
            {
                string fileName = $"{title.Replace(" ", "")}Report.pdf";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.FileName = fileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        using (Document doc = new Document(PageSize.A4.Rotate())) // ปรับเป็นแนวนอน
                        {
                            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                            doc.Open();
                            // สร้างและกำหนดคุณสมบัติของตาราง
                            PdfPTable table = new PdfPTable(data.Columns.Count);
                            table.WidthPercentage = 100;
                            // เพิ่มหัวเรื่องของตาราง
                            foreach (DataColumn column in data.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12)));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            // เพิ่มข้อมูลในแต่ละแถวของตาราง
                            foreach (DataRow row in data.Rows)
                            {
                                foreach (object item in row.ItemArray)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(item.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12)));
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }
                            }
                            // เพิ่มตารางลงในเอกสาร PDF
                            doc.Add(new Paragraph(title, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12))); // ปรับขนาดตัวอักษรให้เล็กลงเป็นขนาดสัก 12
                            doc.Add(table);
                            doc.Close();
                        }
                    }
                    MessageBox.Show("PDF file has been saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No data available for the selected report type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private static void AddDataTableToDocument(Document document, DataTable data)
        {
            PdfPTable table = new PdfPTable(data.Columns.Count);
            foreach (DataColumn column in data.Columns)
            {
                table.AddCell(new PdfPCell(new Phrase(column.ColumnName)));
            }

            foreach (DataRow row in data.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    table.AddCell(new PdfPCell(new Phrase(item.ToString())));
                }
            }

            document.Add(table);
        }

    }
}
