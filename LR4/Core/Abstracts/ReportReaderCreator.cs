using LR4.Core.Interfaces;
using LR4.Core.Model;

namespace LR4.Core.Abstracts
{
    public abstract class ReportReaderCreator
    {
        public abstract IReportReader CreateReport();
        public async Task ImportReport(byte[] fileData)
        {
            IReportReader report = CreateReport();
            await report.GenerateReaderReport(fileData);
        }
    }
}
