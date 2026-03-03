using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface ICategoryService
    {
        public  Task <List<CategoryResponse> >GetAllCategories();
        public Task<CategoryResponse> CreateCategory(CategoryRequest request);
        public Task<CategoryResponse?> GetCategory(Expression<Func<Category, bool>> filiter);


    }
}
