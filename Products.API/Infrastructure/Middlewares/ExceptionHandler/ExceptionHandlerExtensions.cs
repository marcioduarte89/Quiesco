namespace Products.API.Infrastructure.Middlewares.ExceptionHandler
{
    using Api.Infrastructure.Middlewares.ExceptionHandler;
    using Microsoft.AspNetCore.Builder;

    public static class ExceptionHandlerExtensions
    {
        public static void AddCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
