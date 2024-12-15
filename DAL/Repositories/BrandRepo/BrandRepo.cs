using AutoMapper;
using EShop.BLL.DTOS.ProductDTOs;
using EShop.BLL.Model.ProductModel;
using EShop.DAL.Interface.Brands;
using JwtImplementation.BLL.Model.UserModel;
using JwtImplementation.DAL.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace EShop.DAL.Repositories.BrandRepo
{
    public class BrandRepo:IBrands
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BrandRepo(ApplicationDbContext context,IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }
        public async Task<BrandDto> AddBrandasync(BrandDto brandDto)
        {
            try
            {
                var brandExists =  _context.Brands.AnyAsync(e => e.BrandName == brandDto.Name).GetAwaiter().GetResult();
                if (!brandExists)
                {
                    var newbrand = new Brand
                    {
                        BrandName = brandDto.Name,
                    };
                    await _context.Brands.AddAsync(newbrand);
                    await _context.SaveChangesAsync();
                    var dto = new BrandDto
                    {
                        Id = newbrand.BrandId,
                        Name = newbrand.BrandName,
                    };
                    return dto;
                }
                else 
                {
                    Console.WriteLine("brand already exists");
                    return new BrandDto
                    {
                        Id = 0,
                        Name = "brand already exists"
                    };
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BrandDto>> GetBrandsasync()
        {
            var brands = await _context.Brands.ToListAsync();
            var resultDtos = brands.Select(brand => new BrandDto
            {
                Id = brand.BrandId, 
                Name = brand.BrandName 
            }).ToList();
            return resultDtos;
        }
        public async Task<bool> Delete(int id)
        {
            var existtask = await _context.Brands.FirstOrDefaultAsync(b=>b.BrandId==id);
            if (existtask != null)
            {
                existtask.isDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            else 
            {
                return false;
            }

        }
    }
}
