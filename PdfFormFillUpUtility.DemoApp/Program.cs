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

                Console.WriteLine("\nExecuting GetFieldsToMap() function to Get list of all form fields in pdf..");
                var listOfFields = PdfFormUtility.GetFieldsToMap(filePath);
                if (listOfFields?.Count > 0)
                {
                    Console.WriteLine("\nPrinting fields name..\n");
                    foreach (var field in listOfFields)
                    {
                        Console.WriteLine(field);
                    }
                }

                Console.WriteLine("\nExecuting GeneratePdf() function to fill pdf form ..");
                var data = PdfFormUtility.GeneratePdf(filePath, new Dictionary<string, string>
                {
                      {"Given Name Text Box", "Sagar Pathak"},
                      {"Family Name Text Box", "Ramkumar Pathak"},
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
                Console.WriteLine($"\nPdf form is processed & saved at Pdf at {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{"Pdf Processing Failed"} - {ex}");
            }
        }
    }
}
