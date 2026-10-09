using Assignment3.Models;

namespace Assignment3.Repo.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepo<Book>BookRepo{ get;  }
        IGenericRepo<Member>MemebrRepo{ get;  }
        IGenericRepo<Category>CategoryRepo{ get;  }
        IGenericRepo<Borrowing>BorrowRepo{ get;  }
        IBookCustomRepository CustomBook { get; }
        IBorrowCustomRepo CustomBorrow { get; }
        IMemberCustomRepo CustomMember { get; }
        IUserRepository User { get; }
        int Save();
    }
}
