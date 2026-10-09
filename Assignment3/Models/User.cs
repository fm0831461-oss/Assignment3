using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
         public string PasswrdHash { get; set; }
        [Required]
         public string Role { get; set; }
    }
}
