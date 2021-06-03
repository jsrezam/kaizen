namespace Kaizen.Core.DTOs
{
    public class SalesByProductReportViewModelDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalSales { get; set; }
    }
}