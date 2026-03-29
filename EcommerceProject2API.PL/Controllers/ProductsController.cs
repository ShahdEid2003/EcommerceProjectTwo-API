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
    public class ProductsController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IProductService _IProductService;
        public ProductsController(IProductService IProductService, IStringLocalizer<SharedResources> localizer)
        {
            _IProductService = IProductService;
            _localizer = localizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _IProductService.GetAllProductss();

            return Ok(new { data = products, _localizer["Success"].Value });

        }
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm]ProductRequest request)
        {
            await _IProductService.CreateProduct(request);


            return
                Ok(new
                {
                  
                    message = _localizer["Success"].Value
                });


        }
    }
}
