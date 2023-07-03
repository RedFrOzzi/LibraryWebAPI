using LibraryWebAPI.Models;

namespace LibraryWebAPI.Repository.Interfaces
{
    public interface IBookStackRepository
    {
        bool CreateBookStack(BookStack bookStack);
        BookStack GetBookStack(int id);
        ICollection<BookStack> GetBookStacks();
        ICollection<BookStack> GetBookStacksByTitle(string title);
        bool AddAuthor(BookStack bookStack, Author  author);
        bool AddBooks(BookStack bookStack, int booksAmount);
        bool DeleteBookStack(BookStack bookStack);
        bool IsStackExist(string title);
        bool IsStackExist(int stackId);
        bool Save();
    }
}
