using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core;
using Dately.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dately.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly DatelyDbContext _context;
        public UserRepository(DatelyDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}