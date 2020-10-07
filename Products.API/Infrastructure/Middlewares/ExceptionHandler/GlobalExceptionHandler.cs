namespace Products.Api.Infrastructure.Middlewares.ExceptionHandler
{
    using Common.Exceptions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Global Exception Hander
    /// </summary>
    public class GlobalExceptionHandler
    {
        /// <summary>
        /// Map internal error codes to HTTP status code
        /// </summary>
        private static readonly ReadOnlyDictionary<int, HttpStatusCode> CodeToStatusCodeMap = new ReadOnlyDictionary<int, HttpStatusCode>(new Dictionary<int, HttpStatusCode>()
        {
            [ProductException.INVALID_DATA] = HttpStatusCode.BadRequest,
            [ProductException.NOT_FOUND] = HttpStatusCode.NotFound,
            [ProductException.SERVER_ERROR] = HttpStatusCode.InternalServerError
        });

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        /// <param name="env">IWebHostEnvironment</param>
        public GlobalExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        /// <summary>
        /// Generic Invocation
        /// </summary>
        /// <param name="httpContext">Current HttpContext</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ProductException ex)
            {
                await HandleExceptionAsync(httpContext, TranslateException(ex), ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get a proper <see cref="HttpStatusCode"/> based on exception's details
        /// </summary>
        /// <param name="e">An exception</param>
        /// <returns>A status code</returns>
        private HttpStatusCode TranslateException(ProductException e)
        {

            if (CodeToStatusCodeMap.TryGetValue(e.Detail, out var code))
            {
                return code;
            }

            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Handles and re-writes the response with the exception details
        /// </summary>
        /// <param name="context">Http Context</param>
        /// <param name="statusCode">Http Status code</param>
        /// <param name="message">Exception message</param>
        /// <param name="details">Exception details</param>
        /// <param name="invalidProperties">Invalid properties</param>
        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message, string details, IEnumerable<string> invalidProperties = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = (int)statusCode,
                Message = message,
                InvalidProperties = invalidProperties,
                Details = details

            }.ToString());
        }
    }

    /// <summary>
    /// Internal Error Details
    /// </summary>
    internal struct ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public IEnumerable<string> InvalidProperties { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}