using EcommerceProject2API.DAL.DTO.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Extensions
{
    public static class PagenationExtension
    {
        public static async Task<PagenationResponse<T>> ToPagenationAsync<T>(this IQueryable<T> query,int page ,int limit)
        {
            var totalcount = await query.CountAsync();
            var data = await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
            return new PagenationResponse<T>
                { Data= data,
                    TotalCount=totalcount,
                    Page=page,
                    Limit=limit
                    
                };

        }
    }
}
