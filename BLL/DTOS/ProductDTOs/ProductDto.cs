using EShop.BLL.Model.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace EShop.BLL.DTOS.ProductDTOs
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Quantity { get; set; }
        public int BrandId { get; set; }
        public int ProductCategoryId { get; set; }
        public Color ProductColor { get; set; }
        public List<IFormFile> Images { get; set; }
    }

    public class UpdateProductDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Color ProductColor { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string ProductColor { get; set; }
        public int Quantity { get; set; }
        public List<string> Images { get; set; }
    }
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
