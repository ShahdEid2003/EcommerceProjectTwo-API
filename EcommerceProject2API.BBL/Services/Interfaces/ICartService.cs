using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface ICartService
    {
        public Task<bool> AddToCart(AddToCartRequest request, string UserId);
        public Task<List<CartResponse>> GetCart( string UserId);
        public Task<bool> UpdateQuantity(int productId,int count, string UserId);
        public Task<bool> RemoveItem(int productId, string UserId);
        public Task<bool> ClearCart( string UserId);

    }
}
