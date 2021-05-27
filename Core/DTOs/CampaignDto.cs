using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.DTOs
{
    public class CampaignDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Progress { get; set; }
        public ModelDateDto ModelFinishDate { get; set; }
        public ICollection<CampaignDetailDto> CampaignDetails { get; set; }

        public CampaignDto()
        {
            this.CampaignDetails = new Collection<CampaignDetailDto>();
        }
    }
}