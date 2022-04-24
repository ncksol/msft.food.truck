using System.Globalization;
using CsvHelper;
using FoodTruckLocator.Data.Configuration;
using FoodTruckLocator.Data.Models;
using Geolocation;

namespace FoodTruckLocator.Data.Services
{
    public class DataSFService : IDataSFService
    {
        private IEnumerable<FoodTruck> _allFoodTrucks;
        private readonly IApplicationConfiguration _configuration;

        public DataSFService(
            IDataLoaderService dataLoaderService,
            IApplicationConfiguration configuration
            )
        {
            _allFoodTrucks = dataLoaderService.LoadData();
            _configuration = configuration;
        }

        public IEnumerable<FoodTruck> GetFoodTruck(double latitude, double longitude)
        {
            var currentLocation = new Coordinate(latitude, longitude);

#pragma warning disable CS8629 // Nullable value type may be null.
            var orderByDistance = _allFoodTrucks.OrderBy(x => GeoCalculator.GetDistance(latitude, longitude, x.Latitude.Value, x.Longitude.Value, distanceUnit: DistanceUnit.Kilometers));
#pragma warning restore CS8629 // Nullable value type may be null.

            return orderByDistance.Take(_configuration.GetTrucksCount);
        }

        public double CalculateDistance(double originalLatitude, double originalLongitude, double targetLatitude, double targetLongitude)
        {
            return GeoCalculator.GetDistance(originalLatitude, originalLongitude, targetLatitude, targetLongitude);
        }
    }
}
