using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.DTO.Request
{
    public class BrandRequest
    {
        public IFormFile LogoImg { get; set; }
        
        public List<BrandTranslationRequest> Translations { get; set; }
    }
}
