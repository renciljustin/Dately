using System.Collections.Generic;
using Dately.Core.Models;

namespace Dately.Core.Results
{
    public class UserResult
    {
        public IEnumerable<User> Users { get; set; }
        public long Total { get; set; }
    }
}