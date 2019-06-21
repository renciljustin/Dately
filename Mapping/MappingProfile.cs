using AutoMapper;
using Dately.Core.Models;
using Dately.Core.Queries;
using Dately.Core.Results;
using Dately.Persistence.Dtos;
using Dately.Persistence.QueryDtos;
using Dately.Persistence.ResultDtos;

namespace Dately.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // models
            CreateMap<UserForRegisterDto, User>();
            
            CreateMap<RefreshToken, RefreshTokenForDisplayDto>();
            CreateMap<User, UserForDetailDto>();
            CreateMap<User, UserForListDto>();

            // queries
            CreateMap<BaseQueryDto, BaseQuery>();
            CreateMap<UserQueryDto, UserQuery>();

            // results
            CreateMap<UserResult, UserResultDto>();
        }
    }
}