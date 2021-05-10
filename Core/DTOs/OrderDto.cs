using System;

namespace Kaizen.Core.DTOs
{
    public class OrderDto
    {
        public int CampaignDetailId { get; set; }
        public CampaignDetailDto CampaignDetail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
    }
}