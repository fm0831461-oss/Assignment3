using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Repo.Interface;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Repo.IMP
{
    public class BorrowCustomRepo:IBorrowCustomRepo
    {
        private readonly AppDbContext context;
        public BorrowCustomRepo(AppDbContext context)
        {
            this.context = context;
        }
        public ICollection<Borrowing> GetAllWithDetails()
        {
            var d = context.Borrows.Include(b => b.Member).Include(b => b.Book).OrderByDescending(b => b.BorrowDate).ToList();
            return d;
        }

        public Borrowing GetByIdWithDetails(int id)
        {
            var d = context.Borrows.Include(b => b.Book).Include(b => b.Member).FirstOrDefault(b => b.BorrowingId == id);
            return d;
        }
    }
}
