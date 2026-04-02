using EcommerceProject2API.BBL.Services.Classes;
using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EcommerceProject2API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IBrandService _IBrandService;
        public BrandsController(IBrandService IBrandService, IStringLocalizer<SharedResources> localizer)
        {
            _IBrandService = IBrandService;
            _localizer = localizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _IBrandService.GetAllBrands();

            return Ok(new { data = products, _localizer["Success"].Value });

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _IBrandService.GetBrand(b => b.Id == id);

            return Ok(new { data = brand, _localizer["Success"].Value });

        }
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] BrandRequest request)
        {

            await _IBrandService.CreateBrand(request);

            return
                Ok(new
                {
                    message = _localizer["Success"].Value
                });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _IBrandService.DeleteBrand(id);
            if (!result) return NotFound(new { messege = _localizer["NotFound"].Value });
            return Ok(new { messege = _localizer["Success"].Value }); ;


        }
    }
}
