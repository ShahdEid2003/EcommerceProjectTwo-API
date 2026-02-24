using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }
       
        public List<CategoryTranslations>Translations { get; set; }
    }
}
