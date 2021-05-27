using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetActiveAgentsAsync();
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string userId);
    }
}