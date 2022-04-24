using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FoodTruckLocator.Data.Models;
using FoodTruckLocator.Test.Extensions;
using FoodTruckLocator.Test.Fixtures;
using FoodTruckLocator.Test.TestInfrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;
using Xunit.Abstractions;


namespace FoodTruckLocator.Test.Component.GetFoodTrucksFunction
{
    public class when_microsoft_office_location_supplied
    {
        private readonly ApiClient _client;

        public when_microsoft_office_location_supplied(ITestOutputHelper logger)
        {
            var fixture = new ServerFixture();
            _client = fixture.CreateClient(logger);
        }

        [Fact]
        public async Task should_return_200_OK_with_five_specific_trucks()
        {
            var response = await _client.GetFoodTrucks("37.7921574681233", "-122.40417475911627");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var foodTrucks = await response.DeserializeToObject<List<FoodTruck>>();
            foodTrucks.Count.Should().Be(5);

            foodTrucks[0].Applicant.Should().Be("Zuri Food Facilities");
            foodTrucks[1].Applicant.Should().Be("Plaza Garibaldy");
            foodTrucks[2].Applicant.Should().Be("BOWL'D ACAI, LLC.");
            foodTrucks[3].Applicant.Should().Be("Think is Good Inc.");
            foodTrucks[4].Applicant.Should().Be("Bonito Poke");
        }
    }
}
