using EShop.BLL.DTOS.ProductDTOs;
using EShop.DAL.Interface.CategoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var productcategories = await _category.GetAllCategoriesAsync();
            return Ok(productcategories);
        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _category.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }

            var createdCategory = await _category.CreateCategoryAsync(createCategoryDto);
            return Ok(createdCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var updatedCategory = await _category.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(updatedCategory);
        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _category.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound("Category not found.");
            }

            return NoContent();
        }
    }
}
