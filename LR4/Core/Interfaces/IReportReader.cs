using LR4.Core.Model;

namespace LR4.Core.Interfaces
{
    public interface IReportReader
    {
        Task GenerateReaderReport(byte[] fileData);
    }
}
