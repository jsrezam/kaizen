using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(ApplicationUser user, string userPassword);
        Task<IdentityResult> AddClaimsAsync(ApplicationUser user, List<Claim> claims);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
    }
}