using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using FoodTruckLocator.Data.Configuration;
using System.Linq;
using System.Diagnostics;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace FoodTruckLocator.Function
{
    public class HealthCheckFunction
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly IApplicationConfiguration _applicationConfiguration;

        public HealthCheckFunction(
            HealthCheckService healthCheckService,
            IApplicationConfiguration applicationConfiguration
            )
        {
            _healthCheckService = healthCheckService;
            _applicationConfiguration = applicationConfiguration;
        }

        [FunctionName("HealthCheckFunction")]
        [OpenApiOperation(operationId: "HealthCheck", tags: new[] { "HealthCheck" }, Summary = "Health Check", Description = "Provides a health check of the function and dependant components.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(HealthReport), Summary = "JSON representation of function health", Description = "Provides a health check of the function and dependant components.")]
        public async Task<IActionResult> HealthCheck(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = $"{nameof(HealthCheck)}")]
            HttpRequest req)
        {
            try
            {
                var healthResult = await _healthCheckService.CheckHealthAsync();
                var processedReport = ProcessHealthReport(healthResult);

                return new OkObjectResult(processedReport);
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(ex);
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                return result;
            }
        }

        private object ProcessHealthReport(HealthReport report) => new
        {
            Status = report.Status.ToString(),
            TotalDuration = report.TotalDuration,
            Entries = report.Entries.ToDictionary(
                entry => entry.Key,
                entry => new
                {
                    Status = entry.Value.Status.ToString(),
                    Description = entry.Value.Description,
                    Duration = entry.Value.Duration,
                    Data = entry.Value.Data,
                    Exception = _applicationConfiguration.ShowHealthCheckExceptions ? entry.Value.Exception?.Demystify().ToString() : string.Empty
                }
            )
        };
    }
}
