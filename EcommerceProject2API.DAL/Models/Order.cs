using EcommerceProject2API.DAL.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Models
{
    public enum OrderStatusEnum
    {
        Pending=1,
        Approved=2,
        Shipped=3,
        Delivered=4,
        Cancelled=5,
        Paid=6

    }
    public class Order
    {
        public int Id { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ShippedDate { get; set; }
        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Pending;
        public  string? StripeSessionId { get; set; }
        public decimal? AmountPaid{ get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderItem> OrderItems {  get; set; }

    }
}
