using LR4.Core.Interfaces;
using LR4.Core.Model;
using System.Text.Json;

namespace LR4.Application.Service.Readers
{
    public class JsonReportReader : IReportReader
    {
        private readonly IDataDBService _data;

        public JsonReportReader(IDataDBService data)
        {
            _data = data;
        }

        public async Task GenerateReaderReport(byte[] fileData)
        {
            var jsonString = System.Text.Encoding.UTF8.GetString(fileData);
            var books = JsonSerializer.Deserialize<List<Book>>(jsonString);

            foreach (var book in books)
            {
                if (book.DateRelease.HasValue)
                    book.DateRelease = DateTime.SpecifyKind(book.DateRelease.Value, DateTimeKind.Utc);
            }

            await _data.AddList(books);
        }
    }
}
