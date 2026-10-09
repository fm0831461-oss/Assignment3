using Assignment3.DTOS.Book;
using Assignment3.DTOS.Borrow;
using Assignment3.Models;
using Assignment3.Repo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public BorrowController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var g = unitOfWork.BorrowRepo.GetAll();
            var result = g.Select(b => new BorrowDto
            {
                BorrowDate=b.BorrowDate,
                BookId=b.BookId,
                BorrowingId=b.BorrowingId,
                MemberId=b.MemberId,
                ReturnDate=b.ReturnDate,
            }).ToList();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateBorrow dto)
        {
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }
            var borrow = new Borrowing
            {
                BorrowDate = dto.BorrowDate,
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                ReturnDate = dto.ReturnDate,
            };
            unitOfWork.BorrowRepo.Create(borrow);
            unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{Id}")]
        public IActionResult Update(UpdateBorrow dto, int Id)
        {
            var f = unitOfWork.BorrowRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }

            f.BorrowDate = dto.BorrowDate;
                f.BookId = dto.BookId;
            f.MemberId = dto.MemberId;
            f.ReturnDate = dto.ReturnDate;

            unitOfWork.BorrowRepo.Update(f);
            unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var f = unitOfWork.BorrowRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            unitOfWork.BorrowRepo.Delete(f);
            unitOfWork.Save();
            return NoContent();
        }
        [HttpGet("GetAllWithDetails")]
        public IActionResult GetAllWithDetails()
        {
            var g = unitOfWork.CustomBorrow.GetAllWithDetails();
            var result = g.Select(b => new BorrowDto
            {
                BorrowDate = b.BorrowDate,
                BookId = b.BookId,
                BorrowingId = b.BorrowingId,
                MemberId = b.MemberId,
                ReturnDate = b.ReturnDate,
            }).ToList();
            return Ok(result);
        }

        [HttpGet("GetByIdWithDetails")]
        public IActionResult GetByIdWithDetails(int Id)
        {
            var b = unitOfWork.CustomBorrow.GetByIdWithDetails(Id);
            if (b == null)
            {
                return NotFound();
            }
            var result = new BorrowDto
            {
                BorrowingId = b.BorrowingId,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate,
                BookId = b.BookId,
                MemberId = b.MemberId,
            };
            return Ok(b);
        }
    }
}
