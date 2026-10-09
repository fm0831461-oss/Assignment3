using Assignment3.Models;

namespace Assignment3.Repo.Interface
{
    public interface IUserRepository
    {
        User GetByUserName(string userName);
    }
}
