using System.Security.Claims;
using System.Threading.Tasks;
using Kaizen.Core.DTOs;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(ApplicationUser user, string userPassword);
        Task<ResponseAuthenticationDto> BuildToken(ApplicationUser user, bool IsNew = false);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<bool> AddRoleAsync(ApplicationUser user, string roleName);
        Task<bool> RemoveRoleAsync(ApplicationUser user, string roleName);
    }
}