using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public class BrandTranslationRequest
    {
       
        public string Name { get; set; }
       
        public string Language { get; set; }="en";
    }
}
