using System.IO;
using System.Net;
using System.Threading.Tasks;
using FoodTruckLocator.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace FoodTruckLocator.Function
{
    public class GetFoodTrucks
    {
        private readonly ILogger<GetFoodTrucks> _logger;
        private readonly IDataSFService _dataSFService;

        public GetFoodTrucks(ILogger<GetFoodTrucks> log, IDataSFService dataSFService)
        {
            _logger = log;
            _dataSFService = dataSFService;
        }

        [FunctionName("GetFoodTrucks")]
        [OpenApiOperation(operationId: "GetFoodTrucks", tags: new[] { "Food Trucks" }, Summary = "Get Food Trucks", Description = "This gets closest food trucks to the specified location.")]
        [OpenApiParameter(name: "latitude", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Location latitude")]
        [OpenApiParameter(name: "longitude", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Location longitude")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = "foodtrucks/latitude/{latitude}/longitude/{longitude}")] HttpRequest req, string latitude, string longitude)
        {
            double.TryParse(latitude, out var dLatitude);
            double.TryParse(longitude, out var dLongitude);


            var truck = _dataSFService.GetFoodTruck(dLatitude, dLongitude);

            var responseMessage = $"Truck: {truck.Address}";

            return new OkObjectResult(responseMessage);
        }
    }
}

