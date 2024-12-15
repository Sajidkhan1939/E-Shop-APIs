using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.BLL.Model.Cart
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        [NotMapped]
        public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Product.Price);
    }
}
