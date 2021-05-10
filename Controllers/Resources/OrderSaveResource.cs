using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Controllers.Resources
{
    public class
    OrderSaveResource
    {
        public int CampaignDetailId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public ICollection<OrderDetailResource> OrderDetails { get; set; }
        public OrderSaveResource()
        {
            this.OrderDetails = new Collection<OrderDetailResource>();
        }
    }
}