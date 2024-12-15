using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EShop.BLL.Model.ProductModel
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool isDelete { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
