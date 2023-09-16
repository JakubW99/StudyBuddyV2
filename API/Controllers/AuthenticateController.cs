
﻿using API.Authentication;
using API.Configuration;
using API.Dto;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly RoleManager<UserRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        public AuthenticateController(UserManager<UserEntity> userManager, RoleManager<UserRole> roleManager, IConfiguration configuration, JwtSettings settings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _jwtSettings = settings;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            var logged = await userManager.FindByNameAsync(user.Username);
            if (await userManager.CheckPasswordAsync(logged, user.Password))
            {
                return Ok(new { Token = CreateToken(logged) });
            }
            return Unauthorized();
        }

        private string CreateToken(UserEntity user)
        {

     
            return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
                .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
                .AddClaim(JwtRegisteredClaimNames.Gender, "male")
                .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
                .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
                .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
                .AddClaim(JwtRegisteredClaimNames.Sub, user.Id)
                .Audience(_jwtSettings.Audience)
                .Issuer(_jwtSettings.Issuer)
                .Encode();
        }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });

        UserEntity user = new UserEntity()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!await roleManager.RoleExistsAsync(UserRole.Admin))
            await roleManager.CreateAsync(new UserRole(UserRole.Admin));
        if (!await roleManager.RoleExistsAsync(UserRole.User))
            await roleManager.CreateAsync(new UserRole(UserRole.User));

        if (await roleManager.RoleExistsAsync(UserRole.User))
        {
            await userManager.AddToRoleAsync(user, UserRole.User);
        }
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again. Password must contain an uppercase, lowercase, number character and special character" });

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }
}
}
