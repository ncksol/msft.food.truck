using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoodTruckLocator.Test.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> DeserializeToObject<T>(this HttpResponseMessage? httpResponseMessage) where T : class
    {
        var contentString = await httpResponseMessage.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(contentString);
    }

    public static async Task<string> TryGetPrettyPrintJson(this HttpContent? httpContent)
    {
        var rawJson = await httpContent.ReadAsStringAsync();

        return rawJson.TryGetPrettyPrintJson();
    }

    private static string TryGetPrettyPrintJson(this string? rawJson)
    {
        try
        {
            return JToken.Parse(rawJson).ToString(Formatting.Indented);
        }
        catch
        {
            return rawJson;
        }
    }
}
