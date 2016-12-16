using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace myRetail.Tests
{
    public class ProductsRouteShould
    {
        private readonly HttpClient _client;

        public ProductsRouteShould()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        // [Fact]
        // public async Task ReturnCollection()
        // {
        //     var response = await _client.GetAsync("/products");
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);

        //     dynamic data = JObject.Parse(await response.Content.ReadAsStringAsync());
        //     Assert.True(data.items.Count > 0);
        // }

        [Fact]
        public async Task ReturnNotFoundForUnknownId()
        {
            var response = await _client.GetAsync("/products/100abc");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
