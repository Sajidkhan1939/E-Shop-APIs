using EShop.BLL.Dtos.Cart;
using EShop.BLL.DTOS.ProductDTOs;
using EShop.BLL.Model.Cart;
using EShop.DAL.Interface.Cart;
using JwtImplementation.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace EShop.DAL.Repositories.CartRepo
{
    public class CartRepo : ICart
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CartDto> AddtoCart(AddToCartDto addToCartDto)
        {
            var cart = await _context.Carts.Include(c=>c.Items)
                .ThenInclude(ci=>ci.Product)
                .FirstOrDefaultAsync(c=>c.CartId == addToCartDto.CartId);
            if (cart == null) 
            {
                cart = new Cart { CartId = addToCartDto.CartId };
                _context.Carts.Add(cart);
            }
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == addToCartDto.ProductId);
            if (existingItem != null) 
            {
                // Update quantity if item already exists
                existingItem.Quantity += addToCartDto.Quantity;
            }
            else
            {
                var product = _context.Products.FindAsync(addToCartDto.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                var newItem = new CartItem
                {
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    Product = new ProductDto // Create a new ProductDto
                    {
                        ProductId = product.Result,
                        ProductName = product.ProductName,
                        Price = product.Price
                        // Add other properties as needed
                    }
                };
                cart.Items.Add(newItem);
            }
            await _context.SaveChangesAsync();

            return new CartDto
            {
                CartId = cart.CartId,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductName = i.Product.ProductName,
                    Price = i.Product.Price
                }).ToList(),
                TotalPrice = cart.TotalPrice
            };
        }

        public Task ClearCartAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> GetCartAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> RemoveFromCartAsync(int cartId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> UpdateCartItemAsync(int cartId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
