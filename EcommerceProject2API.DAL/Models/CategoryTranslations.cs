using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Models
{
    public class CategoryTranslations
    {
        public int Id { get; set; }

        public string Name { get; set; } = null;
        public string Language { get; set; } = "en";
        public int CategoryId {  get; set; }
        public Category Category { get; set; }

    }
}
