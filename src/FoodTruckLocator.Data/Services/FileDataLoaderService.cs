using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using FoodTruckLocator.Data.Models;

namespace FoodTruckLocator.Data.Services
{
    public class FileDataLoaderService : IDataLoaderService
    {
        public IEnumerable<FoodTruck> LoadData()
        {
            using (var reader = new StreamReader("Mobile_Food_Facility_Permit.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<FoodTruckClassMap>();
                return csv.GetRecords<FoodTruck>().Where(x => x.Longitude.HasValue && x.Latitude.HasValue).ToList();
            }
        }

        public bool HealthCheck()
        {
            return true;
        }
    }
}
