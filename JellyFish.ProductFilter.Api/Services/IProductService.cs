using ProductFilter.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductFilter.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Get(decimal price);
        Task<IEnumerable<Product>> GetAll();
    }
}