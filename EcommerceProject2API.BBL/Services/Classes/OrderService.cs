using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Response;
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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
          

        }
        public async Task<List<OrderResponse>> GetUserOrders(string userId)
        {
            var orders=await _orderRepository.GetAll(
                filiter:o=>o.UserId == userId,
                includes:new []
                { nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}" ,
                   $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}" ,
                     $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}"+ $".{nameof(Product.Translations)}"
                });
            return orders.Adapt<List<OrderResponse>>();
        }
        public async Task<OrderDetailedResponse?> GetUserOrder(string userId,int orderId)
        {
            var order = await _orderRepository.GetOne(
                filiter: o => o.UserId == userId&& o.Id==orderId,
                includes: new[]
                { nameof(Order.OrderItems),
                    $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}" ,
                   $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}" ,
                     $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}"+ $".{nameof(Product.Translations)}"
                });
            if (order == null) return null;
            return order.Adapt<OrderDetailedResponse>();
        }

        public async Task<bool> CancelOrder(string userId, int orderId)
        {
            var order = await _orderRepository.GetOne(o => o.UserId == userId && o.Id == orderId);
            if (order is null) return false;
            if(order.OrderStatus!=OrderStatusEnum.Pending) return false;
            order.OrderStatus = OrderStatusEnum.Cancelled;
            return await _orderRepository.Update(order);
        }
    }
}
