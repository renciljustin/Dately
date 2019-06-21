using System.Collections.Generic;
using Dately.Persistence.Dtos;

namespace Dately.Persistence.ResultDtos
{
    public class UserResultDto
    {
        public IEnumerable<UserForListDto> Users { get; set; }
        public long Total { get; set; }
    }
}