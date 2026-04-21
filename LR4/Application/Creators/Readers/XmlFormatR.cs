using LR4.Application.Service.Readers;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;

namespace LR4.Application.Creators.Readers
{
    public class XmlFormatR : ReportReaderCreator
    {

        private readonly IDataDBService _data;

        public XmlFormatR(IDataDBService data)
        {
            _data = data;
        }

        public override string Extension => ".xml";

        public override IReportReader CreateReport()
        {
            return new XmlReportReader(_data);
        }
    }
}
