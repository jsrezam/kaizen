using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kaizen.Controllers.Common;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly KaizenDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountRepository(KaizenDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IdentityResult> SignUpAsync(ApplicationUser user, string userPassword)
        {
            return await userManager.CreateAsync(user, userPassword);
        }

        public async Task<IdentityResult> AddClaimsAsync(ApplicationUser user, List<Claim> claims)
        {
            return await userManager.AddClaimsAsync(user, claims);
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await userManager.GetClaimsAsync(user);
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public async Task<bool> AddRoleAsync(ApplicationUser user, string roleName)
        {
            return (await userManager
            .AddClaimAsync(user, new Claim(Policies.RoleClaimTypeValue, roleName)))
            .Succeeded;
        }

        public async Task<bool> RemoveRoleAsync(ApplicationUser user, string roleName)
        {
            return (await userManager
            .RemoveClaimAsync(user, new Claim(Policies.RoleClaimTypeValue, roleName)))
            .Succeeded;
        }
    }
}