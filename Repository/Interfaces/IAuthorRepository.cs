using LibraryWebAPI.Models;

namespace LibraryWebAPI.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        bool CreateAuthor(string name);
        Author GetAuthor(int id);
        ICollection<Author> GetAuthorsByName(string name);
        ICollection<Author> GetAuthors();
        bool ChangeAuthorName(Author author, string name);
        bool DeleteAuthor(Author author);
        bool IsAuthorExist(string name);
        bool IsAuthorExist(int authorId);
        bool Save();
    }
}
