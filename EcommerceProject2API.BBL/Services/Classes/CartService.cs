using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            
        }
        public async Task <bool> AddToCart(AddToCartRequest request, string UserId)
        {
            var product = await _productRepository.GetOne(p => p.Id == request.ProductId);
            if (product == null) { return false; }
            var ExistingItem = await _cartRepository.GetOne(
                    c => c.ProductId == request.ProductId && c.UserId == UserId
            );
            var currentCount=ExistingItem?.Count ?? 0;
            var newCount=currentCount+request.Count;
            if (newCount > product.Qauntity) { return false; }

            if (ExistingItem != null)
            {
                ExistingItem.Count = newCount;
                await _cartRepository.Update(ExistingItem);
            }
            else
            {
                var cartItem = request.Adapt<Cart>();
                cartItem.UserId = UserId;
                await _cartRepository.Create(cartItem);
            }
            return true;

        }
    }
}
