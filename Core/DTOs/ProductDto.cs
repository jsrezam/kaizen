namespace Kaizen.Core.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public CategoryViewDto Category { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}