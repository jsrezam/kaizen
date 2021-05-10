using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ApplicationUser>> GetAgentUsersAsync()
        {
            return await (from u in context.Users
                          join uc in context.UserClaims on u.Id equals uc.UserId
                          where uc.ClaimType.Equals("role") && uc.ClaimValue.Equals("agent")
                          select new ApplicationUser
                          {
                              Id = u.Id,
                              LastName = u.LastName,
                              FirstName = u.FirstName,
                              UserName = u.UserName,
                              Email = u.Email
                          }).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        // public async Task<IEnumerable<ApplicationUser>> GetUsersByAgent(string agentId)
        // {
        //     var userCampaigns = await (from u in context.Users
        //                   join cp in context.Campaigns on u.Id equals cp.UserId
        //                   where u.Id.Equals(agentId) && cp.IsActive && cp.FinishDate > DateTime.UtcNow
        //                   select new Campaign
        //                   {
        //                       CampaignDetails = cp.CampaignDetails                              
        //                   }).ToListAsync();
        // }
    }
}