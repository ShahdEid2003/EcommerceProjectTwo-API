using EcommerceProject2API.DAL.Data;
using System.Collections;
using Microsoft.EntityFrameworkCore;
namespace EcommerceProject2API.PL.Extensions
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection Services, IConfiguration Configuration)
        {
           Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );
            return Services;
        }
    }
}
