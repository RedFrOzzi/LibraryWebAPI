using LibraryWebAPI.Models;

namespace LibraryWebAPI.Repository.Interfaces
{
    public interface IBookReaderRepository
    {
        bool CreateReader(string name);
        BookReader GetReader(int id);
        ICollection<BookReader> GetReaders();
        ICollection<BookReader> GetReaders(string name);
        bool ChangeReaderName(BookReader reader, string name);
        bool AddBorrowedBooks(BookReader reader, params Book[] books);
        bool RemoveBorrowedBooks(BookReader reader, params Book[] books);
        bool DeleteReader(BookReader reader);
        bool Save();
    }
}
