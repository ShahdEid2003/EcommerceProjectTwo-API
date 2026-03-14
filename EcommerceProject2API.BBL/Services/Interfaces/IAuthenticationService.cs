using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> Register(RegisterRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<bool> ConfirmEmail(string token, string userId);
       
    }
}
