using Core.Entities;
using Demo.Helper;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;
using Services.HandleResponse;
using Services.Helper;
using Services.Services.ProductServices;
using Services.Services.ProductServices.Dto;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Cache(10)]
        public async Task<ActionResult<Pagination<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecifications specs)
        {
            var products = await _productService.GetProductsAsync(specs);

            return Ok(products);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrandsProducts()
            => Ok(await _productService.GetProductsBrandAsync());
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypeProducts()
            => Ok(await _productService.GetProductsTypesAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [Cache(100)]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int? id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if(product is null)
                return NotFound(new ApiResponse(404));

            return Ok(product);

        } 

    }
}
