using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Book
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public BookStack BookStack { get; set; }
        public BookReader? CurrentReader { get; set; }
    }
}
