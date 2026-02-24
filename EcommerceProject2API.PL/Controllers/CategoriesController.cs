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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStringLocalizer _localizer;
        public CategoriesController(ICategoryRepository categoryRepository, IStringLocalizer<SharedResources> localizer)
        {
            _categoryRepository = categoryRepository;
            _localizer = localizer;
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _categoryRepository.Create(category);


            return 
                Ok(new
            {
                _message = _localizer["Success"].Value
            });


        }
        [HttpGet("")]
        public IActionResult Get() {
            var categories = _categoryRepository.GetAll();
            var response = categories.Adapt<List<CategoryResponse>>();

            return Ok(new { data=response,_localizer["Success"].Value });

        }
    }
}
