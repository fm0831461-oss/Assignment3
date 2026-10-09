using Assignment3.Models;

namespace Assignment3.Repo.Interface
{
    public interface IBookCustomRepository
    {
        ICollection<Book> Search(string Title, string Author);
        Book HighestPrice();
    }
}
