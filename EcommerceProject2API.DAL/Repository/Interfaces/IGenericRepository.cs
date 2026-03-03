using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T> where T :class
    {
        Task<List<T>> GetAll(string[]? includes = null);
        Task<T> Create(T entity);
        Task<T> GetOne(Expression <Func <T,bool>> filiter,string[]? includes = null);
    }
}
