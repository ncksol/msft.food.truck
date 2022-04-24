using FoodTruckLocator.Data.Models;

namespace FoodTruckLocator.Data.Services
{
    public interface IDataLoaderService
    {
        bool HealthCheck();
        IEnumerable<FoodTruck> LoadData();
    }
}