using iTextSharp.text.pdf;
using System.Text;

public class CustomEncodingProvider : EncodingProvider
{
    public override Encoding GetEncoding(int codepage)
    {
        // สร้าง Encoding ด้วยรหัส code page ที่ต้องการ
        switch (codepage)
        {
            case 0: // รหัส code page 0 คือ ANSI
                string fontPath = @"C:\Windows\Fonts\THSarabunNew.ttf";
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12);
                return Encoding.GetEncoding(codepage); // สร้าง Encoding ด้วยรหัส code page
            default:
                return null; // ถ้าไม่รู้จักรหัส code page ที่กำหนดให้ return null
        }
    }

    public override Encoding GetEncoding(string name)
    {
        // ไม่ได้ใช้เมทอดนี้ในตัวอย่างนี้
        throw new NotSupportedException();
    }
}

