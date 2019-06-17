using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core;
using Dately.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Dately.Persistence
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
           return await _userManager.FindByNameAsync(userName);
        }

        public async Task<User> GetByEmailAsync(string userName)
        {
           return await _userManager.FindByEmailAsync(userName);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user)
        {
            return await _userManager.AddToRoleAsync(user, "User");
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }
    }
}