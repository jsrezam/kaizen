using System;

namespace Kaizen.Core.DTOs
{
    public class CampaignDetailDto
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public int TotalCallsNumber { get; set; }
        public string LastCallDuration { get; set; }
        public DateTime LastCallDate { get; set; }
        public string LastValidCallDuration { get; set; }
        public DateTime LastValidCallDate { get; set; }
        public string State { get; set; }
    }
}