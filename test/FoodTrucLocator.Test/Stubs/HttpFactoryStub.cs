using System;
using System.Net.Http;

namespace FoodTruckLocator.Test.Stubs
{
    public class HttpFactoryStub : IHttpClientFactory
    {
        public HttpMessageHandlerStub Handler { get; set; }
        public HttpClient Client { get; set; }

        public HttpFactoryStub()
        {
            Handler = new HttpMessageHandlerStub();
        }

        public HttpClient CreateClient(string name)
        {
            Client = new HttpClient(Handler) { BaseAddress = new Uri("http://invalid.domain") };
            return Client;
        }
    }
}
