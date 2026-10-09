namespace Assignment3.DTOS.Member
{

    public class MemberStatisticsDto
    {
        public string FullName { get; set; }
        public int TotalBorrowed { get; set; }
        public int ReturnedBooks { get; set; }
        public int CurrentlyBorrowed { get; set; }
    }
}
