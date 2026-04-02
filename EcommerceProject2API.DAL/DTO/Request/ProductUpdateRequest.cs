using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public class ProductUpdateRequest
    {
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public double? Rate { get; set; }
        public int? Qauntity { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public IFormFile? MainImg { get; set; }
        public List<ProductTranslationRequest>? Translations { get; set; }
    }
}
