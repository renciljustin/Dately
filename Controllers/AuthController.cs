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
        private readonly IUnitOfWork _uow;
        public AuthController(IConfiguration config, IMapper mapper, IAuthRepository repo, IUnitOfWork uow)
        {
            _config = config;
            _mapper = mapper;
            _repo = repo;
            _uow = uow;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto model)
        {
            var userFromDb = await _repo.GetByUserNameAsync(model.UserName);

            if (userFromDb == null)
                return Unauthorized("Username is invalid.");

            var passwordCheck = await _repo.CheckPasswordAsync(userFromDb, model.Password);

            if (!passwordCheck.Succeeded)
                return Unauthorized("Password is invalid.");

            var token = await GenerateTokenAsync(userFromDb);

            var refreshToken = _repo.CreateRefreshToken(userFromDb.Id);

            await _uow.SaveChangesAsync();

            return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = _mapper.Map<RefreshTokenForDisplayDto>(refreshToken)
                }
            );
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

        [HttpPost("{refreshToken}/refresh")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var refreshTokenFromDb = await _repo.GetRefreshTokenAsync(refreshToken);

            if (refreshTokenFromDb == null)
                return NotFound("Token not found");

            if (refreshTokenFromDb.ExpiryDate < DateTime.UtcNow)
                return Unauthorized("Refresh token is expired.");

            var userFromDb = await _repo.GetUserByIdAsync(refreshTokenFromDb.UserId);

            var token = await GenerateTokenAsync(userFromDb);

            var newRefreshToken = _repo.UpdateRefreshToken(refreshTokenFromDb);
            
            await _uow.SaveChangesAsync();

            return Ok(new 
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = _mapper.Map<RefreshTokenForDisplayDto>(newRefreshToken)
                }
            );
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            await Task.Delay(1000);
            return Ok();
        }

        #region Generate Token

        private async Task<JwtSecurityToken> GenerateTokenAsync(User user)
        {
            var claims = await RenderClaimsAsync(user);
            var credentials = RenderCredentials();

            var token = new JwtSecurityToken(
                _config["Token:Issuer"],
                _config["Token:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );

            return token;
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
            claims.AddRange(roles.Select(r => new Claim("role", r)));

            return claims;
        }

        private SigningCredentials RenderCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            return credentials;
        }

        #endregion
    }
}