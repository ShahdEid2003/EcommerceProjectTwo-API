using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public int cat_id { get; set; }
        public string UserCreated { get; set; }
        public string Name { get; set; }
        //public List<CategoryTranslationResponse> Translations { get; set; }
    }
}
