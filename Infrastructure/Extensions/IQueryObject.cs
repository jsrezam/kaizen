namespace Kaizen.Infrastructure.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
        bool ApplyPagingFromClient { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}