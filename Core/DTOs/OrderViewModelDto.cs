using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.DTOs
{
    public class OrderViewModelDto
    {
        public CustomerDto Customer { get; set; }
        public int CampaignId { get; set; }
        public int CampaignDetailId { get; set; }
        public ICollection<ItemOrderDetailViewModelDto> OrderDetailViewModel { get; set; }

        public OrderViewModelDto()
        {
            this.OrderDetailViewModel = new Collection<ItemOrderDetailViewModelDto>();
        }
    }
}