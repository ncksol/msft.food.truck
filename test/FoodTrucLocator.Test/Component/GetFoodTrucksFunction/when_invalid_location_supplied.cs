using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FoodTruckLocator.Test.Fixtures;
using FoodTruckLocator.Test.TestInfrastructure;
using Xunit;
using Xunit.Abstractions;

namespace FoodTruckLocator.Test.Component.GetFoodTrucksFunction
{
    public class when_invalid_location_supplied
    {
        private readonly ApiClient _client;

        public when_invalid_location_supplied(ITestOutputHelper logger)
        {
            var fixture = new ServerFixture();
            _client = fixture.CreateClient(logger);
        }

        [Theory]
        [InlineData("aaaa", "-122.3")]
        [InlineData("37.1", "bbbb")]
        [InlineData("300", "500")]
        public async Task should_return_400_BadRequest(string latitude, string longitude)
        {
            var response = await _client.GetFoodTrucks(latitude, longitude);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
