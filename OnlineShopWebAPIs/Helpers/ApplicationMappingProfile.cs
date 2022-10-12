using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Models;

namespace OnlineShopWebAPIs.Helpers
{
    public class ApplicationMappingProfile :Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
            .ForMember(dest =>
                dest.categoryName,
                opt => opt.MapFrom(src => src.category.categoryName));
        }
    }
}
