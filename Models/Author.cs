using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
