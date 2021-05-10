using System.ComponentModel.DataAnnotations;

namespace Kaizen.Core.DTOs
{
    public class UserCredentialsDto
    {
        [EmailAddress]
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(255)]
        [Required]
        public string LastName { get; set; }
        [StringLength(255)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(255)]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid identification card")]
        public string IdentificationCard { get; set; }
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; }
    }
}