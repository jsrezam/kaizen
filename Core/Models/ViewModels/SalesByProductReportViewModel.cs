namespace Kaizen.Core.Models.ViewModels
{
    public class SalesByProductReportViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalSales { get; set; }
    }
}