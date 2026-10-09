using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Repo.Interface;

namespace Assignment3.Repo.IMP
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public User GetByUserName(string userName)
        {
            return context.Users
                .FirstOrDefault(u => u.UserName == userName);
        }
    }
}
