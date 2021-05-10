using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.DTOs
{
    public class CampaignSaveDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Progress { get; set; }
        public ICollection<CustomerDto> Customers { get; set; }
        public CampaignSaveDto()
        {
            this.Customers = new Collection<CustomerDto>();
        }
    }
}