using System.ComponentModel.DataAnnotations;

namespace Assignment3.DTOS.Usre
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string PasswrdHash { get; set; }
    }
}

