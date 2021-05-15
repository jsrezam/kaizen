using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Kaizen.Core.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public ICollection<ProductDto> Products { get; set; }
        public CategoryDto()
        {
            this.Products = new Collection<ProductDto>();
        }
    }
}