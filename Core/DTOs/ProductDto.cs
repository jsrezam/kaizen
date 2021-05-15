using System.ComponentModel.DataAnnotations;

namespace Kaizen.Core.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public CategoryViewDto Category { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "please enter valid unit price")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = " unit price must have 2 decimal places")]
        public decimal UnitPrice { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "please enter valid stock number")]
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}