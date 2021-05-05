namespace Kaizen.Controllers.Resources
{
    public class CampaignDetailResource
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int CustomerId { get; set; }
        public CustomerResource Customer { get; set; }
        public int CallTimes { get; set; }
        public string CallDuration { get; set; }
        public string Status { get; set; }
    }
}