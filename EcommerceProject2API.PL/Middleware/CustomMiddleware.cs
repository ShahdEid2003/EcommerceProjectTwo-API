namespace EcommerceProject2API.PL.Middleware
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddleware>();
        }
    }
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next){
            _next = next;
        }
        public async Task Invoke(HttpContext context) {
            Console.WriteLine("processing Request");
            await _next(context);
            Console.WriteLine("processing Response");
        }
    }
}
