using EShop.BLL.DTOS.ProductDTOs;

namespace EShop.DAL.Interface.Brands
{
    public interface IBrands
    {
        /// <summary>
        /// Add Brand.
        /// </summary>
        Task<BrandDto> AddBrandasync(BrandDto brandDto);
        /// <summary>
        /// Get Brands.
        /// </summary>
        Task<List<BrandDto>> GetBrandsasync();
    }
}
