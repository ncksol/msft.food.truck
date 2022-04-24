using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace FoodTruckLocator.Test.TestInfrastructure;

public class ApiClient : IDisposable
{
    private readonly HttpClientWrapper _httpClient;

    public ApiClient(ITestOutputHelper logger, HttpClient httpClient)
    {
        _httpClient = new HttpClientWrapper(logger, httpClient);
    }

    public async Task<HttpResponseMessage> HealthCheck()
    {
        var requestUri = "api/HealthCheck";

        return await _httpClient.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> GetFoodTrucks(string latitude, string longitude)
    {
        var requestUri = $"api/foodtrucks/latitude/{latitude}/longitude/{longitude}";

        return await _httpClient.GetAsync(requestUri);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
