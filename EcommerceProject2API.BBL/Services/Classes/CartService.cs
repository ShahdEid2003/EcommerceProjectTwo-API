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
      
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            
        }
        public async Task AddToCart(AddToCartRequest request, string UserId)
        {
            var ExistingItem = await _cartRepository.GetOne(
                    c => c.ProductId == request.ProductId && c.UserId == UserId
            );

            if (ExistingItem != null)
            {
                ExistingItem.Count += request.Count;
                await _cartRepository.Update(ExistingItem);
            }
            else
            {
                var cartItem = request.Adapt<Cart>();
                cartItem.UserId = UserId;
                await _cartRepository.Create(cartItem);
            }

        }
    }
}
