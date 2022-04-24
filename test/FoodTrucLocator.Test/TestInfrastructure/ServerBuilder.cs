using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodTruckLocator.Function;
using FunctionTestServer;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTruckLocator.Test.TestInfrastructure
{
    public class ServerBuilder : IDisposable
    {
        private Action<IServiceCollection> _configureServicesHook;
        private FunctionTestServer<Startup> _server;

        public ServerBuilder WithServices(Action<IServiceCollection> configureServicesHook)
        {
            _configureServicesHook = configureServicesHook;
            return this;
        }

        public FunctionTestServer<Startup> Start()
        {
            _server = new FunctionTestServer<Startup>("api", _configureServicesHook);
            _server.StartAsync().GetAwaiter().GetResult();
            return _server;
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
