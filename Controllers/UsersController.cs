using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dately.Core;
using Dately.Core.Queries;
using Dately.Persistence.Dtos;
using Dately.Persistence.QueryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dately.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryDto query)
        {
            var usersFromDb = await _repo.GetUsersAsync(_mapper.Map<UserQuery>(query));

            return Ok(_mapper.Map<List<UserForListDto>>(usersFromDb));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var userFromDb = await _repo.GetUserAsync(id);

            if (userFromDb is null)
                return BadRequest(userFromDb);

            return Ok(_mapper.Map<UserForDetailDto>(userFromDb));
        }
    }
}