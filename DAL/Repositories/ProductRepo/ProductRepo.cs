using AutoMapper;
using EShop.BLL.DTOS.ProductDTOs;
using EShop.BLL.Model.ProductModel;
using EShop.DAL.Interface.ProductInterface;
using JwtImplementation.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.DAL.Repositories.ProductRepo
{
    public class ProductRepo:IProducts
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepo(ApplicationDbContext context,IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }


        public async Task<ProductDto> CreateProductAsync(CreateProductDto create)
        {
            var product = _mapper.Map<Product>(create);
            if (create.Images != null && create.Images.Count > 0)
            {
                var productimages = new List<ProductImage>();
                foreach (var image in create.Images)
                {
                    if (image.Length > 0)
                    {
                        var filename = Path.GetFileName(image.FileName);
                        var filepath = Path.Combine("D:\\Dot Net Core Projects\\E-Shop\\Images", filename);
                        using (var steam = new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(steam);
                        }
                        productimages.Add(new ProductImage
                        {
                            FileName = $"/images/products/{filename}",
                            IsPrimary = productimages.Count == 0 ? "true" : "false"
                        });
                    }
                }
                product.ProductImages = productimages;
            }            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var products = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.Brand)
            .ToListAsync();
            return true;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.Brand)
            .ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto update)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;
            _mapper.Map(update, product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        
    }
}
