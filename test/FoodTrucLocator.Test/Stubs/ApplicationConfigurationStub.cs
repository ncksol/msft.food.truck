using FoodTruckLocator.Data.Configuration;

namespace FoodTruckLocator.Test.Fixtures
{
    public class ApplicationConfigurationStub : IApplicationConfiguration
    {
        public int GetTrucksCount { get => 5; }

        public bool ShowHealthCheckExceptions { get => true; }
    }
}
