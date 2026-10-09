using Assignment3.Models;

namespace Assignment3.Repo.Interface
{
    public interface IBorrowCustomRepo
    {
        ICollection<Borrowing> GetAllWithDetails();
        Borrowing GetByIdWithDetails(int Id);
    }
}
