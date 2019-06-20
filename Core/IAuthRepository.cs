using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Dately.Core
{
    public interface IAuthRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserByEmailAsync(string userName);
        Task<SignInResult> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> DeleteUserAsync(User user);
        RefreshToken CreateRefreshToken(string userId);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        RefreshToken UpdateRefreshToken(RefreshToken refreshToken);
        void RevokeToken(RefreshToken token);
    }
}