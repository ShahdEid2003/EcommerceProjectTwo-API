using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _ICategoryRepository;
        public CategoryService(ICategoryRepository ICategoryRepository)
        {
            _ICategoryRepository = ICategoryRepository;
        }
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            await _ICategoryRepository.Create(category);
            return category.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>>GetAllCategories()
        {
            var categories=await _ICategoryRepository.GetAll();
            return categories.Adapt<List<CategoryResponse>>();  

        }
    }
}
