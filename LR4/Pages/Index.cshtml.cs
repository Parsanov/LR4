using LR4.Core.Abstracts;
using LR4.Core.Interfaces;
using LR4.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LR4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Dictionary<string, ReportWriterCreator> _writers;
        private readonly Dictionary<string, ReportReaderCreator> _readers;
        private readonly IDataDBService _data;

        public List<Book> Books { get; set; } = new();

        public IndexModel(
            IEnumerable<ReportWriterCreator> writers,
            IEnumerable<ReportReaderCreator> readers,
            IDataDBService data
            )
        {
            _writers = writers.ToDictionary(w => w.Extension);
            _readers = readers.ToDictionary(r => r.Extension);
            _data = data;
        }

        public async Task OnGetAsync()
        {
            Books = (List<Book>)await _data.Get();
        }
         

        public async Task<IActionResult> OnGetDownload(string format)
        {
            if (!_writers.TryGetValue(format, out var creator))
                return BadRequest("Цей формат не підтримується");

            var bytes = await creator.ExportReport();
            return File(bytes,creator.ContentType, creator.FileName);

        }


        public async Task<IActionResult> OnPostImportReport(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_readers.TryGetValue(extension, out var creators))
                return BadRequest("Цей формат не підтримується");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            await creators.ImportReport(ms.ToArray());

            return RedirectToPage();

        }
        
    }
}
