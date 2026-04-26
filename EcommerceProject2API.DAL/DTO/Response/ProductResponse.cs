using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public double Rate { get; set; }
        public int Qauntity { get; set; }
        public string UserCreated {  get; set; }
       public string Name { get; set; }
        public string MainImg { get; set; }
        public List<string> SubImages { get; set; }

    }
}
