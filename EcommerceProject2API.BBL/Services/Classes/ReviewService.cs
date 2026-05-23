using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public  ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReview(string UserId, AddReveiwRequest request)
        {
            var purchasedOrder=await _orderRepository.GetOne(
             filiter:o=>o.UserId == UserId
                && o.OrderStatus==DAL.Models.OrderStatusEnum.Delivered&& o.OrderItems != null &&
                o.OrderItems.Any(oi=>oi.ProductId==request.ProductId),
             includes: new[]
             { 
                nameof(Order.OrderItems)
             });
            if (purchasedOrder==null) {return false;}

            var AlreadyReviews=await _reviewRepository.GetOne(
                r=>r.ProductId==request.ProductId&&
                r.UserId==UserId);

            if (AlreadyReviews != null) { return false; }

            var review=request.Adapt<Review>();
            review.UserId=UserId;
            await _reviewRepository.Create(review);
            return true;
        }
    }
}
