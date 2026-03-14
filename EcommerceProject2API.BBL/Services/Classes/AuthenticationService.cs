using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new RegisterResponse() { Success = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) }
                ;
            //لازم ننشأ التوكن بعد عملية التسجيل بالداتا بيس بصرش تحطيها فوق قبل فنكشن انشاء الحساب
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);//عشان الترميز مع المتصفح يفهم انو نفس التوكن التي تم توليدها من قبله
            var emailurl = $"https://localhost:7186/api/Account/ConfirmEmail?token={token}&userId={user.Id}";

            await _userManager.AddToRoleAsync(user, "User");
            await _emailSender.SendEmailAsync(user.Email, "welcome", $"<h1>welcome{request.UserName}</h1>" +
                $"" +
                $"<a href='{emailurl}'> confirm</a>");
            return new RegisterResponse() { Success = true, Message = "Success" }
                ;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new LoginResponse() { Success = false, Message = "invalid email" };
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse() { Success = false, Message = " email is not confirmed" };

            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new LoginResponse() { Success = false, Message = "invalid password" };
            }
            return new LoginResponse() { Success = true, Message = "Success", AccessToken = await GenerateAccessToken(user) };
        }
        private async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
        };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],   // الجهة التي أصدرت التوكن (Auth Server / API)
                audience: _configuration["Jwt:Audience"], // الجهة المسموح لها استخدام التوكن 
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded) return false;
            return true;

        }


    }
}
