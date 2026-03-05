using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EcommerceProject2API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IAuthenticationService _authService;
        public AccountController(IAuthenticationService authService, IStringLocalizer<SharedResources> localizer)
        {
         _authService=authService;
        _localizer = localizer;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result=await _authService.Register(request);
            return Ok(new { message = _localizer["Success"].Value ,result});
        }
    }
}
