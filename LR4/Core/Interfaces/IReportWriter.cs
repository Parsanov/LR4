using LR4.Core.Model;

namespace LR4.Core.Interfaces
{
    public interface IReportWriter
    {
        Task<byte[]> GenerateWriteReport();
    }
}
