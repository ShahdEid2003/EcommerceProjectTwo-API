using EcommerceProject2API.DAL.Models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category Create(Category category);
    }
}
