using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Repository.BusinessRepository;
using BusinesssLogic.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Interfaces.IBusinessRepository;
using Models.Interfaces.IGenericRepository;
using Models.Interfaces.IUnitOfWork;
using Models.Models;
using OnlineShopWebAPIs.BusinessLogic.DBContext;

namespace OnlineShopWebAPIs.BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly OnlineShopDbContext _context;
        
        public IProductsRepository Products { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Review> Reviews { get; }
        public IGenericRepository<ProductImage> ProductImages { get; }


        public IGenericRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGenericRepository<Order> Orders { get; }

        public IGenericRepository<ShoppingCart> ShoppingCart { get; }
     

        public UnitOfWork(OnlineShopDbContext context)
        {
            _context = context;

            Products = new ProductsRepository(context);
            Categories = new GenericRepository<Category>(context);
            Reviews = new GenericRepository<Review>(context);
            ProductImages = new GenericRepository<ProductImage>(context);

            DeliveryMethods = new GenericRepository<OrderDeliveryMethods>(_context);
            Orders = new GenericRepository<Order>(_context);
            
            ShoppingCart = new GenericRepository<ShoppingCart>(_context);
            //CartItem = new GenericRepository<CartItem>(_context);

        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
