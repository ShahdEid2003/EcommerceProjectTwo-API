using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Repository.Classes
{
    public class ReviewRepository:GenericRepository<Review>, IReviewRepository
    {

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {


        }
    }

}
