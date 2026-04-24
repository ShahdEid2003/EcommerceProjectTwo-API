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

        public async Task<List<Product>?> DecreaseQuantityAsync(List<OrderItem> orderItems)
        {
            var productIds = orderItems.Select(i => i.ProductId).ToList();

            var products = await GetAll(p => productIds.Contains(p.Id));

            foreach (var product in products)
            {
                var item = orderItems.FirstOrDefault(p => p.ProductId == product.Id);
                product.Qauntity -= item.Quantity;
            }

            await UpdateRange(products);

            return products.Where(p => p.Qauntity < 5).ToList();
        }
    }
}
