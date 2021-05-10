namespace Kaizen.Core.DTOs
{
    public class CampaignDetailQueryDto
    {
        public string Status { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}