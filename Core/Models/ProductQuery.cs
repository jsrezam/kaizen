using Kaizen.Infrastructure.Extensions;

namespace Kaizen.Core.Models
{
    public class ProductQuery : IQueryObject
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}