using LibraryWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Data
{
    public class LibraryDataContext : DbContext
    {
        public LibraryDataContext(DbContextOptions<LibraryDataContext> options) : base(options)
        {            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookStack> BookStacks { get; set; }
        public DbSet<BookReader> BookReaders { get; set; }
    }
}
