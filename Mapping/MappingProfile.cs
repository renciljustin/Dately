using AutoMapper;
using Dately.Core.Models;
using Dately.Persistence.Dtos;

namespace Dately.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegisterDto, User>();
            
            CreateMap<User, UserForDetailDto>();
            CreateMap<User, UserForListDto>();
            CreateMap<RefreshToken, RefreshTokenForDisplayDto>();
        }
    }
}