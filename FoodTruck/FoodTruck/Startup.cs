using FoodTruckLocator.Data.Extensions;
using FoodTruckLocator.Function;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]

namespace FoodTruckLocator.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //var appConfiguration = builder.Services.ConfigureAndBindFromLocalSettingsJson<ApplicationConfiguration>();
            //builder.Services.TryAddSingleton<IApplicationConfiguration>(appConfiguration);
            builder.Services.DataService();

            //builder.Services.AddHealthChecks()
              //  .AddCheck<CosmosDbHealthCheck>(nameof(CosmosDbHealthCheck));
            builder.Services.AddLogging();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", true)
                .AddEnvironmentVariables();

            base.ConfigureAppConfiguration(builder);
        }
    }
}
