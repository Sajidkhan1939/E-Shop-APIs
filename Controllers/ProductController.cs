using AutoMapper;
using EShop.BLL.DTOS.ProductDTOs;
using EShop.DAL.Interface.ProductInterface;
using EShop.DAL.Repositories.ProductRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _products;
        private readonly IMapper _mapper;
        public ProductController(IProducts products,IMapper mapper)
        {
             _products = products;
            _mapper = mapper;
        }
        [Authorize(Roles = "User, Admin")]
        [HttpGet("AllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _products.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("getProduct")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _products.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpPost("addNew")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var createdProduct = await _products.CreateProductAsync(createProductDto);
            if (createdProduct == null) return NotFound();
            return Ok(createdProduct);
            
        }
        [HttpPut("changeproduct")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            var updatedProduct = await _products.UpdateProductAsync(id, updateProductDto);
            if (updatedProduct == null)
            {
                return NotFound(); 
            }
            return Ok(updatedProduct);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _products.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound(); 
            }
            return Ok("successfully deleted product"); 
        }
    }
}
