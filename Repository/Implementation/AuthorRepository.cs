using LibraryWebAPI.Data;
using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using System.Xml.Linq;

namespace LibraryWebAPI.Repository.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDataContext _context;

        public AuthorRepository(LibraryDataContext context)
        {
            _context = context;
        }

        public bool CreateAuthor(string name)
        {
            _context.Authors.Add(new Author { Name = name });

            return Save();
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.Where(a => a.Id == id).FirstOrDefault()!;
        }

        public ICollection<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public ICollection<Author> GetAuthorsByName(string name)
        {
            return _context.Authors
                .Where(a => a.Name.Contains(name))
                .Take(10)
                .ToList()!;
        }

        public bool ChangeAuthorName(Author author, string name)
        {
            author.Name = name;

            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);

            return Save();
        }

        public bool IsAuthorExist(string name)
        {
            return _context.Authors.Where(a => a.Name.Contains(name)).Any();
        }

        public bool IsAuthorExist(int authorId)
        {
            return _context.Authors.Where(a => a.Id == authorId).Any();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
