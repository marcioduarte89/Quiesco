namespace Products.API.Infrastructure.Ioc
{
    using Autofac;
    using MediatR;
    using System.Reflection;
    using Module = Autofac.Module;

    public class MediatorRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {

                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[] {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }

        }
    }
}
