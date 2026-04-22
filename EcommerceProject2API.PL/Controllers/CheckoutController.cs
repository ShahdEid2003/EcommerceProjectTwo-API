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
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly ICheckoutService _ICheckoutService;
        public CheckoutController(ICheckoutService ICheckoutService, IStringLocalizer<SharedResources> localizer)
        {
            _ICheckoutService = ICheckoutService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody]CheckoutRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _ICheckoutService.ProcessCheckout(UserId, request);
            if (!response.Success) {
                return BadRequest(response);
            }

            return
                Ok(new
                {
                    response,
                    message = _localizer["Success"].Value
                });


        }
        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromQuery] string sessionId)
        {
            var response=await _ICheckoutService.HandleSuccess(sessionId);
            return
                Ok(new
                {
                    
                    message = _localizer["Success"].Value,
                    sessionId=sessionId
                });


        }
    }
}
