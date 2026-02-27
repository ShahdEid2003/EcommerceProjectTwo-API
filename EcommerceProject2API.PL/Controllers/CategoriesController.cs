using Azure.Core;
using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using EcommerceProject2API.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EcommerceProject2API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IStringLocalizer _localizer;
        private readonly ICategoryService _ICategoryService;
        public CategoriesController(ICategoryService ICategoryService, IStringLocalizer<SharedResources> localizer)
        {
            _ICategoryService = ICategoryService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var response =await _ICategoryService.CreateCategory(request);


            return
                Ok(new
                {
                    response,
                    _message = _localizer["Success"].Value
                });


        }
        [HttpGet("")]
        public async Task<IActionResult >Index()
        {
            var response = await _ICategoryService.GetAllCategories();
             
            return Ok(new { data = response, _localizer["Success"].Value });

        }
    }
}
