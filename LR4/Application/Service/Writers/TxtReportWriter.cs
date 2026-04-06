using LR4.Core.Interfaces;
using LR4.Core.Model;
using System.Linq;
using System.Text;

namespace LR4.Application.Service.Writers
{
    public class TxtReportWriter : IReportWriter
    {
        private readonly IDataDBService _data;

        public TxtReportWriter(IDataDBService data)
        {
            _data = data;
        }

        public async Task<byte[]> GenerateWriteReport()
        {

            var books = await _data.Get();
            var text = string.Join('\n', books.Select(b => $"ID: {b.Id}|Title: {b.Title}|Author: {b.Author}|Date: {b.DateRelease?.ToString("yyyy-MM-dd")}"));

            return Encoding.UTF8.GetBytes(text);
        }
    }
}
