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
        Task<List<T>> GetAll(Expression<Func<T, bool>> filiter = null,string[]? includes = null);
        Task<T> Create(T entity);
        Task<bool> Delete(T entity);
        Task<T?> GetOne(Expression <Func <T,bool>> filiter,string[]? includes = null);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> filiter = null, string[]? includes = null);
        Task<bool> Update(T entity);
         Task<bool> DeleteRange(List<T> entites);
        Task<bool> UpdateRange(List<T> entites);
    }
}
