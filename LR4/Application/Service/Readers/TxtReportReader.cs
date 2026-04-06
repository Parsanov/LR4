using LR4.Core.Interfaces;
using LR4.Core.Model;

namespace LR4.Application.Service.Readers
{
    public class TxtReportReader : IReportReader
    {
        private readonly IDataDBService _data;

        public TxtReportReader(IDataDBService data)
        {
            _data = data;
        }

        public async Task GenerateReaderReport(byte[] fileData)
        {
            var txtString = System.Text.Encoding.UTF8.GetString(fileData);

            var lines = txtString.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var books = lines.Select(line =>
            {
                var parts = line.Split('|', StringSplitOptions.TrimEntries);

                string GetValue(string part)
                {
                    var splitPart = part.Split(':', 2);
                    return splitPart.Length > 1 ? splitPart[1].Trim() : string.Empty;
                }


                return new Book
                {
                    Title = GetValue(parts[1]),
                    Author = GetValue(parts[2]),
                    DateRelease = DateTime.SpecifyKind(
                        DateTime.TryParse(GetValue(parts[3]), out var date) ? date : DateTime.MinValue,
                        DateTimeKind.Utc)
                };
            }).ToList();

            await _data.AddList(books);
        }
    }
}
