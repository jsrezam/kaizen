using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Core.Models
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

        public Category()
        {
            this.Products = new Collection<Product>();
        }
    }
}