using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

namespace EcommerceProject2API.PL.Extensions
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationServices(this IServiceCollection Services)
        {
            Services.AddLocalization(options => options.ResourcesPath = "");
            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
             new CultureInfo(defaultCulture),
             new CultureInfo("ar")
            };

            Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Clear();
                //options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider { QueryStringKey = "lang" });
                options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
            });
            return Services;
        }
    }
}
