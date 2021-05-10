using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.DTOs
{
    public class CampaignSaveResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserResource User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Progress { get; set; }
        public ICollection<CustomerResource> Customers { get; set; }
        public CampaignSaveResource()
        {
            this.Customers = new Collection<CustomerResource>();
        }
    }
}