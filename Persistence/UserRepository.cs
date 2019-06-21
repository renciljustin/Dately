using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dately.Core;
using Dately.Core.Models;
using Dately.Core.Queries;
using Dately.Shared.Extensions;
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
        public async Task<IEnumerable<User>> GetUsersAsync(UserQuery query)
        {
            var users = _context.Users
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.Roles.Any(r => r.Role.Name == "User"))
                .AsQueryable();

            if (query.Name != null)
                users = users.Where(u => (u.FirstName + ' ' + u.LastName).Contains(query.Name));

            if (query.Gender != null)
                users = users.Where(u => u.Gender == query.Gender);

            var columnsMap = new Dictionary<string, Expression<Func<User, object>>>
            {
                ["name"] = user => user.FirstName + ' ' + user.LastName,
                ["age"] = user => user.BirthDate
            };

            users = users.ApplyOrdering(query, columnsMap);

            users = users.ApplyPaging(query);

            return await users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}