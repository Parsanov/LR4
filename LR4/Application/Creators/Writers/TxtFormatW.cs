using LR4.Application.Service;
using LR4.Application.Service.Writers;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;

namespace LR4.Application.Creators.Writers
{
    public class TxtFormatW : ReportWriterCreator
    {
        private readonly IDataDBService _data;

        public TxtFormatW(IDataDBService data)
        {
            _data = data;
        }

        public override IReportWriter CreateReport()
        {
            return new TxtReportWriter(_data);
        }
    }
}
