using iText.Forms;
using iText.Kernel.Pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PdfFormFillUpUtility.Utility
{
    public static class PdfFormFillUpService
    {
        /// <summary>
        /// Returns a list of form fields that exist in the PDF
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GetFieldsToMap(string filePath)
        {
            using var outMS = new MemoryStream();
            var pdfDoc = new PdfDocument(new PdfReader(filePath));
            var form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            return form.GetFormFields()
                .Select(i => i.Key)
                .ToList();
        }


        /// <summary>
        /// Fill form in the PDF with input model and return the processed PDF byte array
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public static byte[]? GeneratePdf(string filePath, Dictionary<string, string> dictDataModel)
        {
            using var outMS = new MemoryStream();
            var reader = new PdfReader(filePath);
            var pdfDoc = new PdfDocument(reader, new PdfWriter(outMS));
            if (dictDataModel != null)
            {
                FillForm(pdfDoc, dictDataModel);

                pdfDoc.Close();

                return outMS.ToArray();
            }
            else
            {
                return default;
            }
        }

        private static void FillForm(PdfDocument pdfDoc, Dictionary<string, string> dictMapper)
        {
            var form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            foreach (var item in form.GetFormFields())
            {
                item.Value.SetReadOnly(false);
                if (dictMapper.TryGetValue(item.Key, out string value))
                {
                    item.Value.SetValue(value);
                }
                item.Value.SetReadOnly(true);
            }
        }
    }
}
