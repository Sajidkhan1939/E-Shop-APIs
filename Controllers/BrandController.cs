using AutoMapper;
using EShop.BLL.DTOS.ProductDTOs;
using EShop.DAL.Interface.Brands;
using EShop.DAL.Interface.ProductInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrands _brand;
        private readonly IMapper _mapper;
        public BrandController(IBrands brand, IMapper mapper)
        {
            _brand = brand;
            _mapper = mapper;
        }
        [HttpGet("Brands")]
        public async Task<IActionResult> Getbrands() 
        {
            var allbrands = await _brand.GetBrandsasync();
            return Ok(allbrands);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddBrand(BrandDto brandDto)
        {
            var result = await _brand.AddBrandasync(brandDto);
            if (result.Id == 0)
                return Conflict(new { message = "Brand already exists" });

            return CreatedAtAction(nameof(AddBrand), new { id = result.Id }, result);
        }

    }
}
