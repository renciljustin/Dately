using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dately.Core;
using Dately.Core.Models;
using Dately.Persistence.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dately.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _repo;
        public AuthController(IConfiguration config, IMapper mapper, IAuthRepository repo)
        {
            _config = config;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto model)
        {
            var userFromDb = await _repo.GetByUserNameAsync(model.UserName);

            if (userFromDb == null)
                return Unauthorized("Username is invalid.");

            var passwordCheck = await _repo.CheckPasswordAsync(userFromDb, model.Password);

            if (!passwordCheck)
                return Unauthorized("Password is invalid.");

            var claims = await RenderClaimsAsync(userFromDb);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = RenderToken(claims, credentials);

            return Ok(new { token });
        }

        private async Task<List<Claim>> RenderClaimsAsync(User userFromDb)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userFromDb.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, userFromDb.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userFromDb.Email)
            };

            var roles = await _repo.GetRolesAsync(userFromDb);
            claims.AddRange(roles.Select(r => new Claim("roles", r)));

            return claims;
        }

        private string RenderToken(List<Claim> claims, SigningCredentials credentials)
        {
            var token = new JwtSecurityToken(
                _config["Token:Issuer"],
                _config["Token:Audience"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto model)
        {
            if (await _repo.GetByUserNameAsync(model.UserName) != null)
                return BadRequest("Username is already used.");

            if (await _repo.GetByEmailAsync(model.Email) != null)
                return BadRequest("Email is already used.");

            var userToCreate = _mapper.Map<User>(model);

            var createUserResult = await _repo.CreateUserAsync(userToCreate, model.Password);

            if (!createUserResult.Succeeded)
                return BadRequest("Registration failed.");

            var addRoleResult = await _repo.AddToRoleAsync(userToCreate);

            if (!createUserResult.Succeeded)
            {
                await _repo.DeleteUserAsync(userToCreate);
                return BadRequest("Registration failed.");
            }

            var userFromDb = await _repo.GetByUserNameAsync(userToCreate.UserName);
            
            return CreatedAtRoute("", _mapper.Map<UserForDetailDto>(userFromDb));
        }
    }
}