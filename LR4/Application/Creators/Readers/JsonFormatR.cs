using LR4.Application.Service.Readers;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;

namespace LR4.Application.Creators.Readers
{
    public class JsonFormatR : ReportReaderCreator
    {
        private readonly IDataDBService _data;

        public JsonFormatR(IDataDBService data)
        {
           _data = data;
        }

        public override string Extension => ".json";

        public override IReportReader CreateReport()
        {
           return new JsonReportReader(_data);
        }
    }
}
