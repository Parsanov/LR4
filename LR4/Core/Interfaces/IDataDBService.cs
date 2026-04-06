using LR4.Core.Model;

namespace LR4.Core.Interfaces
{
    public interface IDataDBService
    {
        Task Add(Book book);
        Task AddList(List<Book> books);
        Task<IEnumerable<Book>> Get();
        Task Delete(int id);
        Task Update(int id);
    }
}
