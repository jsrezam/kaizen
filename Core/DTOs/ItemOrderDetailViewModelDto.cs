namespace Kaizen.Core.DTOs
{
    public class ItemOrderDetailViewModelDto
    {
        public ProductDto Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Import { get; set; }
        public decimal Discount { get; set; }
    }
}