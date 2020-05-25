using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FB.EventSourcing.Api.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddMapper(this IServiceCollection services)
        { 
            services.AddAutoMapper(typeof(Startup));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(new []
                {
                    "FB.EventSourcing.Domain",
                    "FB.EventSourcing.Application.Contracts",
                });
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}