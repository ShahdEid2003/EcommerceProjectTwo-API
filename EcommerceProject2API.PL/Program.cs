
using EcommerceProject2API.BBL.Services.Classes;
using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using EcommerceProject2API.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EcommerceProject2API.BBL.Mapping;
using EcommerceProject2API.PL.Extensions;


namespace EcommerceProject2API.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            //CORS
            builder.Services.AddCorsPolicy();

            //db
            builder.Services.AddDatabaseServices(builder.Configuration);
            //Identity
            builder.Services.AddIdentityServices();
            //Auth
            builder.Services.AddJwtAuthentication(builder.Configuration);
            //Localization
            builder.Services.AddLocalizationServices();
            //ApplicationServices
            builder.Services.AddAplicationServices(builder.Configuration);

            builder.Services.AddAuthorization();//jwt
            MapsterConfig.MapsterConfigRegister();


            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseCors(CorsPolicy.PolicyName);
            app.UseHttpsRedirection();
            app.UseAuthentication();//jwt
            app.UseAuthorization();
            app.UseStaticFiles();//Â«Ì ⁄‘«‰ «·’Ê— „‰ «·(wwwroot)  ÊŒ– «·’Ê—…
            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }
            app.Run();
        }
    }
}
