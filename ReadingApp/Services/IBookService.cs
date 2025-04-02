using ReadingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingApp.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(string id);
        Task<Book> AddBookAsync(string filePath);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(string id);
        Task<List<TextPage>> GetBookPagesAsync(Book book);
        Task<TextPage?> GetPageAsync(Book book, int pageNumber);
    }
} 