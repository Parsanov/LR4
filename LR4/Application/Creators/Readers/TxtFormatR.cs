using LR4.Application.Service.Readers;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;

namespace LR4.Application.Creators.Readers
{
    public class TxtFormatR : ReportReaderCreator
    {

        private readonly IDataDBService _data;

        public TxtFormatR(IDataDBService data)
        {
            _data = data;
        }

        public override IReportReader CreateReport()
        {
            return new TxtReportReader(_data);
        }
    }
}
