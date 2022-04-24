using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoodTruckLocator.Test.Stubs
{
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        public HttpRequestMessage Request { get; set; }
        public Dictionary<string, HttpResponseMessage> Responses { get; set; }
        public Dictionary<string, int> Calls { get; set; }

        public HttpMessageHandlerStub()
        {
            Calls = new Dictionary<string, int>();
            Responses = new Dictionary<string, HttpResponseMessage>();
        }

        public bool WasEndpointCalled(string endpoint) => Calls[endpoint] > 0;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            var url = Request.RequestUri.LocalPath;
            var response = Responses[url];
            Calls[url] = Calls.TryGetValue(url, out var count) ? ++count : 1;
            return Task.FromResult(response);
        }

        public void AddResponse<T>(string url, T responseBody)
        {
            var httpResponse = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseBody), Encoding.UTF8, "application/json")
            };
            Responses.Add(url, httpResponse);
        }
    }
}
