using LibraryWebAPI.Data;
using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDataContext _context;

        public BookRepository(LibraryDataContext context)
        {
            _context = context;
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault()!;
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.Include(b => b.BookStack).Include(b => b.CurrentReader).ToList();
        }

        public bool RemoveBook(Book book)
        {
            _context.Books.Remove(book);

            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
