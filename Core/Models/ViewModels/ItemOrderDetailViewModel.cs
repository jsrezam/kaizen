namespace Kaizen.Core.Models.ViewModels
{
    public class ItemOrderDetailViewModel
    {
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Import { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}