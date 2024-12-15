using EShop.BLL.Model.ProductModel;
using JwtImplementation.BLL.Model.ProductModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.BLL.Model.Cart
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
