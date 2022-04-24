using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodTruckLocator.Data.Models;
using FoodTruckLocator.Data.Services;

namespace FoodTruckLocator.Test.Stubs
{
    public class UnhealthyFileDataLoaderServiceStub : IDataLoaderService
    {
        public bool HealthCheck()
        {
            return false;
        }

        public IEnumerable<FoodTruck> LoadData()
        {
            return null;
        }
    }
}
