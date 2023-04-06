using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Models.Models;
using OnlineShopWebAPIs.DTOs;

namespace Helpers.ValueResolvers
{
    public class ProductPictureUrlResolver : IValueResolver<ProductImage, ProductImageDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(ProductImage source, ProductImageDTO destination, string destMember, ResolutionContext context)
        {
          
                if (!string.IsNullOrEmpty(source.productImagePath))
                {
                    return _configuration["ApiUrl"] + source.productImagePath;
                }
                else
                {
                    return null;
                }

        }
    }
}
