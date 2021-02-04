namespace Reservation.API.Infrastructure.Ioc
{
    using Autofac;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    using Reservations.Infrastructure.Data.Repositories;
    using System.Linq;

    /// <summary>
    /// Registers all application generic components (like services, repositories, etc)
    /// </summary>
    public class ComponentRegistration : Module
    {
        /// <summary>
        /// Loads the container builder and registers generic components
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var mainConnectionString = configuration.GetConnectionString("Main");
                var database = configuration.GetConnectionString("Database");
                var client = new MongoClient(mainConnectionString);
                return client.GetDatabase(database);
            }).InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(IWriteReservationRepository).Assembly)
                .Where(t => t.GetInterfaces().Any())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
