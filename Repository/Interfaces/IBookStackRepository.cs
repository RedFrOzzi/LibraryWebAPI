using LibraryWebAPI.Models;

namespace LibraryWebAPI.Repository.Interfaces
{
    public interface IBookStackRepository
    {
        bool CreateBookStack(string title);
        ICollection<BookStack> GetBookStacks();
        BookStack GetBookStack(int id);
        ICollection<BookStack> GetBookStacksByTitle(string title);
        ICollection<BookStack> GetBookStacksByAuthors(string author);
        bool AddAuthor(int bookStackId, int  authorId);
        bool AddBooks(int  bookStackId, int booksAmount);
        bool Save();
    }
}
