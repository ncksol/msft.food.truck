using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FoodTruckLocator.Data.Models;
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
    public class GetFoodTrucksFunction
    {
        private readonly ILogger<GetFoodTrucksFunction> _logger;
        private readonly IDataSFService _dataSFService;
        private readonly IValidationService _validationService;

        public GetFoodTrucksFunction(
            ILogger<GetFoodTrucksFunction> log,
            IDataSFService dataSFService,
            IValidationService validationService
            )
        {
            _logger = log;
            _dataSFService = dataSFService;
            _validationService = validationService;
        }

        [FunctionName("GetFoodTrucksFunction")]
        [OpenApiOperation(operationId: "GetFoodTrucks", tags: new[] { "Food Trucks" }, Summary = "Get Food Trucks", Description = "This gets closest food trucks to the specified location.")]
        [OpenApiParameter(name: "latitude", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Location latitude")]
        [OpenApiParameter(name: "longitude", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Location longitude")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<FoodTruck>), Description = "List of food trucks closest to the specified location")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Summary = "Bad Request caused by data validation", Description = "Data validation issue has occured and can be recovered by the caller")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = "foodtrucks/latitude/{latitude}/longitude/{longitude}")] HttpRequest req, string latitude, string longitude)
        {
            if (double.TryParse(latitude, out var dLatitude) == false)
                return HandleBadRequest(nameof(latitude));

            if (double.TryParse(longitude, out var dLongitude) == false)
                return HandleBadRequest(nameof(longitude));

            if (_validationService.ValidateCoordinates(dLatitude, dLongitude) == false)
                return new BadRequestObjectResult($"Invalid coordinates");

            var trucks = _dataSFService.GetFoodTruck(dLatitude, dLongitude);

            return new OkObjectResult(trucks);
        }

        private IActionResult HandleBadRequest(string paramName)
        {
            return new BadRequestObjectResult($"Invalid value for {paramName}");
        }
    }
}

