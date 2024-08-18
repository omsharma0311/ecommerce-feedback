using Newtonsoft.Json;
using System.Net;

namespace ECommerceFeedback.Common
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = JsonConvert.SerializeObject(new { error = ex.Message });
                await context.Response.WriteAsync(errorResponse);
            }
        }
    }
}
