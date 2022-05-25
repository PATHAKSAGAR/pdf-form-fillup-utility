using PdfFormFillUtility.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfFormFillUtility
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var root = Environment.CurrentDirectory.Replace("\\bin\\Debug\\netcoreapp3.1", "");
                var filePath = @$"{root}\leaveform.pdf";

                Console.WriteLine("Executing Function-1 GetFeildsToMap() = To Get list of all feilds from given pdf");
                var listOfFeilds = PdfFormUtility.GetFieldsToMap(filePath);
                if (listOfFeilds?.Count > 0)
                {
                    Console.WriteLine("Printing results..");
                    foreach (var feild in listOfFeilds)
                    {
                        Console.WriteLine(feild);
                    }
                }

                Console.WriteLine("Executing Function-2 GeneratePdf()");
                var data = PdfFormUtility.GeneratePdf(filePath, new Dictionary<string, string>
                {
                      {"Given Name Text Box", "Sagar Pathak"},
                      {"Family Name Text Box", "Anjali R Sharma"},
                      {"Favourite Colour List Box", "Red"},
                      {"Language 1 Check Box", "Off"},
                      {"Language 2 Check Box", "Off"},
                      {"Language 3 Check Box", "Off"},
                      {"Language 4 Check Box", "Off"},
                      {"Language 5 Check Box", "On"},
                      {"Address 1 Text Box", "JB Nagar"},
                      {"Address 2 Text Box", "Chakala, Andheri East"},
                      {"Postcode Text Box", "12345"},
                      {"City Text Box", "Mumbai"},
                      {"House nr Text Box", "23 A"},
                      {"Country Combo Box", "Denmark"},
                      {"Gender List Box", "Man"},
                      {"Height Formatted Field", "175 cm"},
                      {"Driving License Check Box", "On"}
                });

                var outputFilePath = @$"{root}\leaveformoutput.pdf";
                File.WriteAllBytes(outputFilePath, data);
                Console.WriteLine($"Pdf is processed & saved at {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{"Pdf Processing Failed"} - {ex}");
            }
        }
    }
}
