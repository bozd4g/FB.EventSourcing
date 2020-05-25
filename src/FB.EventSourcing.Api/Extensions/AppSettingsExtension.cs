
using FB.EventSourcing.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FB.EventSourcing.Api.Extensions
{
    public static class AppSettingsExtension
    {
        public static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettingsSection.Bind(appSettings);
            
            services.Configure<AppSettings>(appSettingsSection);
        }
    }
}