using EShop.BLL.DTOS.ProductDTOs;
using EShop.DAL.Interface.CategoryInterface;
using JwtImplementation.BLL.Model.ProductModel;
using JwtImplementation.DAL.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EShop.DAL.Repositories.CategoryRepo
{
    public class CategoryRepo : ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
           var categoryexist = await _context.ProductCategories.AnyAsync(c=>c.CategoryName==createCategoryDto.Name);
            if (!categoryexist) 
            {
                var newcategory = new ProductCategory
                {
                    CategoryName = createCategoryDto.Name,
                    CategoryDescription = createCategoryDto.Description,
                };
                await _context.ProductCategories.AddAsync(newcategory);
                await _context.SaveChangesAsync();
                var categorydto = new CategoryDto
                {
                    Id = newcategory.ProductCategoryId,
                    Name = newcategory.CategoryName,
                    Description = newcategory.CategoryDescription
                };
                return categorydto;
            }
            else
            {
                var failedresponse = new CategoryDto
                {
                    Id = 0,
                    Name = "category exists with this name"
                };
                return failedresponse;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var existcategory = await _context.ProductCategories
                .FirstOrDefaultAsync(c=>c.ProductCategoryId==categoryId); 
            if (existcategory == null)
            {
                return false; 
            }
            existcategory.IsDelete = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var allcategories = await _context.ProductCategories.ToListAsync();
            var catergorydto = allcategories.Select(c => new CategoryDto
            {
                Id = c.ProductCategoryId,
                Name = c.CategoryName,
                Description = c.CategoryDescription
            });
            return catergorydto;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.ProductCategoryId == categoryId);
            if (category == null)
            {
                return new CategoryDto
                {
                    Id = 0,
                    Name = "does not found any category with this provided Id"
                };
            }
            else
            {
                var categorydto = new CategoryDto
                {
                    Id = category.ProductCategoryId,
                    Name = category.CategoryName,
                    Description = category.CategoryDescription
                };
                return categorydto;
            }
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto)
        {
            var existcategory = await _context.ProductCategories
                .FirstOrDefaultAsync(c=>c.ProductCategoryId == categoryId);
            if (existcategory == null)
            {
                return new CategoryDto
                {
                    Id = 0,
                    Name = "provide valid Id"
                };
            }
            existcategory.CategoryName = categoryDto.Name;
            existcategory.CategoryDescription = categoryDto.Description;
            await _context.SaveChangesAsync();
            return new CategoryDto
            {
                Id = existcategory.ProductCategoryId,
                Name = existcategory.CategoryName,
                Description = existcategory.CategoryDescription
            };
        }
    }
}
