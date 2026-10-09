using System.ComponentModel.DataAnnotations;

namespace Assignment3.DTOS.Book
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int CategoryId { get; set; }
    }
}
