namespace Products.API.Infrastructure.Middlewares.ExceptionHandler
{
    using Api.Infrastructure.Middlewares.ExceptionHandler;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Exception Handler Exceptions
    /// </summary>
    public static class ExceptionHandlerExtensions
    {
        /// <summary>
        /// Adds custom Exception Handler middleware
        /// </summary>
        /// <param name="builder"></param>
        public static void AddCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
