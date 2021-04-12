using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Kaizen.Controllers.Resources
{
    public class CategoryResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductResource> Products { get; set; }
        public CategoryResource()
        {
            this.Products = new Collection<ProductResource>();
        }
    }
}