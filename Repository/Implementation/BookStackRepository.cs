using LibraryWebAPI.Data;
using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Repository.Implementation
{
    public class BookStackRepository : IBookStackRepository
    {
        private readonly LibraryDataContext _context;

        public BookStackRepository(LibraryDataContext context)
        {
            _context = context;
        }

        public bool CreateBookStack(BookStack bookStack)
        {
            _context.BookStacks.Add(bookStack);

            return Save();
        }

        public ICollection<BookStack> GetBookStacks()
        {
            return _context.BookStacks.Include(bs => bs.Books).ToList();
        }

        public BookStack GetBookStack(int id)
        {
            return _context.BookStacks.Where(x => x.Id == id).Include(x => x.Books).SingleOrDefault()!;
        }

        public ICollection<BookStack> GetBookStacksByAuthors(Author author)
        {
            return _context.BookStacks
                .Where(bs => bs.Authors.Contains(author))
                .ToList();
        }

        public ICollection<BookStack> GetBookStacksByTitle(string title)
        {
            return _context.BookStacks
                .Where(bs => bs.Title.Contains(title))
                .Take(10)
                .Include(x => x.Books)
                .ToList();
        }

        public bool AddAuthor(BookStack bookStack, Author author)
        {
            bookStack.Authors.Add(author);

            return Save();
        }

        public bool AddBooks(BookStack bookStack, int booksAmount)
        {
            for (int i = 0; i < booksAmount; i++)
            {
                bookStack.Books.Add(new Book()
                {
                    BookStack = bookStack
                });
            }

            return Save();
        }

        public bool DeleteBookStack(BookStack bookStack)
        {
            _context.BookStacks.Remove(bookStack);

            return Save();
        }

        public bool IsStackExist(string title)
        {
            return _context.BookStacks.Where(bs => bs.Title.Contains(title)).Any();
        }

        public bool IsStackExist(int stackId)
        {
            return _context.BookStacks.Where(bs => bs.Id == stackId).Any();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
