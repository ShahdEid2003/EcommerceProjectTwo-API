using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace EcommerceProject2API.PL.Extensions
{
    public static class CorsPolicy
    {
        public const string PolicyName = "_myAllowSpecificOrigins";
        public static IServiceCollection AddCorsPolicy(this IServiceCollection Services)
        {
            

            Services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });
            return Services;
        }
    }
}
