namespace Kaizen.Core.DTOs
{
    public class OrderDetailQueryDto
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}