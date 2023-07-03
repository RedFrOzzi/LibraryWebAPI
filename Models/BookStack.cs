using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebAPI.Models
{
    public class BookStack
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
        [NotMapped]
        public int BooksAmount => Books.Count;
    }
}
