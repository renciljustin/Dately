using Dately.Shared.Enums;

namespace Dately.Persistence.QueryDtos
{
    public class UserQueryDto: BaseQueryDto
    {
        public string Name { get; set; }
        public Gender? Gender { get; set; }
    }
}