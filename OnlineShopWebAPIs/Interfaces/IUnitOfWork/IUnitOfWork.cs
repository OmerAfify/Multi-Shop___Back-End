using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using OnlineShopWebAPIs.Interfaces.IGeneralRepository;
using OnlineShopWebAPIs.Models;

namespace OnlineShopWebAPIs.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGeneralRepository<Product> Products { get; } 
        IGeneralRepository<Category> Categories { get; } 
        IGeneralRepository<Review> Reviews { get; }
        IGeneralRepository<ProductImage> ProductImages { get; }

        public IGeneralRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGeneralRepository<Order> Orders { get; }

        public int Save();
    }
}
