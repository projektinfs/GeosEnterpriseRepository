using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise
{
    public static class Util
    {
        public static string EnumToDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)((TEnum)EnumValue));
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static void CreatePDF(string text, string pdfPath)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
            doc.Open();
            var font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 12);
            doc.Add(new Phrase(text, font));
            doc.Close();
        }
    }
}
