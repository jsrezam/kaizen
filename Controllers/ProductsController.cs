using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
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
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<ProductDto, Product>(productDto);
            await productService.CreateProductAsync(product);
            var result = mapper.Map<Product, ProductDto>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            mapper.Map<ProductDto, Product>(productDto, product);
            await productService.UpdateProductAsync(product);

            var result = mapper.Map<Product, ProductDto>(product);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(ProductQueryDto productQueryDto)
        {
            var productQuery = mapper.Map<ProductQueryDto, ProductQuery>(productQueryDto);
            var queryResult = await productService.GetProductsAsync(productQuery);
            var resultQuery = mapper.Map<QueryResult<Product>, QueryResultDto<ProductDto>>(queryResult);

            return Ok(resultQuery);
        }

        [HttpGet("validated")]
        public async Task<IActionResult> GetValidProducts(ProductQueryDto productQueryDto)
        {
            var productQuery = mapper.Map<ProductQueryDto, ProductQuery>(productQueryDto);
            var queryResult = await productService.GetValidProducts(productQuery);
            var resultQuery = mapper.Map<QueryResult<Product>, QueryResultDto<ProductDto>>(queryResult);

            return Ok(resultQuery);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
                return NotFound();

            var productDto = mapper.Map<Product, ProductDto>(product);
            return Ok(productDto);
        }
    }
}