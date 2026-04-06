using LR4.Application;
using LR4.Application.Creators.Readers;
using LR4.Application.Creators.Writers;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;
using LR4.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly JsonFormatW _jsonFormatW;
        private readonly TxtFormatW _txtFormatW;
        private readonly XmlFormatW _xmlFormatW;

        private readonly JsonFormatR _jsonFormatR;
        private readonly TxtFormatR _txtFormatR;
        private readonly XmlFormatR _xmlFormatR;

        private readonly IDataDBService _data;

        public List<Book> Books { get; set; } = new List<Book>();

        public IndexModel(JsonFormatW jsonFormat, TxtFormatW txtFormat, XmlFormatW xmlFormat
            , JsonFormatR jsonFormatR, TxtFormatR txtFormatR, XmlFormatR xmlFormatR
            , IDataDBService data)
        {
            _jsonFormatW = jsonFormat;
            _txtFormatW = txtFormat;
            _xmlFormatW = xmlFormat;

            _jsonFormatR = jsonFormatR;
            _txtFormatR = txtFormatR;
            _xmlFormatR = xmlFormatR;
            _data = data;
        }

        public async Task<IActionResult> OnGetDownloadJson()
        {
            var bytes = await _jsonFormatW.ExportReport();
            return File(bytes, "application/json", "report.json");
        }


        public async Task OnGetAsync()
        {
            Books = (List<Book>)await _data.Get();
        }


        public async Task<IActionResult> OnGetDownloadTXT()
        {
            var bytes = await _txtFormatW.ExportReport();
            return File(bytes, "text/plain", "report.txt");
        }


        public async Task<IActionResult> OnGetDownloadXML()
        {
            var bytes = await _xmlFormatW.ExportReport();
            return File(bytes, "application/xml", "report.xml");
        }


        public async Task<IActionResult> OnPostImportReport(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            ReportReaderCreator creator = extension switch
            {
                ".json" => _jsonFormatR,
                ".txt" => _txtFormatR,
                ".xml" => _xmlFormatR,
                _ => null
            };

            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            await creator.ImportReport(bytes);
            return RedirectToPage();
        }
    }
}
