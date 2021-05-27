using System;
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
        public int TotalCallsNumber { get; set; }
        public string LastCallDuration { get; set; }
        public DateTime LastCallDate { get; set; }
        public string LastValidCallDuration { get; set; }
        public DateTime LastValidCallDate { get; set; }
        public string State { get; set; }

        public ICollection<Order> Orders { get; set; }

        public CampaignDetail()
        {
            this.Orders = new Collection<Order>();
        }
    }
}