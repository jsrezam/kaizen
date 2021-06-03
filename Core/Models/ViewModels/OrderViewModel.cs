using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.Models.ViewModels
{
    public class OrderViewModel
    {
        public Customer Customer { get; set; }
        public int CampaignId { get; set; }
        public int CampaignDetailId { get; set; }
        public ICollection<ItemOrderDetailViewModel> OrderDetailViewModel { get; set; }

        public OrderViewModel()
        {
            this.OrderDetailViewModel = new Collection<ItemOrderDetailViewModel>();
        }
    }
}