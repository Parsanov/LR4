using LR4.Application.Service;
using LR4.Application.Service.Writers;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;

namespace LR4.Application.Creators.Writers
{
    public class XmlFormatW : ReportWriterCreator
    {
        private readonly IDataDBService _data;
        public XmlFormatW(IDataDBService data)
        {
            _data = data;
        }

        public override string Extension => ".xml";

        public override string ContentType => "application/xml";

        public override string FileName => "report.xml";

        public override IReportWriter CreateReport()
        {
            return new XmlReportWriter(_data);
        }
    }
}
