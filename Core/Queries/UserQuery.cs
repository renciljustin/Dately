using Dately.Shared.Enums;

namespace Dately.Core.Queries
{
    public class UserQuery: BaseQuery
    {
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public Gender? Interest { get; set; }
        public Age? Age { get; set; }
    }
}