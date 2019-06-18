using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core;
using Dately.Core.Models;
using Dately.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dately.Persistence
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DatelyDbContext _context;
        private readonly IConfiguration _config;

        public AuthRepository(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, DatelyDbContext context)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<User> GetByEmailAsync(string userName)
        {
            return await _userManager.FindByEmailAsync(userName);
        }

        public async Task<SignInResult> CheckPasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user)
        {
            return await _userManager.AddToRoleAsync(user, RolePrefix.User);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public RefreshToken CreateRefreshToken(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                ExpiryDate = DateTime.UtcNow.AddHours(3),
                Revoked = false,
                CreationTime = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);

            return refreshToken;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string token)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token && !t.Revoked);
        }

        public RefreshToken UpdateRefreshToken(RefreshToken refreshToken)
        {
            refreshToken.ExpiryDate = DateTime.UtcNow.AddHours(3);
            refreshToken.TotalRefresh++;
            refreshToken.LastModified = DateTime.UtcNow;
            return refreshToken;
        }
    }
}