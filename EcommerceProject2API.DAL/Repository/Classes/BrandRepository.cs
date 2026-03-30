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
    public class BrandRepository: GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {


        }
    }
}
