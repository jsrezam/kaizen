using System;

namespace Kaizen.Core.DTOs
{
    public class CampaignQueryDto
    {
        public DateTime? FinishDate { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}