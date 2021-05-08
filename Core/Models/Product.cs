using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Core.Models
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool IsDiscontinued { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            this.OrderDetails = new Collection<OrderDetail>();
        }
    }
}