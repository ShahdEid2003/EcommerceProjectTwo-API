using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Migrations;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.Checkout;
using IEmailSender = EcommerceProject2API.BBL.Services.Interfaces.IEmailSender;


namespace EcommerceProject2API.BBL.Services.Classes
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartService _cartService;
        private readonly IEmailSender _emailSender;



        public CheckoutService(ICartRepository cartRepository, IEmailSender emailSender, IProductRepository productRepository, ICartService cartService, IOrderRepository orderRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _cartService = cartService;
            _emailSender = emailSender;
        }



        public async Task<CheckoutResponse> ProcessCheckout(string userId, CheckoutRequest request)
        {
            var cartItems = await _cartRepository.GetAll(c => c.UserId == userId, new[] { nameof(Cart.Product), $"{nameof(Cart.Product)}.{nameof(Product.Translations)}" });
            if (!cartItems.Any())
            {
                return new CheckoutResponse { Success = false, Error = "Cart is empty" };
            }
            var user = await _userManager.FindByIdAsync(userId);
            var city = request.City ?? user.City;//if <request.city> is null then take the value from <user.city>
            if (city is null)
            {
                return new CheckoutResponse { Success = false, Error = "city is requierd" };
            }
            var street = request.Street ?? user.Street;
            if (street is null)
            {
                return new CheckoutResponse { Success = false, Error = "street is requierd" };
            }
            var phoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            if (phoneNumber is null)
            {
                return new CheckoutResponse { Success = false, Error = "phoneNumber is requierd" };
            }
            foreach (var item in cartItems)
            {
                if (item.Count > item.Product.Qauntity)
                {
                    return new CheckoutResponse { Success = false, Error = "Doesn’t have enough stock" };
                }
            }
            var order = new Order()
            {
                UserId = userId,
                City = city,
                Street = street,
                PhoneNumber = phoneNumber,
                PaymentMethod = request.PaymentMethod,
                AmountPaid = cartItems.Sum(c => c.Product.Price * c.Count),
                OrderItems = cartItems.Select(c => new OrderItem()
                {
                    ProductId = c.ProductId,
                    UnitPrice = c.Product.Price,
                    TotalPrice = c.Product.Price * c.Count,
                    Quantity = c.Count
                }).ToList()
            };
            await _orderRepository.Create(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckoutResponse { Success = true };
            }
            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",
                    SuccessUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Checkout/success?sessionId={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/checkout/cancel",
                    LineItems = new List<SessionLineItemOptions>()
                };
                foreach (var item in cartItems)
                {
                    options.LineItems.Add(
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "USD",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Translations.FirstOrDefault(t => t.Language == "en").Name,
                                },
                                UnitAmount = (long)(item.Product.Price * 100),
                            },
                            Quantity = item.Count,
                        }
                    );
                }
                var service = new SessionService();
                var session = service.Create(options);
                order.StripeSessionId = session.Id;
                await _orderRepository.Update(order);
                return new CheckoutResponse { Success = true, StripeUrl = session.Url };
            }
            return new CheckoutResponse { Success = false, Error = "Invaild Payment" };
        }


        public async Task<CheckoutResponse> HandleSuccess(string sessionId)
        {
            var order = await _orderRepository.GetOne
                (
                    filiter: o => o.StripeSessionId == sessionId
                    , includes: new[]
                    {   nameof(Order.OrderItems)  ,
                        $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}",
                        $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Product)}.{nameof(Product.Translations)}"
                    }
                );
            order.OrderStatus = OrderStatusEnum.Paid;
            await _orderRepository.Update(order);
            await _cartService.ClearCart(order.UserId);

            var user = await _userManager.FindByIdAsync(order.UserId);
            await _emailSender.SendEmailAsync(user.Email, "order confirmed", "<h1>Your Order has been placed successfully</h1>");
            var LowStockProducts = await _productRepository.DecreaseQuantityAsync(order.OrderItems);
            foreach (var item in LowStockProducts)
            {
                if (LowStockProducts != null)
                {
                    await _emailSender.SendEmailAsync("shahdeid012@gmail.com", "low stock alert",
                        $"<h2>product{item.Translations.FirstOrDefault(t => t.Language == "en").Name} current quantity:{item.Qauntity}</h2>");
                }
            }

            return new CheckoutResponse()
            {
                Success = true,
                OrderId = order.Id
            };

        }
    }
}
