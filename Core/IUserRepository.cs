using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Core
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAgentUsersAsync();
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string userId);
    }
}