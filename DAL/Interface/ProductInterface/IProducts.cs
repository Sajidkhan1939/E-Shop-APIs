using EShop.BLL.DTOS.ProductDTOs;
using EShop.BLL.Model.ProductModel;
using JwtImplementation.BLL.Model.UserModel;

namespace EShop.DAL.Interface.ProductInterface
{
    public interface IProducts
    {
        /// <summary>
        /// Creates a new product.
        /// </summary>
        Task<ProductDto> CreateProductAsync(CreateProductDto create);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto update);

        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        Task<List<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        Task<ProductDto> GetProductByIdAsync(int id);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        Task<bool> DeleteProductAsync(int id);
        
    }
}
