using FoodTruckLocator.Data.Configuration;
using FoodTruckLocator.Data.Extensions;
using FoodTruckLocator.Function;
using FoodTruckLocator.Function.Configuration;
using FoodTruckLocator.Function.Extensions;
using FoodTruckLocator.Function.HealthChecks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace FoodTruckLocator.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var appConfiguration = builder.Services.ConfigureAndBindFromLocalSettingsJson<ApplicationConfiguration>();
            builder.Services.TryAddSingleton<IApplicationConfiguration>(appConfiguration);
            builder.Services.DataService();

            builder.Services.AddHealthChecks()
                   .AddCheck<FileDataLoaderHealthCheck>(nameof(FileDataLoaderHealthCheck));
            builder.Services.AddLogging();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            /*builder.ConfigurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", true)
                .AddEnvironmentVariables();

            base.ConfigureAppConfiguration(builder);*/
        }
    }
}
