using Microsoft.AspNetCore.Mvc;
using ProductFilter.Api.Models;
using ProductFilter.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductFilter.Controllers
{
    [Route("products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }       
       
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            IEnumerable<Product> products = await _productService.GetAll();

            return Ok(products);
        }

        
        [Route("{id:decimal}")]
        //[Route("{id:decimal:min(1)}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Product>>> Get([FromRoute] decimal id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Price");
            }

            IEnumerable<Product> products = await _productService.Get(id);

            return Ok(products);
        }
    }
}
