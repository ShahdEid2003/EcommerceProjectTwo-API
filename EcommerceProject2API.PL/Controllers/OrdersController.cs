using EcommerceProject2API.BBL.Services.Classes;
using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.Models;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _IOrderService.GetUserOrder(UserId,id);

            return Ok(new { data = order, _localizer["Success"].Value });


        }
        [HttpGet("admin")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllOrder([FromQuery]OrderStatusEnum status=OrderStatusEnum.Pending)
        {
            
            var orders = await _IOrderService.GetAllOrders(status);

            return Ok(new { data = orders, _localizer["Success"].Value });


        }
        [HttpPatch("admin/{id}/status")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult>ChangeStatus(int id,[FromBody] ChangeOrderStatusRequest request)
        {

            var result = await _IOrderService.ChangeOrderStatus(id,request);

            if (!result)
                return BadRequest();

            return Ok(new {  _localizer["Success"].Value });


        }
    }
}
