using System.ComponentModel.DataAnnotations;

namespace Assignment3.DTOS.Member
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int BorrowingCount { get; set; }
        public string Phone { get; set; }
    }
}
