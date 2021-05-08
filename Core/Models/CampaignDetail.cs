using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.Models
{
    public class CampaignDetail : BaseEntity
    {
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int CallTimes { get; set; }
        public string CallDuration { get; set; }
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; }

        public CampaignDetail()
        {
            this.Orders = new Collection<Order>();
        }
    }
}