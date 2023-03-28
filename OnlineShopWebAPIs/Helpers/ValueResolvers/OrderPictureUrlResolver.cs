
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
        private readonly IUnitOfWork _unitOfWork;
        public OrderPictureUrlResolver(IConfiguration configuration, IUnitOfWork unitofWork)
        {
            _configuration = configuration;
            _unitOfWork = unitofWork;
        }

        public string Resolve(OrderedItem source, OrderedItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            {
                var catId = _unitOfWork.Products.GetById(source.ProductItemOrdered.ProductId).categoryId;

                return _configuration["ApiUrl"]+ "Images/Productsimages/cat"+ catId + "/" + source.ProductItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}
