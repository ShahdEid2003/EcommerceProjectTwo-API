using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
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
        //Admin
        Task<List<OrderResponse>> GetAllOrders(OrderStatusEnum status);
        Task<bool> ChangeOrderStatus(int orderId, ChangeOrderStatusRequest request);

    }
}
