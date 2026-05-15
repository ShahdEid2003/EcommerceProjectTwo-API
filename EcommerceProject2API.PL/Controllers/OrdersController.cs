using EcommerceProject2API.BBL.Services.Classes;
using EcommerceProject2API.BBL.Services.Interfaces;
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
    public class OrdersController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IOrderService _IOrderService;
        public OrdersController(IOrderService IOrderService, IStringLocalizer<SharedResources> localizer)
        {
            _IOrderService = IOrderService;
            _localizer = localizer;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _IOrderService.GetUserOrders(UserId);

            return Ok(new { data = orders, _localizer["Success"].Value });


        }
    }
}
