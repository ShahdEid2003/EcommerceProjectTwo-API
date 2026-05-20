using EcommerceProject2API.BBL.Services.Classes;
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
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class UserManagmentController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IUserMangmentService _IUserManagmentService;
        public UserManagmentController(IUserMangmentService IUserManagmentService, IStringLocalizer<SharedResources> localizer)
        {
            _IUserManagmentService = IUserManagmentService;
            _localizer = localizer;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            
            var users = await _IUserManagmentService.GetAllUsers();

            return Ok(new { data = users, _localizer["Success"].Value });

        }
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute]string userId)
        {

            var users = await _IUserManagmentService.GetUser(userId);

            return Ok(new { data = users, _localizer["Success"].Value });

        }
        [HttpPatch("{userId}/role")]
        public async Task<IActionResult> ChangeRole([FromRoute] string userId,[FromBody] ChangeRoleRequest request)
        {

            var result = await _IUserManagmentService.ChangeRole(userId,request.newRole);
            if(!result)
                return BadRequest();
            return Ok(new {  _localizer["Success"].Value });

        }
    }
}
