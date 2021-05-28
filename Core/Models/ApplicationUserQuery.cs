using Kaizen.Infrastructure.Extensions;

namespace Kaizen.Core.Models
{
    public class ApplicationUserQuery : IQueryObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationCard { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}