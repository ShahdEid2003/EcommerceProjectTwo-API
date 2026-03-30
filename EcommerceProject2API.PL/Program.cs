
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
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            builder.Services.AddLocalization(options => options.ResourcesPath = "");
            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
             new CultureInfo(defaultCulture),
             new CultureInfo("ar")
            };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                //options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider { QueryStringKey = "lang" });
                options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
            });
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ISeedData, RoleSeedData>();
            builder.Services.AddTransient<BBL.Services.Interfaces.IEmailSender, EmailSender>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;//0-9
                options.Password.RequireLowercase= true;//a-z
                options.Password.RequireUppercase= true;//A-Z
                options.Password.RequireNonAlphanumeric = true;//!@#$%
                options.Password.RequiredLength = 10;
                options.Lockout.MaxFailedAccessAttempts = 5;//after 5 attempts lock 
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);//after 5 minutes lock

            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();//using for generate token 

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
                    )
                };
            });

            builder.Services.AddAuthorization();//jwt
            MapsterConfig.MapsterConfigRegister();


            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseCors(MyAllowSpecificOrigins);
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
