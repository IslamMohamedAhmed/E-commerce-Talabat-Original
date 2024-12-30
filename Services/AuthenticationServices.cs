using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationServices(UserManager<User> userManager) : IAuthenticationServices
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) {
                throw new UnauthorizedException("Email is not found!");
            }
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!result) throw new UnauthorizedException();

            return new UserResultDto(user.DisplayName, user.Email, "token");
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User() { 
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            UserName = registerDto.UserName,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(s=>s.Description).ToList();
                throw new ValidationsException(errors);
            }
            return new UserResultDto(user.DisplayName, user.Email, "token");
        }
    }
}
