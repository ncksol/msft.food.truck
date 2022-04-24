using FoodTruckLocator.Data.Models;

namespace FoodTruckLocator.Data.Services
{
    public interface IDataSFService
    {
        double CalculateDistance(double originalLatitude, double originalLongitude, double targetLatitude, double targetLongitude);
        IEnumerable<FoodTruck> GetFoodTruck(double latitude, double longitude);
    }
}
