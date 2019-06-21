using System;
using System.Threading.Tasks;
using Dately.Core.Models;
using Dately.Core.Queries;
using Dately.Core.Results;

namespace Dately.Core
{
    public interface IUserRepository
    {
         Task<UserResult> GetUsersAsync(UserQuery query);
         Task<User> GetUserAsync(string id);
    }
}