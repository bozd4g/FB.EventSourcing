using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using FB.EventSourcing.Persistence.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FB.EventSourcing.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EnableLogging();

            var host = CreateHostBuilder(args).Build();

            // for seed data
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                DataGenerator.Seed(context);
            }

            Console.WriteLine("Working on http://localhost:5000");
            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseSerilog(); })
                .ConfigureLogging(
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                    });


        static void EnableLogging()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();

            var connectionString = configuration.GetSection("MongoDb:ConnectionString").Value;
            var collectionName = configuration.GetSection("MongoDb:CollectionName").Value;

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    //.WriteTo.MongoDB(connectionString, collectionName: collectionName)
                    .CreateLogger();

                Log.Warning("Starting the web host");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logger does not work: {ex.Message}");
            }
        }
    }
}