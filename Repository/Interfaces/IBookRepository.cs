using LibraryWebAPI.Models;

namespace LibraryWebAPI.Repository.Interfaces
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        ICollection<Book> GetBooks();
        bool RemoveBook(Book book);
        bool Save();
    }
}
