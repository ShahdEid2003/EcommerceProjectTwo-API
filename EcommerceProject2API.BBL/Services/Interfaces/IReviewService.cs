using EcommerceProject2API.DAL.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReview(string UserId, AddReveiwRequest request);
    }
}
