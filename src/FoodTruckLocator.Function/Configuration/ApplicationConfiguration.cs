using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodTruckLocator.Data.Configuration;

namespace FoodTruckLocator.Function.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public int GetTrucksCount { get; set; }

        public bool ShowHealthCheckExceptions { get; set; }
    }
}
