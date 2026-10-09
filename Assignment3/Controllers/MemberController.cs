using Assignment3.DTOS.Book;
using Assignment3.DTOS.Member;
using Assignment3.Models;
using Assignment3.Repo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public MemberController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var g = unitOfWork.MemebrRepo.GetAll();
            var result = g.Select(b => new MemberDto
            {
               Email=b.Email,
               FullName=b.FullName,
               Phone=b.Phone,
               MemberId=b.MemberId,
            }).ToList();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateMember dto)
        {
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }
            var member = new Member
            {
             Email=dto.Email,
             Phone=dto.Phone,
             FullName=dto.FullName
            };
            unitOfWork.MemebrRepo.Create(member);
            unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{Id}")]
        public IActionResult Update(UpdateMember dto, int Id)
        {
            var f = unitOfWork.MemebrRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }

            f.Email = dto.Email;
            f.Phone = dto.Phone;
            f.FullName = dto.FullName;

            unitOfWork.MemebrRepo.Update(f);
            unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var f = unitOfWork.MemebrRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            unitOfWork.MemebrRepo.Delete(f);
            unitOfWork.Save();
            return NoContent();
        }
        [HttpGet("Top5")]

        public IActionResult GetTopFive()
        {
            var g = unitOfWork.CustomMember.TopFive();
            var result = g.Select(b => new MemberDto
            {
                Email = b.Email,
                FullName = b.FullName,
                Phone = b.Phone,
                MemberId = b.MemberId,
            }).ToList();
            return Ok(result);
        }

        [HttpGet("BorrowCount")]

        public IActionResult BorrowCount()
        {
            var g = unitOfWork.CustomMember.BorringCount();
            var result = g.Select(b => new MemberDto
            {
                Email = b.Email,
                FullName = b.FullName,
                Phone = b.Phone,
                MemberId = b.MemberId,
                BorrowingCount = b.Borrows.Count(),
            }).ToList();
            return Ok(result);
        }


        [HttpGet("{id}/statistics")]
        public IActionResult Statistics(int id)
        {
            var member = unitOfWork.CustomMember.GetStatistics(id);

            if (member == null)
                return NotFound("Member not found");

            var result = new MemberStatisticsDto
            {
                FullName = member.FullName,

                TotalBorrowed = member.Borrows.Count(),

                ReturnedBooks = member.Borrows
                    .Where(b => b.ReturnDate != null)
                    .Count(),

                CurrentlyBorrowed = member.Borrows
                    .Where(b => b.ReturnDate == null)
                    .Count()
            };

            return Ok(result);
        }

    }
}
