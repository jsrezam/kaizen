namespace Kaizen.Controllers.Resources
{
    public class CampaignDetailResource
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int CustomerId { get; set; }
        public CustomerResource Customer { get; set; }
        public int CallsNumber { get; set; }
        public decimal CallDuration { get; set; }
        public string Status { get; set; }
    }
}