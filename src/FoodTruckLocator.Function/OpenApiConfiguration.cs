using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace FoodTruckLocator.Function
{
    public class OpenApiConfiguration : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiVersionType OpenApiVersion => GetOpenApiVersion();

        public override OpenApiInfo Info => new OpenApiInfo
        {
            Version = GetOpenApiDocVersion(),
            Title = "San Francisco Food Trucks API",
            Description = "Service that allows consumers to find food trucks in San Francisco"
        };
    }
}
