using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dately.Core;
using Dately.Core.Models;
using Dately.Core.Queries;
using Dately.Core.Results;
using Dately.Shared.Enums;
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
        public async Task<UserResult> GetUsersAsync(UserQuery query)
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
                
            if (query.Interest != null)
                users = users.Where(u => u.Interest == query.Interest);

            if (query.Age != null)
            {
                var yearToday = DateTime.UtcNow.Year - 1;

                if (query.Age == Age.Teen)
                    users = users.Where(u => yearToday - u.BirthDate.Value.Year >= 18 && yearToday - u.BirthDate.Value.Year <= 30);
            
                 if (query.Age == Age.Adult)
                    users = users.Where(u => yearToday - u.BirthDate.Value.Year > 30);
            }

            var columnsMap = new Dictionary<string, Expression<Func<User, object>>>
            {
                ["age"] = user => user.BirthDate,
                ["creationtime"] = user => user.CreationTime,
                ["name"] = user => user.FirstName + ' ' + user.LastName
            };

            users = users.ApplyOrdering(query, columnsMap);

            var total = await users.LongCountAsync();

            users = users.ApplyPaging(query);

            var userResult = new UserResult
            {
                Users = await users.ToListAsync(),
                Total = total
            };

            return userResult;
        }

        public async Task<User> GetUserAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}