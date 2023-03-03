using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Models;
using OnlineShopWebAPIs.Models.SettingsModels;

namespace OnlineShopWebAPIs.Helpers
{
    public class ApplicationMappingProfile :Profile
    {
        public ApplicationMappingProfile()
        {

            CreateMap<Product, AddProductDTO>().ReverseMap();
   
            CreateMap<Product, ProductDTO>()
            .ForMember(dest =>
                dest.categoryName,
                opt => opt.MapFrom(src => src.category.categoryName));


            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>()
            .ForMember(dest =>
                dest.productsNumber,
                opt => opt.MapFrom(src => src.products.Count));

            CreateMap<Category, AddCategoryDTO>().ReverseMap();


            CreateMap<UserDTO, IdentityUserContext>().ReverseMap();

        }
    }
}
