using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Response
{
    public class CartResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string MainImg { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Count { get; set; }

        public decimal Subtotal => Count * (Price - (Price * Discount) / 100);
    }
}
