using Kaizen.Extensions;

namespace Kaizen.Core.Models
{
    public class EmployeeQuery : IQueryObject
    {
        public string LastName { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}