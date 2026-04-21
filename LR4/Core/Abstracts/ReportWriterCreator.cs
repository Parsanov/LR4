using LR4.Core.Interfaces;

namespace LR4.Core.Abstracts
{
    public abstract class ReportWriterCreator
    {
        public abstract string Extension { get; }
        public abstract string ContentType { get; }
        public abstract string FileName { get; }
        public abstract IReportWriter CreateReport();

        public async Task<byte[]> ExportReport()
        {
            IReportWriter report = CreateReport();
            return await report.GenerateWriteReport();
        }
    }
}
