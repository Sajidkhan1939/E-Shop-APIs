using JwtImplementation.BLL.Model.ProductModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.BLL.Model.ProductModel
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int ProductCategoryId {  get; set; }

        [ForeignKey(nameof(ProductCategoryId))]
        public virtual ProductCategory ProductCategory { get; set; }
        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }
        public  Color ProductColor { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        public bool IsDeleted {  get; set; } = false;

    }
}
