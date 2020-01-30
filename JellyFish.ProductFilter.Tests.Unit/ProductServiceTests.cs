using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductFilter.Api.Data;
using ProductFilter.Api.Models;
using ProductFilter.Api.Services;
using System.Linq;
using System.Collections.Generic;

namespace ProductFilter.Tests.Unit
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly Mock<IRepository<Product>> _repository = new Mock<IRepository<Product>>();

        [TestMethod]
        public void GetAll_Returns_All_Products_In_Correct_Order()
        {
            var products = new List<Product>
            {
                new Product{ Id = 1, Description = "Description1", Name = "Product1", Price = 1.99M },
                new Product{ Id = 2, Description = "Description2", Name = "Product2", Price = 2.99M },
                new Product{ Id = 3, Description = "Description3", Name = "Product3", Price = 3.99M }
            };

            _repository.Setup(x => x.GetAll).Returns(products.AsQueryable());

            var sut = new ProductService(_repository.Object);

            var result = sut.GetAll().Result.ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(3.99M, result[0].Price);
        }

        [TestMethod]
        public void Get_Returns_Filtered_Products_In_Correct_Order()
        {
            var products = new List<Product>
            {
                new Product{ Id = 1, Description = "Description1", Name = "Product1", Price = 1.99M },
                new Product{ Id = 2, Description = "Description2", Name = "Product2", Price = 2.99M },
                new Product{ Id = 3, Description = "Description3", Name = "Product3", Price = 3.99M }
            };

            _repository.Setup(x => x.GetAll).Returns(products.AsQueryable());

            var sut = new ProductService(_repository.Object);

            var result = sut.Get(2.00M).Result.ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1.99M, result[0].Price);
        }
    }
}
