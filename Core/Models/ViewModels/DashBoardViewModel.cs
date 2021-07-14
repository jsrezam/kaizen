using System;
using System.Collections.Generic;

namespace Kaizen.Core.Models.ViewModels
{
    public class DashBoardViewModel
    {
        public DateTime OrderDate { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public double? TotalImport { get; set; }
    }
}