namespace FoodTruckLocator.Data.Services
{
    public interface IValidationService
    {
        bool ValidateCoordinates(double latitude, double longitude);
    }
}