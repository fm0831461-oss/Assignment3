using Assignment3.DTOS.Book;
using Assignment3.DTOS.Category;
using Assignment3.Models;
using Assignment3.Repo.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var g = unitOfWork.CategoryRepo.GetAll();
            var result = g.Select(b => new CategoryDto
            {
               CategoryId= b.CategoryId,
               Description=b.Description,
               Name=b.Name,
            }).ToList();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateCategory dto)
        {
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }
            var Book = new Category
            {
                Name = dto.Name,
                Description=dto.Description,
            };
            unitOfWork.CategoryRepo.Create(Book);
            unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{Id}")]
        public IActionResult Update(UpdateCategoty dto, int Id)
        {
            var f = unitOfWork.CategoryRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            if (dto == null)
            {
                return BadRequest("Please Enter All Data");
            }
            f.Description = dto.Description;
            f.Name = dto.Name;
          
            unitOfWork.CategoryRepo.Update(f);
            unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var f = unitOfWork.CategoryRepo.GetById(Id);
            if (Id == null)
            {
                return NotFound();
            }
            unitOfWork.CategoryRepo.Delete(f);
            unitOfWork.Save();
            return NoContent();
        }
    }
}
