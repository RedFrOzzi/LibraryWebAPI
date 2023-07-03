using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebAPI.Models
{
    public class BookReader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> BorrowedBooks { get; set; } = new List<Book>();
        [NotMapped]
        public int BorrowedBooksCount => BorrowedBooks.Count;
    }
}
