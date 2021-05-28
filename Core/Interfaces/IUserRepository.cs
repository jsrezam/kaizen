using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetActiveAgentsAsync();
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<QueryResult<UserViewModel>> GetUsersViewAsync(ApplicationUserQuery queryObj);
        Task<UserViewModel> GetUserViewAsync(string userId);
        void ChangeUserState(ApplicationUser user);
    }
}