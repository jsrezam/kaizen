using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Controllers.Resources
{
    public class CustomerResource
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string CompanyName { get; set; }
        public ICollection<OrderResource> Orders { get; set; }
        public CustomerResource()
        {
            this.Orders = new Collection<OrderResource>();
        }
    }
}