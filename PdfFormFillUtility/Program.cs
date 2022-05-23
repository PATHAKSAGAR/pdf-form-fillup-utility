using Newtonsoft.Json;
using PdfFormFillUtility.Utility;
using System;
using System.IO;

namespace PdfFormFillUtility
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Pdf is getting processed");
                var root = Environment.CurrentDirectory.Replace("\\bin\\Debug\\netcoreapp3.1", "");
                var filePath = @$"{root}\leaveform.pdf";

                var dataModel = new Model()
                {
                    FullName = "sagar Pathak",
                    FamilyName = "Sagar Pathak",
                    Language1 = "Off",
                    Language2 = "Off",
                    Language3 = "Off",
                    Language4 = "Off",
                    Language5 = "On",
                    FavColour = "Orange",
                    Address1 = "JB Nagar",
                    Address2 = "Chakala, Andheri East",
                    HouseNumber = "23 A",
                    City = "Akola",
                    PostalCode = "12345",
                    Country = "Denmark",
                    Gender = "Man",
                    Height = "120",
                    DrivingLicense = "On"
                };

                var data = PdfFormUtility<Model>.Generate(filePath, JsonConvert.SerializeObject(dataModel));
                var outputFilePath = @$"{root}\leaveformoutput.pdf";
                File.WriteAllBytes(outputFilePath, data);

                Console.WriteLine($"Pdf is processed & saved at {outputFilePath}");

                /* Another Way of Doing */
                //var data = PdfFormUtility<Model>.Generate(filePath, dataModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{"Pdf Processing Failed"} - {ex}");
            }
        }

        public class Model
        {
            [JsonProperty(PropertyName = "Given Name Text Box")]
            public string FullName { get; set; }
            [JsonProperty(PropertyName = "Family Name Text Box")]
            public string FamilyName { get; set; }
            [JsonProperty(PropertyName = "Favourite Colour List Box")]
            public string FavColour { get; set; }
            [JsonProperty(PropertyName = "Language 1 Check Box")]
            public string Language1 { get; set; }
            [JsonProperty(PropertyName = "Language 2 Check Box")]
            public string Language2 { get; set; }
            [JsonProperty(PropertyName = "Language 3 Check Box")]
            public string Language3 { get; set; }
            [JsonProperty(PropertyName = "Language 4 Check Box")]
            public string Language4 { get; set; }
            [JsonProperty(PropertyName = "Language 5 Check Box")]
            public string Language5 { get; set; }
            [JsonProperty(PropertyName = "Address 1 Text Box")]
            public string Address1 { get; set; }
            [JsonProperty(PropertyName = "Address 2 Text Box")]
            public string Address2 { get; set; }
            [JsonProperty(PropertyName = "Postcode Text Box")]
            public string PostalCode { get; set; }
            [JsonProperty(PropertyName = "City Text Box")]
            public string City { get; set; }
            [JsonProperty(PropertyName = "House nr Text Box")]
            public string HouseNumber { get; set; }
            [JsonProperty(PropertyName = "Country Combo Box")]
            public string Country { get; set; }
            [JsonProperty(PropertyName = "Gender List Box")]
            public string Gender { get; set; }
            [JsonProperty(PropertyName = "Height Formatted Field")]
            public string Height { get; set; }
            [JsonProperty(PropertyName = "Driving License Check Box")]
            public string DrivingLicense { get; set; }
        }
    }
}
