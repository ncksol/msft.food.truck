using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckLocator.Data.Models;

public class HealthCheckReport
{
    public string Status { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public IDictionary<string, HealthCheckReportEntry> Entries { get; set; }
}

public class HealthCheckReportEntry
{
    public string Status { get; set; }
    public string? Description { get; set; }
    public TimeSpan Duration { get; set; }
    public IReadOnlyDictionary<string, object> Data { get; set; }
    public string? Exception { get; set; }
}
