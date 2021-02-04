namespace Availability.API.Infrastructure.Swagger {
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Swagger Configuration
    /// </summary>
    public static class Swagger {
        /// <summary>
        /// Configures Swagger through ServiceCollection
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services) {
            services
                .AddSwaggerGen(c => {
                    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Availability API", Version = "V1" });
                    c.CustomSchemaIds(t => t.FullName);
                    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}Availability.API.xml");
                });
        }
    }
}
