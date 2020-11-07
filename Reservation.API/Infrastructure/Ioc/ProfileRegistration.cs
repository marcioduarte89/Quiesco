namespace Reservation.API.Infrastructure.Ioc {
    using Autofac;
    using AutoMapper;
    using System.Reflection;
    using Module = Autofac.Module;

    /// <summary>
    /// Registers AutoMapper profiles and related configuration
    /// </summary>
    public class ProfileRegistration : Module {
        /// <summary>
        /// Loads the container builder and registers Automapper components
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder) {

            builder.Register(ctx => new MapperConfiguration(cfg => {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(ITypeConverter<,>)).AsSelf().SingleInstance();
        }
    }
}
