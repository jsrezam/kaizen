using System.ComponentModel.DataAnnotations;

namespace Kaizen.Core.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid identification card")]
        public string IdentificationCard { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid home phone")]
        public string HomePhone { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid cell phone")]
        public string CellPhone { get; set; }
        public string PostalCode { get; set; }
        public int CampaignId { get; set; }
        public int CampaignDetailId { get; set; }
        public bool State { get; set; }

    }
}