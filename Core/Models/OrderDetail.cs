using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Core.Models
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public Order order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
    }
}