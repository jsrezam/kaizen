using Kaizen.Infrastructure.Extensions;

namespace Kaizen.Core.Models
{
    public class OrderDetailQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}