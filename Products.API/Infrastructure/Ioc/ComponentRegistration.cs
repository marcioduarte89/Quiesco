using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Products.Infrastructure.Data;

namespace Products.API.Infrastructure.Ioc {
    public class ComponentRegistration : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.Register(context => {
                var configuration = context.Resolve<IConfiguration>();
                var mainConnectionString = configuration.GetConnectionString("Main");
                var opt = new DbContextOptionsBuilder<ProductsContext>();
                opt.UseSqlServer(mainConnectionString);
                return new ProductsContext(opt.Options);
            }).InstancePerLifetimeScope();
        }
    }
}
