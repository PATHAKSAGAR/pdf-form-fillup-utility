# FillPdfForm 

.NET Library to fill pdf forms dynamically 

## Getting Started

### Installing Package

Install-Package FillPdfFormUtility -Version 1.0.0

## Usage

### Get list of form fields in a PDF

```csharp
 var filePath = ".\leaveform.pdf";

 var listOfFields = PdfFormUtility.GetFieldsToMap(filePath);

```

### Filling a PDF Form.

Fill a PDF Form with given data and returns the PDF bytes.

```csharp

var filePath = ".\leaveform.pdf";

var inputData= new Dictionary<string, string>
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

var result = PdfFormUtility.GeneratePdf(filePath, inputData);

```

### DemoConsoleApp

This is a [console app ](https://github.com/PATHAKSAGAR/pdf-form-fillup-utility/blob/main/PdfFormFillUtility/Program.cs) for reference purpose where you can see how we can hookup fill form utility.
