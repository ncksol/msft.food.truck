using System.Globalization;
using CsvHelper;
using FoodTruckLocator.Data.Models;
using Geolocation;

namespace FoodTruckLocator.Data.Services
{
    public class DataSFService : IDataSFService
    {
        private List<FoodTruck> _allFoodTrucks = new List<FoodTruck>();

        public DataSFService()
        {
            using (var reader = new StreamReader("Mobile_Food_Facility_Permit.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<FoodTruckClassMap>();
                _allFoodTrucks = new List<FoodTruck>(csv.GetRecords<FoodTruck>());
            }
        }

        public FoodTruck GetFoodTruck(double latitude, double longitude)
        {
            var currentLocation = new Coordinate(latitude, longitude);

            return _allFoodTrucks.FirstOrDefault();
        }
    }
}