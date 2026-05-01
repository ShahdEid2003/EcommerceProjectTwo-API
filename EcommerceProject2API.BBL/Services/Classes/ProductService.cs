using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Migrations;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var product = request.Adapt<DAL.Models.Product>();

            product.SubImages = new List<ProductImage>();

            if (request.MainImg != null)
            {
                var imagePath = await _IFileService.UploadeAsync(request.MainImg);
                product.MainImg = imagePath;
            }

            if (request.SubImages != null)
            {
                foreach (var image in request.SubImages)
                {
                    var imagePath = await _IFileService.UploadeAsync(image);

                    product.SubImages.Add(new ProductImage
                    {
                        ImagePath = imagePath
                    });
                }
            }

            await _IProductRepository.Create(product);
        }

        public async Task<List<ProductResponse>> GetAllProductss()
        {
            var products= await _IProductRepository.GetAll(p=>p.Status==EntityStatus.Active,
                new string[] { nameof(DAL.Models.Product.Translations), 
                    nameof(DAL.Models.Product.CreatedBy) , nameof(DAL.Models.Product.SubImages) });
            return products.Adapt<List<ProductResponse>>();
        }
        public async Task<ProductResponse?> GetProduct(Expression<Func<DAL.Models.Product, bool>> filiter)
        {
            var product = await _IProductRepository.GetOne(filiter, new string[] { nameof(DAL.Models.Product.Translations),nameof(DAL.Models.Product.CreatedBy) }); //{nameof(Category.Translations)}تكافىء{"translatins"}
            if (product == null) return null;
            return product.Adapt<ProductResponse>(); ;

        }
        public async Task<bool> DeleteProduct(int id)
        {

            var product = await _IProductRepository.GetOne(p => p.Id == id, new string[] { nameof(DAL.Models.Product.SubImages) });
            if (product == null) return false;
            _IFileService.Delete(product.MainImg);
            foreach (var image in product.SubImages)
            {
                _IFileService.Delete(image.ImagePath);
            }
                return await _IProductRepository.Delete(product);
        }
        public async Task<bool> UpdateProduct(int id,ProductUpdateRequest request)
        {

            var product = await _IProductRepository.GetOne(p => p.Id == id,new string[] {
                nameof(DAL.Models.Product.Translations) });
            
            if (product == null) return false;
            request.Adapt<DAL.Models.Product>();
            if (request.Translations != null)
            {
                foreach (var translationRequest in request.Translations)
                {
                    var existing = product.Translations
                        .FirstOrDefault(t => t.Language == translationRequest.Language);

                    if (existing != null)
                    {
                        if (translationRequest.Name != null)
                        {
                            existing.Name = translationRequest.Name;
                        }

                        if (translationRequest.Description != null)
                        {
                            existing.Description = translationRequest.Description;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            var oldImage = product.MainImg;
            if (request.MainImg != null)
            {
                _IFileService.Delete(product.MainImg);

                product.MainImg = await _IFileService.UploadeAsync(request.MainImg);
            }
            else
            {
                product.MainImg = oldImage;
            }
            return await _IProductRepository.Update(product);
        }

        public async Task<bool> ToggleStatus(int id)
        {
            var product=await _IProductRepository.GetOne(p=>p.Id == id);
            if (product == null) return false;
            product.Status = product.Status == EntityStatus.Active ? EntityStatus.Inactive : EntityStatus.Active;
            return await _IProductRepository.Update(product);
        }
    }
}
