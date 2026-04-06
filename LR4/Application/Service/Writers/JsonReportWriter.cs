using LR4.Core.Interfaces;
using LR4.Core.Model;
using LR4.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace LR4.Application.Service.Writers
{
    public class JsonReportWriter : IReportWriter
    {
        private readonly IDataDBService _data;

        public JsonReportWriter(IDataDBService data)
        {
            _data = data;
        }

        public async Task<byte[]> GenerateWriteReport()
        {
            var books = await _data.Get();

            var json = JsonSerializer.Serialize(books, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            return Encoding.UTF8.GetBytes(json);
        }

       
    }
}
