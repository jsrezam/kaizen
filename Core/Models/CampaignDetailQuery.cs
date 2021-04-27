using Kaizen.Extensions;

namespace Kaizen.Core.Models
{
    public class CampaignDetailQuery : IQueryObject
    {
        public string Status { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}