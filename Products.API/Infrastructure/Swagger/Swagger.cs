namespace Products.API.Infrastructure.Swagger
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class Swagger
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Products API", Version = "V1" });
                    c.CustomSchemaIds(t => t.FullName);
                    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}Products.API.xml");
                });
        }
    }
}
