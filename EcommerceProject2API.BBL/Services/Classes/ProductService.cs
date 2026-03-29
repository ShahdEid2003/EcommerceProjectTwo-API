using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Migrations;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _IProductRepository;
        private readonly IFileService _IFileService;
        public ProductService(IProductRepository IProductRepository, IFileService   IFileService)
        {
            _IProductRepository = IProductRepository;
            _IFileService = IFileService;
        }
       

        public async Task CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImg != null)
            {
                var ImagePath=await _IFileService.UploadeAsync(request.MainImg);
                product.MainImg = ImagePath;
            }
            await _IProductRepository.Create(product);
        }

        public async Task<List<ProductResponse>> GetAllProductss()
        {
            var products= await _IProductRepository.GetAll(new string[] { nameof(Product.Translations), nameof(Product.CreatedBy) });
            return products.Adapt<List<ProductResponse>>();
        }
    }
}
