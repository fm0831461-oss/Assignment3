namespace Assignment3.DTOS.Borrow
{
    public class UpdateBorrow
    {
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
    }
}
