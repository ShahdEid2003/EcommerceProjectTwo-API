using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface ICategoryService
    {
        public  Task <List<CategoryResponse> >GetAllCategories();
        public Task<CategoryResponse> CreateCategory(CategoryRequest request);
        
    }
}
