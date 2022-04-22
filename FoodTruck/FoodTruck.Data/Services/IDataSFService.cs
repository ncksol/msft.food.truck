using FoodTruckLocator.Data.Models;

namespace FoodTruckLocator.Data.Services
{
    public interface IDataSFService
    {
        FoodTruck GetFoodTruck(double latitude, double longitude);
    }
}