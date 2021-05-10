using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaizen.Core.DTOs
{
    public class OrderSaveDto
    {
        public int CampaignDetailId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public ICollection<OrderDetailDto> OrderDetails { get; set; }
        public OrderSaveDto()
        {
            this.OrderDetails = new Collection<OrderDetailDto>();
        }
    }
}