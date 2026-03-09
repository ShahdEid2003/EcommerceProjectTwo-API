using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public AuthenticationService(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new LoginResponse() { Success = false, Message = "invalid email" };
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new LoginResponse() { Success = false, Message = "invalid password" };
            }
            return new LoginResponse() { Success = true, Message = "Success" };
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var emailurl = $"https://localhost:7186/api/Account/ConfirmEmail?token={token}";
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new RegisterResponse() { Success = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) }
                ;
            await _userManager.AddToRoleAsync(user, "User");
            await _emailSender.SendEmailAsync(user.Email, "welcome", $"<h1>welcome{request.UserName}</h1>" +
                $"" +
                $"<a href='{emailurl}'> confirm</a>");
            return new RegisterResponse() { Success = true, Message = "Success" }
                ;
        }


    }
}
