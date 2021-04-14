using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Core.Models
{
    public class Customer : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string CompanyName { get; set; }

        public bool IsAssigned { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            this.Orders = new Collection<Order>();
        }
    }
}