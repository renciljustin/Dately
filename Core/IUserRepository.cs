using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dately.Core.Models;

namespace Dately.Core
{
    public interface IUserRepository
    {
         Task<IEnumerable<User>> GetUsersAsync();
         Task<User> GetUserAsync(string id);
    }
}