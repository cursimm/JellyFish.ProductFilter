using ProductFilter.Api.Data;
using ProductFilter.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductFilter.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IEnumerable<Product> _products;
        private int counter = 0;

        public ProductService(IRepository<Product> repository)
        {
            List<Product> products = repository.GetAll.ToList();
            products.ForEach(x => x.Id = counter++);
            _products = products;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            IEnumerable<Product> products = null;

            await Task.Run(() =>
            {
                products = _products.OrderByDescending(x => x.Price);
            });

            return products;
        }

        public async Task<IEnumerable<Product>> Get(decimal price)
        {
            IEnumerable<Product> products = null;

            await Task.Run(() =>
            {
                products = _products.Where(x => x.Price < price).OrderByDescending(x => x.Price);
            });

            return products.AsEnumerable();
        }
    }
}
