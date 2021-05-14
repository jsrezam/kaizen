using Kaizen.Infrastructure.Extensions;

namespace Kaizen.Core.Models
{
    public class CustomerQuery : IQueryObject
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string IdentificationCard { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}