using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public class CategoryRequest
    {
       public List<CategoryTranslationRequest> Translations { get; set; }
    }
}
