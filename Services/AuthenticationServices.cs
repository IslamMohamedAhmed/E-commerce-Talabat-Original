using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services
{
    public class AuthenticationServices(UserManager<User> userManager, IOptions<JwtOptions> options) : IAuthenticationServices
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                throw new UnauthorizedException("Email is not found!");
            }
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnauthorizedException();

            return new UserResultDto(user.DisplayName, user.Email, await GetToken(user));
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(s => s.Description).ToList();
                throw new ValidationsException(errors);
            }
            return new UserResultDto(user.DisplayName, user.Email, await GetToken(user));
        }

        private async Task<string> GetToken(User user)
        {
            var JwtOptions = options.Value;
            var authClaim = new List<Claim> {
            new Claim(ClaimTypes.Name,user.UserName!),
            new Claim(ClaimTypes.Email,user.Email!),
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: JwtOptions.Audience,
                signingCredentials: signingCreds,
                claims: authClaim,
                issuer: JwtOptions.Issuer,
                expires: DateTime.UtcNow.AddDays(JwtOptions.PeriodInDays)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
