using EShop.BLL.Model.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace JwtImplementation.BLL.Model.ProductModel
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
