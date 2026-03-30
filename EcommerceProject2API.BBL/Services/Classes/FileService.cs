using EcommerceProject2API.BBL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class FileService : IFileService
    {
        public void Delete(string fileName)
        {
            var path= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images", fileName);
            if (File.Exists(path)) File.Delete(path);
        }

       

        public async Task<string?> UploadeAsync(IFormFile file)
        {
            // التأكد أن الملف موجود وحجمه أكبر من صفر
            if (file != null && file.Length > 0)
            {
                // إنشاء اسم جديد عشوائي للملف لمنع تكرار الأسماء
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // تحديد المسار الذي سيتم حفظ الملف فيه داخل مجلد images
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", fileName);

                // إنشاء ملف جديد في المسار المحدد
                using (var stream = File.Create(filePath))
                {
                    // نسخ محتوى الملف المرفوع إلى الملف الجديد
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            return null;
        }
    }
}
