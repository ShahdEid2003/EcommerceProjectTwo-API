using EcommerceProject2API.BBL.Services.Classes;
using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace EcommerceProject2API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var result= await _ICartService.AddToCart(request, UserId);

            if (!result) return NotFound(new { messege = _localizer["NotFound"].Value });
            return
                Ok(new
                {  message = _localizer["Success"].Value
                });


        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _ICartService.GetCart(UserId);

            return Ok(new { data = cartItems, _localizer["Success"].Value });

        }
        [HttpDelete("{ProductId}")]
        
        public async Task<IActionResult> DeleteCartItems([FromRoute]int ProductId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _ICartService.RemoveItem(ProductId,UserId);
            if (!result) return NotFound(new { messege = _localizer["NotFound"].Value });
            return Ok(new { messege = _localizer["Success"].Value }); ;


        }
        [HttpDelete("")]

        public async Task<IActionResult> ClearCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _ICartService.ClearCart(UserId);
            if (!result) return NotFound(new { messege = _localizer["NotFound"].Value });
            return Ok(new { messege = _localizer["Success"].Value }); ;


        }
        [HttpPatch("{ProductId}")]
        
        public async Task<IActionResult> UpdateProduct([FromRoute]int ProductId,[FromBody] UpdateCartRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _ICartService.UpdateQuantity(ProductId, request.Count, UserId);
            if (!result) return NotFound(new { messege = _localizer["NotFound"].Value });
            return Ok(new { messege = _localizer["Success"].Value }); ;


        }
    }
}
