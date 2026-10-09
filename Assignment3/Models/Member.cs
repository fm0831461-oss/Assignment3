using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    //[Index(nameof(Email),IsUnique =true)]
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required]

        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public ICollection<Borrowing> Borrows { get; set; } = new List<Borrowing>();

    }
}
