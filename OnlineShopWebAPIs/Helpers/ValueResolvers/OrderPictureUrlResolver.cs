
using AutoMapper;
using DTOs;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Interfaces.IUnitOfWork;
using Models.Models;

namespace Helpers.ValueResolvers
{
    public class OrderPictureUrlResolver : IValueResolver<OrderedItem, OrderedItemDTO, string>
    {
        private readonly IConfiguration _configuration;
        public OrderPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderedItem source, OrderedItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            {
                return _configuration["ApiUrl"]+ source.ProductItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}
