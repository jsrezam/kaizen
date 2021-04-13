using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Core.Models
{
    public class Employee : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Extension { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Employee()
        {
            this.Customers = new Collection<Customer>();
            this.Orders = new Collection<Order>();
        }
    }
}