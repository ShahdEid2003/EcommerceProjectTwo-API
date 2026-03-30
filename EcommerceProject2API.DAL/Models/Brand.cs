using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Models
{
    public class Brand:AuditableEntity
    {
        public int Id { get; set; }
        public string LogoImg { get; set; }
        public List<Product> Products { get; set; }
        public List<BrandTranslations> Translations { get; set; }
    }
}
