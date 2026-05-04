using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public class PagenationRequest
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;

        public string? Search { get; set;}
    }
}
