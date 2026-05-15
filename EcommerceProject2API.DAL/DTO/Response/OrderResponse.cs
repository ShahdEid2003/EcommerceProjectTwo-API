using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public decimal AmountPaid { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string? StripeSessionId { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }
}
