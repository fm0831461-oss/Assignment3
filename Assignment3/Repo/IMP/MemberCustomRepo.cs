using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Repo.Interface;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Repo.IMP
{
    public class MemberCustomRepo : IMemberCustomRepo
    {
        private readonly AppDbContext context;
        public MemberCustomRepo(AppDbContext context)
        {
            this.context = context;
        }
        public ICollection<Member> BorringCount()
        {
            var c = context.Members.OrderByDescending(m => m.Borrows.Count()).ToList();
            return c;
        }
        public ICollection<Member> TopFive()
        {
            var t = context.Members.OrderByDescending(e => e.Borrows).Take(5).ToList();
            return t;
        }
        public Member GetStatistics(int id)
        {
            var member = context.Members
                .Include(m => m.Borrows)
                .FirstOrDefault(m => m.MemberId == id);

            return member;
        }
    }
}
