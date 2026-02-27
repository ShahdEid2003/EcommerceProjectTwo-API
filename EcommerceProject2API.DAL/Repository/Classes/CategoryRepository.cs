using Azure.Core;
using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Models;
using EcommerceProject2API.DAL.Repository.Interfaces;


namespace EcommerceProject2API.DAL.Repository.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {


        }
    }

}