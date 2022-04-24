using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckLocator.Data.Configuration
{
    public interface IApplicationConfiguration
    {
        public int GetTrucksCount { get; }
        bool ShowHealthCheckExceptions { get; }
    }
}
