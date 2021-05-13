using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Kaizen.Core.Models
{
    public class Customer : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        public string IdentificationCard { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public bool State { get; set; }
        public ICollection<CampaignDetail> CampaignDetails { get; set; }
        public Customer()
        {
            this.CampaignDetails = new Collection<CampaignDetail>();
        }
    }
}