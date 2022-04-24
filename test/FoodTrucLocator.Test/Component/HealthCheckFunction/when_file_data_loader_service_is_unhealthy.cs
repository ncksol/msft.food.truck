using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FoodTruckLocator.Data.Models;
using FoodTruckLocator.Data.Services;
using FoodTruckLocator.Test.Extensions;
using FoodTruckLocator.Test.Stubs;
using FoodTruckLocator.Test.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;
using Xunit.Abstractions;

namespace FoodTruckLocator.Test.Component.HealthCheckFunction
{
    public class when_file_data_loader_service_is_unhealthy
    {
        private readonly ApiClient _client;

        public when_file_data_loader_service_is_unhealthy(ITestOutputHelper logger)
        {
            var server = new ServerBuilder()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IDataLoaderService>(_ => new UnhealthyFileDataLoaderServiceStub());
                })
                .Start();

            _client = new ApiClient(logger, server.Client);
        }


        [Fact]
        public async Task should_return_200_OK_with_unhealthy_result()
        {
            var response = await _client.HealthCheck();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var healthReport = await response.DeserializeToObject<HealthCheckReport>();
            healthReport.Status.Should().Be(HealthStatus.Unhealthy.ToString());
        }
    }
}
