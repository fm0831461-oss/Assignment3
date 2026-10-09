using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    //[Index(nameof(ISBN),IsUnique =true)]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]

        public string ISBN { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public int Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int CategoryId { get;set;  }
        public Category Category {  get; set;}
        public ICollection<Borrowing> Borrows { get; set; } = new List<Borrowing>();

    }
}
