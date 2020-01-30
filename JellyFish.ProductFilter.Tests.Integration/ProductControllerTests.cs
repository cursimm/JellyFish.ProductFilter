using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using ProductFilter;
using ProductFilter.Api.Models;
using ProductFilter.Api.Services;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JellyFish.ProductFilter.Tests.Integration
{
    [TestClass]
    public class ProductControllerTests
    {       
        private TestServer _server;
        private WebHostBuilder _webHostBuilder;
        private Mock<IProductService> _productService = new Mock<IProductService>();

        [TestInitialize]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            _webHostBuilder = new WebHostBuilder();
            _webHostBuilder.UseConfiguration(config);
            _webHostBuilder.UseStartup<Startup>();
            _webHostBuilder.ConfigureTestServices(services =>
            {
                services.AddTransient<IProductService>(t => _productService.Object);
            });

            var products = new List<Product>
            {
                new Product{ Id = 1, Description = "Description1", Name = "Product1", Price = 1.99M },
                new Product{ Id = 2, Description = "Description2", Name = "Product2", Price = 2.99M },
                new Product{ Id = 3, Description = "Description3", Name = "Product3", Price = 3.99M }
            };

            _productService.Setup(x => x.Get(It.IsAny<decimal>())).ReturnsAsync(products);

            _server = new TestServer(_webHostBuilder);
        }

        [TestMethod]
        public async Task Request_With_Valid_Price_Returns_200_StatusCode_And_Result()
        {
            // Arrange
            var client = _server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/products/" + 4.99M); 

            // Act
            var response = await client.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<List<Product>>(responseJson);

            // Assert 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(3, responseObject.Count);

            _productService.Verify(x => x.Get(It.IsAny<decimal>()), Times.Once);
        }

        [TestMethod]
        public async Task Request_With_Invalid_Price_Returns_400_StatusCode_And_ErrorMessage()
        {
            // Arrange           
            var client = _server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/products/" + -4.99M);           

            // Act
            var response = await client.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<string>(responseJson);

            // Assert 
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Invalid Price", responseObject);

            _productService.Verify(x => x.Get(It.IsAny<decimal>()), Times.Never);
        }

    }
}
