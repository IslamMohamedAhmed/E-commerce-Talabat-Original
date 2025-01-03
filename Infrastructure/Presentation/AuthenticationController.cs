using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class AuthenticationController(IServicesManager servicesManager) : ApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResultDto>> login(LoginDto loginDto)
        {
            var user = await servicesManager.AuthenticationServices.LoginAsync(loginDto);
            return Ok(user);
        } 
        [HttpPost("register")]
        public async Task<ActionResult<UserResultDto>> register(RegisterDto registerDto)
        {
            var user = await servicesManager.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        } 

       
    }
}
