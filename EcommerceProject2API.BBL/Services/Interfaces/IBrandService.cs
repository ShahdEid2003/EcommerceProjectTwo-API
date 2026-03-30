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
    public interface IBrandService
    {
        public Task CreateBrand(BrandRequest request);
        public Task<List<BrandResponse>> GetAllBrands();
        public Task<bool> DeleteBrand(int id);
        public Task<BrandResponse?> GetBrand(Expression<Func<Brand, bool>> filiter);

    }
}
