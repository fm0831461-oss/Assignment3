using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Repo.Interface;

namespace Assignment3.Repo.IMP
{
    public class BookCustomRepository : IBookCustomRepository
    {
        private readonly AppDbContext context;
        public BookCustomRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Book HighestPrice()
        {
            var p = context.Books.OrderByDescending(e => e.Price).FirstOrDefault();
            return p;
        }

        public ICollection<Book> Search(string Title, string Author)
        {
            var s = context.Books.Where(e => e.Title.Contains(Title) || e.Author.Contains(Author)).ToList();
            return s;
        }
    }
}
