using AutoMapper;
using Dately.Core.Models;
using Dately.Core.Queries;
using Dately.Persistence.Dtos;
using Dately.Persistence.QueryDtos;

namespace Dately.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // entities
            CreateMap<UserForRegisterDto, User>();
            
            CreateMap<RefreshToken, RefreshTokenForDisplayDto>();
            CreateMap<User, UserForDetailDto>();
            CreateMap<User, UserForListDto>();

            // queries
            CreateMap<BaseQueryDto, BaseQuery>();
            CreateMap<UserQueryDto, UserQuery>();
        }
    }
}