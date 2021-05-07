namespace Kaizen.Controllers.Resources
{
    public class ProductQueryResource
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}