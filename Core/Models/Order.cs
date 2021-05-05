using System;

namespace Kaizen.Core.Models
{
    public class Order : BaseEntity
    {
        public int CampaignDetailId { get; set; }
        public CampaignDetail CampaignDetail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }

    }
}