using LibraryWebAPI.Data;
using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net;

namespace LibraryWebAPI.Repository.Implementation
{
    public class BookStackRepository : IBookStackRepository
    {
        private readonly LibraryDataContext _context;

        public BookStackRepository(LibraryDataContext context)
        {
            _context = context;
        }

        public bool CreateBookStack(string title)
        {
            if (_context.BookStacks.Where(bs => bs.Title.Contains(title)).Any())
            {
                return false;
            }

            var bookStack = new BookStack { Title = title };

            _context.BookStacks.Add(bookStack);

            return Save();
        }

        public ICollection<BookStack> GetBookStacks()
        {
            return _context.BookStacks.ToList();
        }

        public BookStack GetBookStack(int id)
        {
            return _context.BookStacks.Where(x => x.Id == id).SingleOrDefault();
        }

        public ICollection<BookStack> GetBookStacksByAuthors(string author)
        {
            return _context.BookStacks
                .Where(bs => EF.Functions.Like(bs.Title, $"%{author}%"))
                .ToList();
        }

        public ICollection<BookStack> GetBookStacksByTitle(string title)
        {
            return _context.BookStacks
                .Where(bs => EF.Functions.Like(bs.Title, $"%{title}%"))
                .ToList();
        }

        public bool AddAuthor(int bookStackId, int authorId)
        {
            var bookStack = _context.BookStacks.Where(bs => bs.Id == bookStackId).FirstOrDefault();

            if (bookStack == null)
            {
                return false;
            }

            var author = _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();

            if (author == null)
            {
                return false;
            }

            bookStack.Authors.Add(author);

            return Save();
        }

        public bool AddBooks(int bookStackId, int booksAmount)
        {
            if (booksAmount <= 0)
            {
                return false;
            }

            var bookStack = _context.BookStacks.Where(bs => bs.Id == bookStackId).FirstOrDefault();

            if (bookStack == null)
            {
                return false;
            }

            for (int i = 0; i < booksAmount; i++)
            {
                bookStack.Books.Add(new Book()
                {
                    BookStack = bookStack
                });
            }

            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
