using System;

namespace Kaizen.Controllers.Resources
{
    public class OrderResource
    {
        public int CampaignDetailId { get; set; }
        public CampaignDetailResource CampaignDetail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
    }
}