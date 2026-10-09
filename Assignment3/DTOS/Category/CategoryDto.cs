using System.ComponentModel.DataAnnotations;

namespace Assignment3.DTOS.Category
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
