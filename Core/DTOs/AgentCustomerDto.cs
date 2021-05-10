namespace Kaizen.Core.DTOs
{
    public class AgentCustomerDto
    {
        public CustomerDto Customer { get; set; }
        public int CampaignId { get; set; }
        public int CampaignDetailId { get; set; }
    }
}