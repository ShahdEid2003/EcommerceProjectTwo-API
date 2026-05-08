using EcommerceProject2API.DAL.Data;
using EcommerceProject2API.DAL.Repository.Classes;
using EcommerceProject2API.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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
        public async Task<bool> DeleteRange(List<T> entites)
        {
            _context.RemoveRange(entites);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;

        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filiter=null, string[]? includes = null)
        {
            
            // هاي عشان انكلود مرات بتكون نل واحنا بدنا نعمل اشي عام يزبط لكل الريبو زيتوري
            // Iquerable الفكرة اعمل انكلون على مستوى السيرفر احسن عشان هيك نوعها
            //بعدها بفحص اذا كان مش نل يطبق شرط الانكلون ويرجع الداتا بناء عالشرط وبنفس الوقت اذا كانت نل برجع الداتا بدون الشرط 
            IQueryable<T> query = _context.Set<T>();
            if (filiter!= null)
            {
               query = query.Where(filiter);
            }
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
        public  IQueryable<T> GetQueryable(Expression<Func<T, bool>> filiter = null, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filiter != null)
            {
                query = query.Where(filiter);
            }
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return  query;

           
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
        public async Task<bool> Update(T entity)
        {
            _context.Update(entity);
            var affected =await _context.SaveChangesAsync();    
            return affected > 0;
        }

        public async Task<bool> UpdateRange(List<T> entites)
        {
            _context.UpdateRange(entites);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }
      
    }
}



//static void Main(string[] args)
//{
//    List<int> items = new List<int> { 4, 7, 8 };

//    // LINQ query is not executed immediately (Deferred Execution)
//    var result = items.Where(item => item % 2 == 0);

//    // Add new even number before foreach
//    items.Add(10);

//    foreach (var item in result)
//    {
//        Console.WriteLine(item);
//    } //result:4,8,10 not 4,8 
//}

//وهذا فعليًا نفس الفكرة اللي بيشتغل فيها IQueryable.

//التشابه:
//الاثنين ما بينفذوا الاستعلام مباشرة.
//الاثنين بيخزنوا “وصف للاستعلام” مش النتيجة.
//التنفيذ بيصير لما تطلب البيانات (مثل foreach أو ToList()).