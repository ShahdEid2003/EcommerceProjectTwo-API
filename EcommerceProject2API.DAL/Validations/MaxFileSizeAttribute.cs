using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Validations
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var sizeInMb = file.Length / (1024 * 1024);
                if (sizeInMb > _maxFileSize)
                {
                    return new ValidationResult($"allowed size is :{_maxFileSize}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
