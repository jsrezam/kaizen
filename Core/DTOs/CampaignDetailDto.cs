namespace Kaizen.Core.DTOs
{
    public class CampaignDetailDto
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public int CallTimes { get; set; }
        public string CallDuration { get; set; }
        public string Status { get; set; }
    }
}