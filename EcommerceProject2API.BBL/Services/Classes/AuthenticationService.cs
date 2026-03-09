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
        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user= await _userManager.FindByEmailAsync(request.Email);
            if (user is null) {
                return new LoginResponse() { Success = false, Message = "invalid email" };
            }
            var result =await _userManager.CheckPasswordAsync(user,request.Password);
            if (!result)
            {
                return new LoginResponse() { Success = false, Message = "invalid password" };
            }
            return new LoginResponse() { Success = true, Message = "Success" };
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new RegisterResponse() { Success = false, Message = "Error" }
                ;
            await _userManager.AddToRoleAsync(user, "User");
            return new RegisterResponse() { Success = true, Message = "Success" }
                ;
        }

      
    }
}
