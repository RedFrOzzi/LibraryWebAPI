using LibraryWebAPI.Data;
using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Repository.Implementation
{
    public class BookReaderRepository : IBookReaderRepository
    {
        private readonly LibraryDataContext _context;

        public BookReaderRepository(LibraryDataContext context) 
        {
            _context = context;
        }

        public bool CreateReader(string name)
        {
            _context.BookReaders.Add(new BookReader { Name = name });

            return Save();
        }

        public BookReader GetReader(int id)
        {
            return _context.BookReaders
                .Where(br =>  br.Id == id)
                .Include(br => br.BorrowedBooks)
                .FirstOrDefault()!;
        }

        public ICollection<BookReader> GetReaders()
        {
            return _context.BookReaders.ToList();
        }

        public ICollection<BookReader> GetReaders(string name)
        {
            return _context.BookReaders
                .Where(br => br.Name.Contains(name))
                .Take(10)
                .ToList();
        }

        public bool ChangeReaderName(BookReader reader, string name)
        {
            reader.Name = name;

            return Save();
        }

        public bool AddBorrowedBooks(BookReader reader, params Book[] books)
        {
            for (int i = 0; i < books.Length; i++)
            {
                reader.BorrowedBooks.Add(books[i]);
            }

            return Save();
        }

        public bool RemoveBorrowedBooks(BookReader reader, params Book[] books)
        {
            for (int i = 0; i < books.Length; i++)
            {
                reader.BorrowedBooks.Add(books[i]);
            }

            return Save();
        }

        public bool DeleteReader(BookReader reader)
        {
            _context.BookReaders.Remove(reader);

            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
