using System.ComponentModel.DataAnnotations;

namespace Kaizen.Controllers.Resources
{
    public class UserCredentialsResource
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}