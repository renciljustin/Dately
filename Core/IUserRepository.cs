using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core.Models;
using Dately.Core.Queries;

namespace Dately.Core
{
    public interface IUserRepository
    {
         Task<IEnumerable<User>> GetUsersAsync(UserQuery query);
         Task<User> GetUserAsync(string id);
    }
}