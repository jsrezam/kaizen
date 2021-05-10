using System.Threading.Tasks;
using Kaizen.Core.DTOs;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(ApplicationUser user, string userPassword);
        Task<ResponseAuthenticationDto> BuildToken(UserCredentialsDto userCredentialsResource, bool IsNew = false);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
    }
}