using System.Reflection;
using Autofac;
using FB.EventSourcing.Application.Contracts.Dependency;
using Module = Autofac.Module;

namespace FB.EventSourcing.Application.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = Assembly.GetExecutingAssembly();
            
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IInstancePerLifetimeScope>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IInstancePerDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<ISingleInstance>()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IInstancePerRequest>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}