using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace EcommerceProject2API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly ICartService _ICartService;
        public CartsController(ICartService ICartService, IStringLocalizer<SharedResources> localizer)
        {
            _ICartService = ICartService;
            _localizer = localizer;
        }
        
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           await _ICartService.AddToCart(request, UserId);


            return
                Ok(new
                {
                   
                    message = _localizer["Success"].Value
                });


        }
    }
}
