using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string IdentificationCard { get; set; }
        public bool IsActive { get; set; }
    }
}