namespace Kaizen.Controllers.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public CategoryViewResource Category { get; set; }
        public string Name { get; set; }
        public int QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}