using System;
using System.IO;
using dotnet_code_challenge.Factory;
using dotnet_code_challenge.FileProcessors;
using dotnet_code_challenge.RacePicker;
using dotnet_code_challenge.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Worker>().Work();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(builder => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build());

            services.AddSingleton<Worker>();
            services.AddSingleton<IRacePicker, RacePicker.RacePicker>();
            services.AddSingleton<IFileProcessor, FileProcessor>();
            services.AddSingleton<XmlProcessor>();
            services.AddSingleton<JsonProcessor>();
            services.AddSingleton<ProcessorStrategyFactory>();

            services.AddSingleton(provider => provider.GetService<ProcessorStrategyFactory>().Create());

            services
                .AddLogging(cfg => cfg.AddConsole())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);

            return services;
        }
    }
}
