using EcommerceProject2API.DAL.DTO.Request;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(destniation => destniation.cat_id, source => source.Id)//هون عشان نربط مثلا اذا تغير اسم الاي دي بالريسبونس والكاتيجوري الموديل نربط انو هدول نفس بعض 
                .Map(destniation => destniation.UserCreated, source => source.CreatedBy.UserName)
                .Map(dest => dest.Name, source => source.Translations
                .Where(t => t.Language == CultureInfo.CurrentCulture.Name).Select(t => t.Name).FirstOrDefault());
            //.Map(dest => dest.Name,source => source.Translations
            // .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
            // .Select(t => t.Name)
            // .FirstOrDefault()); هاي طريقة تمرير اللغة بالكويري (الباراميتير


            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(destniation => destniation.UserCreated, source => source.CreatedBy.UserName)
                .Map(dest => dest.Name, source => source.Translations
                .Where(t => t.Language == CultureInfo.CurrentCulture.Name).Select(t => t.Name).FirstOrDefault())
                .Map(dest=>dest.MainImg,source=>$"https://localhost:7186/images/{source.MainImg}")
                .Map(dest => dest.SubImages,
                 src => src.SubImages.Select(s => $"https://localhost:7186/images/{s.ImagePath}"));

            TypeAdapterConfig<ProductUpdateRequest, Product>.NewConfig().IgnoreNullValues(true);

            TypeAdapterConfig<Brand, BrandResponse>.NewConfig()
               .Map(destniation => destniation.UserCreated, source => source.CreatedBy.UserName)
               .Map(dest => dest.Name, source => source.Translations
               .Where(t => t.Language == CultureInfo.CurrentCulture.Name)
               .Select(t => t.Name).FirstOrDefault() ?? "Default Brand Name")
               .Map(dest => dest.LogoImg, source => $"https://localhost:7186/images/{source.LogoImg}");

            TypeAdapterConfig<Cart, CartResponse>.NewConfig()
                .Map(dest => dest.ProductName, source => source.Product.Translations
                .Where(t => t.Language == CultureInfo.CurrentCulture.Name)
                .Select(t => t.Name).FirstOrDefault())
                .Map(dest => dest.MainImg, source => $"https://localhost:7186/images/{source.Product.MainImg}")
                .Map(dest => dest.Price, source => source.Product.Price)
                .Map(dest => dest.Discount, source => source.Product.Discount);
            TypeAdapterConfig<OrderItem, OrderItemResponse>.NewConfig()
                .Map(dest => dest.ProductName, source => source.Product.Translations
                .Where(t => t.Language == CultureInfo.CurrentCulture.Name)
                .Select(t => t.Name).FirstOrDefault());
                

        }
    }
}
