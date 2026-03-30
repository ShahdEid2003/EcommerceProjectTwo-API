using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Models
{
    public class BrandTranslations
    {
        public int Id { get; set; }
        public string Language { get; set; } = "en";
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
