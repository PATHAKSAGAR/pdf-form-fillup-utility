using iText.Forms;
using iText.Kernel.Pdf;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PdfFormFillUtility.Utility
{
    /// <summary>
    /// T should be a class where properties should be same as pdf feilds names or you can also use attibute mapping to set custom name to property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class PdfFormUtility<T> where T : class
    {
        private static Dictionary<string, string> GetMappings(T model)
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var customName = prop.GetCustomAttribute<JsonPropertyAttribute>().PropertyName;
                mappings.Add(string.IsNullOrWhiteSpace(customName) ? prop.Name : customName, prop.GetValue(model).ToString());
            }

            return mappings;
        }

        /// <summary>
        /// Return pdf bytes which can be used to create pdf.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public static byte[]? Generate(string filePath, T dataModel)
        {
            var dictMapper = GetMappings(dataModel);
            using var outMS = new MemoryStream();
            var reader = new PdfReader(filePath);
            var pdfDoc = new PdfDocument(reader, new PdfWriter(outMS));
            if (dataModel != null)
            {
                FillForm(pdfDoc, dictMapper);

                pdfDoc.Close();

                return outMS.ToArray();
            }
            else
            {
                return default;
            }
        }

        public static byte[]? Generate(string filePath, string dataModel)
        {
            var dictMapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataModel);
            using var outMS = new MemoryStream();
            var reader = new PdfReader(filePath);
            var pdfDoc = new PdfDocument(reader, new PdfWriter(outMS));
            if (dataModel != null)
            {
                FillForm(pdfDoc, dictMapper);

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

        public static object GetFeildsToMap(string filePath)
        {
            dynamic expando = new ExpandoObject();
            using var outMS = new MemoryStream();
            var reader = new PdfReader(filePath);
            var pdfDoc = new PdfDocument(reader, new PdfWriter(outMS));
            var form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            var feilds = form.GetFormFields().Select(i => i.Key).ToList();
            feilds.ForEach(i =>
            {
                AddProperty(expando, i.Replace(" ", "_"), string.Empty);
            });
            return expando;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
