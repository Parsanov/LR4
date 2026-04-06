using LR4.Core.Interfaces;
using LR4.Core.Model;
using LR4.Persistence.Data;
using Microsoft.EntityFrameworkCore;
namespace LR4.Persistence
{
    public class DataDBService : IDataDBService
    {

        private readonly DbDataContext _context;


        public DataDBService(DbDataContext context)
        {
            _context = context;
        }


        public async Task Add(Book book)
        {
            await _context.books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddList(List<Book> books)
        {
            await _context.books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var book = await _context.books.FirstOrDefaultAsync(b => b.Id == Id);
            if (book != null) 
            {
                _context.books.Remove(book);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.books.ToListAsync();
        }

        public async Task Update(int Id)
        {
            var book = await _context.books.FirstOrDefaultAsync(b => b.Id == Id);
            if(book != null) 
            {
                _context.books.Update(book);
                _context.SaveChanges();
            }
                
        }

       
    }
}
