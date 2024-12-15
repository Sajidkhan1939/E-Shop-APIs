using DAL.Interface.UserInterface;
using EShop.BLL.Mapper;
using EShop.DAL.Interface.Brands;
using EShop.DAL.Interface.CategoryInterface;
using EShop.DAL.Interface.ProductInterface;
using EShop.DAL.Repositories.BrandRepo;
using EShop.DAL.Repositories.CategoryRepo;
using EShop.DAL.Repositories.ProductRepo;
using EShop.DAL.Repositories.UserRepos;
using JwtImplementation.BLL.Service;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace JwtImplementation.Configuration
{
    public class ServiceConfigurations
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUser,UserRepository>();
            services.AddScoped<IProducts, ProductRepo>();
            services.AddScoped<IBrands, BrandRepo>();
            services.AddScoped<ICategory,CategoryRepo>();
            services.AddAutoMapper(typeof(ProductMappingProfile));
        }
    }
}
