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

        public async Task<bool> DecreaseQauntity(int productId, int amount)
        {
            var product = await GetOne(p => p.Id == productId);
            product.Qauntity-=amount;
            await Update(product);
            return product.Qauntity <5;
        }
    }
}
