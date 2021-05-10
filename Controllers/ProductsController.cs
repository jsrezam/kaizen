using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/products")]
    public class ProductsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<ProductResource, Product>(productResource);
            await productService.CreateProductAsync(product);
            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            mapper.Map<ProductResource, Product>(productResource, product);
            await productService.UpdateProductAsync(product);

            var result = mapper.Map<Product, ProductResource>(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(ProductQueryResource productQueryResource)
        {
            var productQuery = mapper.Map<ProductQueryResource, ProductQuery>(productQueryResource);
            var queryResult = await productService.GetProductsAsync(productQuery);
            var resultQuery = mapper.Map<QueryResult<Product>, QueryResultResource<ProductResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("validated")]
        public async Task<IActionResult> GetValidProducts(ProductQueryResource productQueryResource)
        {
            var productQuery = mapper.Map<ProductQueryResource, ProductQuery>(productQueryResource);
            var queryResult = await productService.GetValidProducts(productQuery);
            var resultQuery = mapper.Map<QueryResult<Product>, QueryResultResource<ProductResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
                return NotFound();

            var productResource = mapper.Map<Product, ProductResource>(product);
            return Ok(productResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            var result = productService.DeleteProductAsync(product);
            return Ok(result);
        }
    }
}