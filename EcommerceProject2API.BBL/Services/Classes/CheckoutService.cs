using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutService(ICartRepository cartRepository,IOrderRepository orderRepository,UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CheckoutResponse> ProcessCheckout(string userId, CheckoutRequest request)
        {
            var cartItems = await _cartRepository.GetAll(c => c.UserId == userId, new [] { nameof(Cart.Product), $"{nameof(Cart.Product)}.{nameof(Product.Translations)}" });
            if (!cartItems.Any())
            {
                return new CheckoutResponse { Success = false, Error = "Cart is empty" };
            }
            var user=await _userManager.FindByIdAsync(userId);
            var city = request.City ?? user.City;//if <request.city> is null then take the value from <user.city>
            if (city is null)
            {
                return new CheckoutResponse { Success = false, Error = "city is requierd" };
            }
            var street = request.Street?? user.Street;
            if (street is null)
            {
                return new CheckoutResponse { Success = false, Error = "street is requierd" };
            }
            var phoneNumber = request.PhoneNumber?? user.PhoneNumber;
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
                    SuccessUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/checkout/success",
                    CancelUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/checkout/cancel",
                    LineItems = new List<SessionLineItemOptions>()
                };
                foreach(var item in cartItems)
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
                return new CheckoutResponse { Success= true ,StripeUrl=session.Url};
            }
            return new CheckoutResponse { Success= false ,Error="Invaild Payment"};
        }
    }
}
