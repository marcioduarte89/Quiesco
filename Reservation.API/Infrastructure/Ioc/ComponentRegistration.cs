namespace Reservation.API.Infrastructure.Ioc
{
    using Autofac;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Registers all application generic components (like services, repositories, etc)
    /// </summary>
    public class ComponentRegistration : Module {
        /// <summary>
        /// Loads the container builder and registers generic components
        /// </summary>
        /// <param name="builder">Container builder</param>
        //protected override void Load(ContainerBuilder builder) {
        //    builder.Register(context => {
        //        var configuration = context.Resolve<IConfiguration>();
        //        var mainConnectionString = configuration.GetConnectionString("Main");
        //        var opt = new DbContextOptionsBuilder<ProductsContext>();
        //        opt.UseSqlServer(mainConnectionString);
        //        return new ProductsContext(opt.Options);
        //    }).InstancePerLifetimeScope();
        //}
    }
}
