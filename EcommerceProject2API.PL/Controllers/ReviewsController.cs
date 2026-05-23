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
    public class ReviewsController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IReviewService _IReviewService;
        public ReviewsController(IReviewService IReviewService, IStringLocalizer<SharedResources> localizer)
        {
            _IReviewService = IReviewService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody]AddReveiwRequest request)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _IReviewService.AddReview(UserId, request);
               

            return
                Ok(new
                {
                    response,
                    message = _localizer["Success"].Value
                });


        }
    }
}
