using Assignment3.DTOS.Book;
using Assignment3.Models;
using Assignment3.Repo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public BookController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var g = unitOfWork.BookRepo.GetAll();
            var result = g.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Price = b.Price,
                IsAvailable = b.IsAvailable,
                CategoryId = b.CategoryId
            }).ToList();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateBook dto)
        {
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }
            var Book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                IsAvailable = dto.IsAvailable,
                Price = dto.Price,
                CategoryId=dto.CategoryId
            };
            unitOfWork.BookRepo.Create(Book);
            unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{Id}")]
        public IActionResult Update(updateBook dto,int Id)
        {
            var f = unitOfWork.BookRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }

            f.Title = dto.Title;
            f.Author = dto.Author;
            f.ISBN = dto.ISBN;
            f.IsAvailable = dto.IsAvailable;
            f.Price = dto.Price;
            f.CategoryId = dto.CategoryId;
         
            unitOfWork.BookRepo.Update(f);
            unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var f = unitOfWork.BookRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            unitOfWork.BookRepo.Delete(f);
            unitOfWork.Save();
            return NoContent();
        }
        [HttpGet("Search")]
        public IActionResult Search(string Title,string Author)
        {
            var g = unitOfWork.CustomBook.Search(Title,Author);
            var result = g.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Price = b.Price,
                IsAvailable = b.IsAvailable,
                CategoryId = b.CategoryId
            }).ToList();
            return Ok(result);
        }

        //[HttpGet("HighestPrice")]
        //public IActionResult HighestPrice()
        //{
        //    var g = unitOfWork.CustomBook.HighestPrice();

        //    return Ok(g);
        //}
        
        [HttpGet("HighestPrice")]
        public IActionResult HighestPrice()
        {
            var book = unitOfWork.CustomBook.HighestPrice();

            if (book == null)
                return NotFound("No books found");

            return Ok(new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Price = book.Price,
                IsAvailable = book.IsAvailable,
                CategoryId = book.CategoryId
            });
        }
    }
}
