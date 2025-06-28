using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.API.Helper;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.HandleResponses;
using Store.Service.Helper;
using Store.Service.Services.ProductService;
using Store.Service.Services.ProductService.Dtos;

namespace Store.API.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Cache(90)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
            => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
            => Ok(await _productService.GetAllTypesAsync());
        [HttpGet]
        public async Task<ActionResult<PaginatedResultDto<ProductDetailsDto>>> GetAllProducts([FromQuery] ProductSpecification input)
            => Ok(await _productService.GetAllProductsAsync(input));
        [HttpGet]
        public async Task<ActionResult<BrandTypeDetailsDto>> GetProduct(int? id)
        {
            if (id == null)
                return BadRequest(new CustomException(400, "Id Is Null"));

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound(new CustomException(404));
            return Ok(product);
        }
    }
}
