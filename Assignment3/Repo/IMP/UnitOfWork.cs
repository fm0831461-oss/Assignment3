using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Repo.Interface;

namespace Assignment3.Repo.IMP
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;
        public IGenericRepo<Book> BookRepo { get; }
        public IGenericRepo<Member> MemebrRepo { get; }
        public IGenericRepo<Category> CategoryRepo { get; }
        public IGenericRepo<Borrowing> BorrowRepo { get; }
        public IBookCustomRepository CustomBook { get; }
        public IBorrowCustomRepo CustomBorrow { get;  }
        public IMemberCustomRepo CustomMember { get;  }
       public IUserRepository User { get; }
        public UnitOfWork(AppDbContext context, IGenericRepo<Book> BookRepo, IGenericRepo<Member> MemebrRepo,
            IGenericRepo<Category> CategoryRepo, IGenericRepo<Borrowing> BorrowRepo, IBookCustomRepository CustomBook,
            IBorrowCustomRepo CustomBorrow, IMemberCustomRepo CustomMember, IUserRepository User)
        {
            this.context = context;
            this.BookRepo = BookRepo;
            this.MemebrRepo = MemebrRepo;
            this.CategoryRepo = CategoryRepo;
            this.BorrowRepo = BorrowRepo;
            this.CustomBook = CustomBook;
            this.CustomBorrow = CustomBorrow;
            this.CustomMember = CustomMember;
            this.User = User;
        }
         public int Save()
        {
            return context.SaveChanges();
        }
    }
}
