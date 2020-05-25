using Autofac;
using FB.EventSourcing.Persistence;
using FB.EventSourcing.Persistence.EntityFrameworkCore;
using Module = Autofac.Module;

namespace FB.EventSourcing.Application.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}