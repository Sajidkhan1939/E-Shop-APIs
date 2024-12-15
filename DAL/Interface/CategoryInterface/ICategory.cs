using EShop.BLL.DTOS.ProductDTOs;

namespace EShop.DAL.Interface.CategoryInterface
{
    public interface ICategory
    {
        // Retrieves a list of all categories
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

        // Retrieves a single category by its ID
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);

        // Creates a new category
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);

        // Updates an existing category
        Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto);

        // Deletes a category by its ID
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
