using EcommerceProject2API.BBL.Services.Classes;
using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using EcommerceProject2API.DAL.Utils;

using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using EcommerceProject2API.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

using System.Text;

namespace EcommerceProject2API.PL.Extensions
{
    public static  class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddTransient<BBL.Services.Interfaces.IEmailSender, EmailSender>();
            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IBrandRepository, BrandRepository>();
            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped<ICartService, CartService>();
            Services.AddScoped<ICartRepository, CartRepository>();
            return Services;
        }
    }
}
