namespace Products.API.Infrastructure.Ioc
{
    using Autofac;
    using AutoMapper;
    using System.Reflection;
    using Module = Autofac.Module;

    public class ProfileRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(ITypeConverter<,>)).AsSelf().SingleInstance();
        }
    }
}
