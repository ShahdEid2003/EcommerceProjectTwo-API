using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public class ProductFiliterRequest:PagenationRequest
    {
        public int? CategoryId { get; set; }

        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public double? MinRate { get; set; }
    }
}
