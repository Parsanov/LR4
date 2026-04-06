using LR4.Core.Interfaces;
using LR4.Core.Model;
using LR4.Persistence;
using System.Xml.Serialization;

namespace LR4.Application.Service.Writers
{
    public class XmlReportWriter : IReportWriter
    {
        private readonly IDataDBService _dataDBService;

        public XmlReportWriter(IDataDBService dataDBService)
        {
            _dataDBService = dataDBService;
        }

        public async Task<byte[]> GenerateWriteReport()
        {
            var books = await _dataDBService.Get();

            var xml = new XmlSerializer(typeof(List<Book>));

            var memoryStream = new MemoryStream();
            xml.Serialize(memoryStream, books);

            return memoryStream.ToArray();
        }
    }
}
