using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Repository.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<T> Create(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(T entity)
        {
             _context.Remove(entity);
            var affected =await _context.SaveChangesAsync();
            return affected > 0;

        }

        public async Task<List<T>> GetAll(string[]? includes = null)
        {
            
            // هاي عشان انكلود مرات بتكون نل واحنا بدنا نعمل اشي عام يزبط لكل الريبو زيتوري
            // Iquerable الفكرة اعمل انكلون على مستوى السيرفر احسن عشان هيك نوعها
            //بعدها بفحص اذا كان مش نل يطبق شرط الانكلون ويرجع الداتا بناء عالشرط وبنفس الوقت اذا كانت نل برجع الداتا بدون الشرط 
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();

            // return await _context.Set<T>().Include(c => c.Translations).ToListAsync() بدل ما كانت 
        }

        public async Task<T?> GetOne(Expression<Func<T, bool>> filiter, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(filiter);
        }
    }
}
