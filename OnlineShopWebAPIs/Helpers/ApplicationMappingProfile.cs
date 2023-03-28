using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using Helpers.ValueResolvers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using Models.Models;
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


            CreateMap<OrderAddress, OrderAddressDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();



            CreateMap<OrderReturnedDTO, Order>().ReverseMap()
             .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
             .ForMember(d => d.Status, opt => opt.MapFrom(s => s.OrderStatus.StatusName))
             .ForMember(d => d.DeliveryPrice, opt => opt.MapFrom(s => s.DeliveryMethod.DeliveryPrice));

            CreateMap<OrderedItemDTO, OrderedItem>().ReverseMap()
                     .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductItemOrdered.ProductId))
                     .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.ProductItemOrdered.ProductName))
                     .ForMember(d => d.ProductSalesPrice, opt => opt.MapFrom(s => s.ProductItemOrdered.SalesPrice))
                     .ForMember(d => d.PictureUrl, opt => opt.MapFrom<OrderPictureUrlResolver>());


        }
    }
}
