using EShop.BLL.Dtos.Cart;

namespace EShop.DAL.Interface.Cart
{
    public interface ICart
    {
        Task<CartDto> AddtoCart(AddToCartDto addToCartDto);
        Task<CartDto> GetCartAsync(int cartId);
        Task<CartDto> UpdateCartItemAsync(int cartId, int productId, int quantity);
        Task<CartDto> RemoveFromCartAsync(int cartId, int productId);
        Task ClearCartAsync(int cartId);
    }
}
