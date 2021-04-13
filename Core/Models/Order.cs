using System;

namespace Kaizen.Core.Models
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }

    }
}