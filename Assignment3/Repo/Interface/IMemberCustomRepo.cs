using Assignment3.Models;

namespace Assignment3.Repo.Interface
{
    public interface IMemberCustomRepo
    {
        ICollection<Member> TopFive();
        ICollection<Member> BorringCount();
        Member GetStatistics(int id);

    }
}
