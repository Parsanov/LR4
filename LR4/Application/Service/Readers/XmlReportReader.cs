using LR4.Core.Interfaces;
using LR4.Core.Model;
using System.Xml.Serialization;

namespace LR4.Application.Service.Readers
{
    public class XmlReportReader : IReportReader
    {
        private readonly IDataDBService _data;

        public XmlReportReader(IDataDBService data)
        {
            _data = data;
        }

        public async Task GenerateReaderReport(byte[] fileData)
        {
            var xmlString = System.Text.Encoding.UTF8.GetString(fileData);
            
            var serializer = new XmlSerializer(typeof(List<Book>));

            using var reader = new StringReader(xmlString); 
            var books = (List<Book>)serializer.Deserialize(reader);

            foreach (var book in books)
            {
                book.Id = 0;
                if (book.DateRelease.HasValue)
                    book.DateRelease = DateTime.SpecifyKind(book.DateRelease.Value, DateTimeKind.Utc);

            }

            await _data.AddList(books);

        }
    }
}
