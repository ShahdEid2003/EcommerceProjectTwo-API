using EcommerceProject2API.DAL.DTO.Response;

namespace EcommerceProject2API.PL.Middleware
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandling(RequestDelegate next ){
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var errorDetails = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error Server",
                    InnerError = e.StackTrace
                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(errorDetails);
            }
        }
    }
}
