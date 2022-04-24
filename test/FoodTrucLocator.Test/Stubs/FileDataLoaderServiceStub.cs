using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using FoodTruckLocator.Data.Models;
using FoodTruckLocator.Data.Services;
using Newtonsoft.Json;

namespace FoodTruckLocator.Test.Stubs
{
    public class FileDataLoaderServiceStub : IDataLoaderService
    {
        public bool HealthCheck()
        {
            return true;
        }

        public IEnumerable<FoodTruck> LoadData()
        {
            var trucks = JsonConvert.DeserializeObject<IEnumerable<FoodTruck>>(File.ReadAllText("dataStub.json"));
            return trucks;
        }

    }
}
