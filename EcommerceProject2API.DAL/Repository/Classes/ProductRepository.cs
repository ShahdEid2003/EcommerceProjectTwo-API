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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {


        }
    }
}
