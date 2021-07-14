using Kaizen.Controllers.Common;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Kaizen.Infrastructure.Extensions;
using Kaizen.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kaizen.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly KaizenDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public UserRepository(KaizenDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }
        public async Task<bool> isUniqueCellphone(string cellPhone)
        {
            var user = await context.Users
            .SingleOrDefaultAsync(u => u.PhoneNumber.Equals(cellPhone));
            return user == null;
        }

        public async Task<IEnumerable<ApplicationUser>> GetActiveAgentsAsync()
        {
            return await (from u in context.Users
                          join uc in context.UserClaims on u.Id equals uc.UserId
                          where uc.ClaimType.Equals(Policies.RoleClaimTypeValue) && uc.ClaimValue.Equals(Policies.AgentRoleValue) && u.IsActive
                          select new ApplicationUser
                          {
                              Id = u.Id,
                              LastName = u.LastName,
                              FirstName = u.FirstName,
                              UserName = u.UserName,
                              PhoneNumber = u.PhoneNumber,
                              IdentificationCard = u.IdentificationCard,
                              Email = u.Email
                          }).ToListAsync();
        }

        public async Task<QueryResult<UserViewModel>> GetUsersViewAsync(ApplicationUserQuery queryObj)
        {
            var result = new QueryResult<UserViewModel>();

            var query = (from u in context.Users
                         join uc in context.UserClaims on u.Id equals uc.UserId
                         where uc.ClaimType.Equals(Policies.RoleClaimTypeValue)
                         select new UserViewModel
                         {
                             Id = u.Id,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             IdentificationCard = u.IdentificationCard,
                             UserName = u.UserName,
                             Email = u.Email,
                             Role = uc.ClaimValue,
                             IsActive = u.IsActive
                         }).AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<UserViewModel, object>>>()
            {
                ["firstName"] = u => u.FirstName,
                ["lastName"] = u => u.LastName,
                ["identificationCard"] = u => u.IdentificationCard,
                ["userName"] = u => u.UserName,
                ["email"] = u => u.Email
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<UserViewModel> GetUserViewAsync(string userId)
        {
            return await (from u in context.Users
                          join uc in context.UserClaims on u.Id equals uc.UserId
                          where uc.ClaimType.Equals(Policies.RoleClaimTypeValue)
                          && u.Id.Equals(userId)
                          select new UserViewModel
                          {
                              Id = u.Id,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              IdentificationCard = u.IdentificationCard,
                              UserName = u.UserName,
                              Email = u.Email,
                              Role = uc.ClaimValue,
                              IsActive = u.IsActive
                          }).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public void ChangeUserState(ApplicationUser user)
        {
            context.Update(user);
        }
    }
}