using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Models
{
    public class Product:AuditableEntity
    {
        public int Id { get; set; }
        public decimal Price{ get; set; }
        public decimal Discount { get; set; }
        public double Rate { get; set; }
        public int Qauntity { get; set; }
        public string MainImg { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductTranslations> Translations { get; set; }
        


    }
}
