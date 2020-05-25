using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using Module = Autofac.Module;

namespace FB.EventSourcing.Application.Modules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = Assembly.GetExecutingAssembly();
            
            var handlers = assemblies.GetTypes().Where(i => i.IsClosedTypeOf(typeof(IRequestHandler<,>)));
            foreach (var handlerType in handlers)
            {
                builder.RegisterType(handlerType);
                var registerAsInterfaceType = handlerType.GetInterfaces().Where(t => t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                    foreach (var item in registerAsInterfaceType)
                {
                    builder.Register(c =>
                    {
                        var handler = c.Resolve(handlerType);
                        return handler;
                    }).As(item);
                }
            }
        }
    }
}