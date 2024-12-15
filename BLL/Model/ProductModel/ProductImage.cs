using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.BLL.Model.ProductModel
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        [Required]
        public string FileName { get; set; }
        public string IsPrimary { get; set; }

    }
}
