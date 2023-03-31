using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Interfaces.IGeneralRepository;
using Models.Models;
using OnlineShopWebAPIs.Models;

namespace Models.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IGeneralRepository<Product> Products { get; } 
        public IGeneralRepository<Category> Categories { get; } 
        public IGeneralRepository<Review> Reviews { get; }
        public IGeneralRepository<ProductImage> ProductImages { get; }

        public IGeneralRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGeneralRepository<Order> Orders { get; }
        
        public IGeneralRepository<ShoppingCart> ShoppingCart { get; }
     //   public IGeneralRepository<CartItem> CartItem { get; }

        public int Save();
    }
}
