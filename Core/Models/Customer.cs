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
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string CompanyName { get; set; }
        public ICollection<CampaignDetail> CampaignDetails { get; set; }
        public Customer()
        {
            this.CampaignDetails = new Collection<CampaignDetail>();
        }
    }
}