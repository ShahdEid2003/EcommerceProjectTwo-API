using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject2API.PL.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;//0-9
                options.Password.RequireLowercase = true;//a-z
                options.Password.RequireUppercase = true;//A-Z
                options.Password.RequireNonAlphanumeric = true;//!@#$%
                options.Password.RequiredLength = 10;
                options.Lockout.MaxFailedAccessAttempts = 5;//after 5 attempts lock 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//after 5 minutes lock

            }).AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();//using for generate token 
            return Services;
        }
    }
}
