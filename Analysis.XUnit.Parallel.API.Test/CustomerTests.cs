using Analysis.XUnit.Parallel.API.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;

namespace Analysis.XUnit.Parallel.API.Test
{
    public class CustomerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CustomerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Customers")]
        public async Task Get_ShouldReturnCorrectData(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var customers = await response.DeserializeContent<List<Customer>>().ConfigureAwait(false);
            Assert.Equal(3, customers.Count);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetById_ShouldWork(int id)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/Customers/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var customer = await response.DeserializeContent<Customer>().ConfigureAwait(false);
            Assert.NotNull(customer);
            Assert.Equal(id, customer.ID);
        }
    }
}
