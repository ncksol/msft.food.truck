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

namespace FoodTruckLocator.Test.Component.HealthCheckFunction
{
    public class when_everything_is_healthy
    {
        private readonly ApiClient _client;

        public when_everything_is_healthy(ITestOutputHelper logger)
        {
            var fixture = new ServerFixture();
            _client = fixture.CreateClient(logger);
        }

        [Fact]
        public async Task should_return_200_OK_with_healthy_result()
        {
            var response = await _client.HealthCheck();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var healthReport = await response.DeserializeToObject<HealthCheckReport>();
            healthReport.Status.Should().Be(HealthStatus.Healthy.ToString());
        }
    }
}
