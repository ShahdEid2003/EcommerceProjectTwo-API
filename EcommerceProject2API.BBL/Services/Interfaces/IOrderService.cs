using EcommerceProject2API.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetUserOrders(string userId);
        Task<OrderDetailedResponse?> GetUserOrder(string userId, int orderId);
        Task<bool> CancelOrder(string userId, int orderId);
    }
}
