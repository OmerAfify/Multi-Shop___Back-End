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
    public class CategoryPictureUrlResolver : IValueResolver<Category, CategoryDTO, string>
    {
        private readonly IConfiguration _configuration;

        public CategoryPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Category source, CategoryDTO destination, string destMember, ResolutionContext context)
        {
          
                if (!string.IsNullOrEmpty(source.categoryImagePath))
                {
                    return _configuration["ApiUrl"] + source.categoryImagePath;
                }
                else
                {
                    return null;
                }

        }
    }
}
