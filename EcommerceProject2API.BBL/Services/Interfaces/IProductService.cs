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
    public interface IProductService
    {
        public Task CreateProduct(ProductRequest request);
        public Task<PagenationResponse<ProductResponse>> GetAllProductss(ProductFiliterRequest request);
        public Task<ProductResponse?> GetProduct(Expression<Func<Product, bool>> filiter);
        public  Task<bool> DeleteProduct(int id);
        public  Task<bool> UpdateProduct(int id, ProductUpdateRequest request);
        public Task<bool> ToggleStatus(int id);
    }
}
