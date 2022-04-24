using FoodTruckLocator.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckLocator.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void DataService(this IServiceCollection services)
        {
            // services
            services.AddScoped<IDataSFService, DataSFService>();
            services.AddTransient<IDataLoaderService, FileDataLoaderService>();
            services.AddTransient<IValidationService, ValidationService>();

            // telemetry
            /*services.AddApplicationInsightsTelemetry();
            services.AddScoped(typeof(ITelemetryService), typeof(TelemetryService));
            services.TryAddSingleton(typeof(ITelemetryClientWrapper), typeof(TelemetryClientWrapper));*/
        }
    }
}
