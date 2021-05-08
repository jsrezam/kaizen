namespace Kaizen.Controllers.Resources
{
    public class AgentCustomerResource
    {
        public CustomerResource Customer { get; set; }
        public int CampaignId { get; set; }
        public int CampaignDetailId { get; set; }
    }
}