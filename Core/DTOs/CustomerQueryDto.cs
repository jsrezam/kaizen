namespace Kaizen.Core.DTOs
{
    public class CustomerQueryDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string IdentificationCard { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public bool ApplyPagingFromClient { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}