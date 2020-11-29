namespace Availability.API
{
    using Autofac;
    using Availability.API.Infrastructure.Service;
    using FluentValidation.AspNetCore;
    using Infrastructure.Middlewares.ExceptionHandler;
    using Infrastructure.Services;
    using Infrastructure.Swagger;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Reflection;

    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {

        private readonly IHostEnvironment _environment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">environment</param>
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<MongoConfigurationService>();
            services.AddHostedService<NServiceBusService>();
            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    x.UseCamelCasing(false);
                    x.SerializerSettings.Converters.Add(new StringEnumConverter());
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }).AddFluentValidation(c =>
                {
                    c.RegisterValidatorsFromAssemblyContaining<Startup>();
                    c.ImplicitlyValidateChildProperties = true;
                }); ;

            if (!_environment.IsProduction())
            {
                services.ConfigureSwagger();
            }
        }

        /// <summary>
        /// Configure Container will be called after running ConfigureServices
        /// Any registration here will override registrations made in ConfigureServices
        /// Don't need to build the container as its done automatically
        /// </summary>
        /// <param name="builder">Container builder</param>
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!_environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"v1/swagger.json", "Availability API");
                });
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.AddCustomExceptionHandler();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
