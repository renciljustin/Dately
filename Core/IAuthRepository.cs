using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Dately.Core
{
    public interface IAuthRepository
    {
        Task<User> GetByUserNameAsync(string userName);
        Task<User> GetByEmailAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> DeleteUserAsync(User user);
    }
}