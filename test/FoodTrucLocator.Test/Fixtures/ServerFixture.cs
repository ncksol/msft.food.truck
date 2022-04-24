using System.Net.Http;
using FoodTruckLocator.Data.Configuration;
using FoodTruckLocator.Data.Services;
using FoodTruckLocator.Function;
using FoodTruckLocator.Test.Stubs;
using FoodTruckLocator.Test.TestInfrastructure;
using FunctionTestServer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit.Abstractions;

namespace FoodTruckLocator.Test.Fixtures
{
    public class ServerFixture
    {
        private readonly FunctionTestServer<Startup> _server;

        public ServerFixture()
        {
            HttpFactoryStub = new HttpFactoryStub();

            _server = new ServerBuilder()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IApplicationConfiguration>(_ => new ApplicationConfigurationStub());
                    services.TryAddScoped<IHttpClientFactory>(_ => HttpFactoryStub);
                    services.TryAddSingleton<IDataLoaderService>(_ => new FileDataLoaderServiceStub());
                })
                .Start();
        }

        public ApiClient CreateClient(ITestOutputHelper logger)
        {
            return new ApiClient(logger, _server.Client);
        }

        public HttpFactoryStub HttpFactoryStub { get; }
    }
}
