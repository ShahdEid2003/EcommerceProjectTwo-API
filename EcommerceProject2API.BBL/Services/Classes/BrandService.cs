using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _IBrandRepository;
        private readonly IFileService _IFileService;
        public BrandService(IBrandRepository IBrandRepository, IFileService IFileService)
        {
            _IBrandRepository = IBrandRepository;
            _IFileService = IFileService;
        }
        public async Task CreateBrand(BrandRequest request)
        {
            var brand = request.Adapt<Brand>();
           
            if (request.LogoImg != null)
            {
                var ImagePath = await _IFileService.UploadeAsync(request.LogoImg);
                brand.LogoImg= ImagePath;
            }

            await _IBrandRepository.Create(brand);
        }
       
        public async Task<List<BrandResponse>> GetAllBrands()
        {
            var brands = await _IBrandRepository.GetAll(new string[] { nameof(Brand.Translations), nameof(Brand.CreatedBy) });
            return brands.Adapt<List<BrandResponse>>();
        }
        public async Task<bool> DeleteBrand(int id)
        {

            var brand= await _IBrandRepository.GetOne(p => p.Id == id);
            if (brand == null) return false;
            _IFileService.Delete(brand.LogoImg);
            return await _IBrandRepository.Delete(brand);
        }

        public async Task<BrandResponse?> GetBrand(Expression<Func<Brand, bool>> filiter)
        {
            var brand = await _IBrandRepository.GetOne(filiter, new string[] { nameof(Brand.Translations), nameof(Brand.CreatedBy) }); 
            if (brand == null) return null;
            return brand.Adapt<BrandResponse>(); ;
        }
    }
}
