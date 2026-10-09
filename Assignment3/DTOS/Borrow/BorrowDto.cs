using System.ComponentModel.DataAnnotations;

namespace Assignment3.DTOS.Borrow
{
    public class BorrowDto
    {
        public int BorrowingId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
    }
}
