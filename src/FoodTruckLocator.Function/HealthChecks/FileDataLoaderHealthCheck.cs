using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodTruckLocator.Data.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FoodTruckLocator.Function.HealthChecks
{
    public class FileDataLoaderHealthCheck : IHealthCheck
    {
        private readonly IDataLoaderService _fileDataLoaderService;

        public FileDataLoaderHealthCheck(
            IDataLoaderService fileDataLoaderService
            )
        {
            _fileDataLoaderService = fileDataLoaderService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var healthCheck = _fileDataLoaderService.HealthCheck();

                return healthCheck ?
                    Task.FromResult(HealthCheckResult.Healthy()) :
                    Task.FromResult(HealthCheckResult.Unhealthy());
            }
            catch (Exception ex)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, exception: ex));
            }
        }
    }
}
