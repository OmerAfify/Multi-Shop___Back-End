using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Interfaces.IBusinessRepository;
using Models.Interfaces.IGenericRepository;
using Models.Models;
using OnlineShopWebAPIs.Models;

namespace Models.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductsRepository Products { get; } 
        public IGenericRepository<Category> Categories { get; } 
        public IGenericRepository<Review> Reviews { get; }
        public IGenericRepository<ProductImage> ProductImages { get; }

        public IGenericRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGenericRepository<Order> Orders { get; }
        
        public IGenericRepository<ShoppingCart> ShoppingCart { get; }
     //   public IGeneralRepository<CartItem> CartItem { get; }

        public Task<int> Save();
    }
}
