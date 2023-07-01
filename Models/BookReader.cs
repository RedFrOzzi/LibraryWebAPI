using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class BookReader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> BorrowedBooks { get; set; } = new List<Book>();
    }
}
