using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.Models
{
    public class Campaign : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<CampaignDetail> CampaignDetails { get; set; }

        public Campaign()
        {
            this.CampaignDetails = new Collection<CampaignDetail>();
        }
    }
}