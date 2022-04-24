using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTruckLocator.Function.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static T ConfigureAndBindFromLocalSettingsJson<T>(this IServiceCollection services) where T : class, new()
        {
            var configuration = services.GetRegisteredServices<IConfiguration>();

            T retVal = new T();
            configuration.Bind(retVal);

            return retVal;

        }

        public static T GetRegisteredServices<T>(this IServiceCollection services)
        {
            return services.BuildServiceProvider()
                           .GetService<T>();

        }
    }
}
