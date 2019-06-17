using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(IConfiguration config,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto model)
        {
            var userFromDb = await _userManager.FindByNameAsync(model.UserName);

            if (userFromDb == null)
                return Unauthorized("Username is invalid.");

            var passwordCheck = await _userManager.CheckPasswordAsync(userFromDb, model.Password);

            if (!passwordCheck)
                return Unauthorized("Password is invalid.");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userFromDb.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, userFromDb.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userFromDb.Email)
            };

            var roles = await _userManager.GetRolesAsync(userFromDb);
            claims.AddRange(roles.Select(r => new Claim("roles", r)));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _config["Token:Issuer"],
                _config["Token:Audience"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return BadRequest("Username is already used.");

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("Email is already used.");

            var userToCreate = _mapper.Map<User>(model);

            var createUserResult = await _userManager.CreateAsync(userToCreate, model.Password);

            if (!createUserResult.Succeeded)
                return BadRequest("Registration failed.");

            var addRoleResult = await _userManager.AddToRoleAsync(userToCreate, "User");

            if (!createUserResult.Succeeded)
            {
                await _userManager.DeleteAsync(userToCreate);
                return BadRequest("Registration failed.");
            }

            var userFromDb = await _userManager.FindByNameAsync(userToCreate.UserName);
            
            return CreatedAtRoute("", _mapper.Map<UserForDetailDto>(userFromDb));
        }
    }
}