using ElasticSearchWithNet.API.Dtos.ProductDtos;
using ElasticSearchWithNet.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchWithNet.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(CreateProductDto createProductDto)
        {
            return CreateActionResult(await _productService.SaveAsync(createProductDto));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _productService.GetAllProductAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return CreateActionResult(await _productService.GetByIdAsync(id));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            return CreateActionResult(await _productService.UpdateAsync(updateProductDto));
        }
    }
}
