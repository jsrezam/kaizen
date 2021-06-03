using System;

namespace Kaizen.Core.DTOs
{
    public class OrderQueryDto
    {
        public int? Id { get; set; }
        public int? CampaignId { get; set; }
        public int? CampaignDetailId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCellPhone { get; set; }
        public DateTime? OrderDate { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}