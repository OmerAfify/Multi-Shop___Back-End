using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Interfaces.IGeneralRepository;
using Models.Interfaces.IUnitOfWork;
using Models.Models;
using OnlineShopWebAPIs.BusinessLogic.DBContext;
using OnlineShopWebAPIs.BusinessLogic.GeneralRepository;

namespace OnlineShopWebAPIs.BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly OnlineShopDbContext _context;
        
        public IGeneralRepository<Product> Products { get; }
        public IGeneralRepository<Category> Categories { get; }
        public IGeneralRepository<Review> Reviews { get; }
        public IGeneralRepository<ProductImage> ProductImages { get; }



        public IGeneralRepository<OrderDeliveryMethods> DeliveryMethods { get; }
        public IGeneralRepository<Order> Orders { get; }



        public UnitOfWork(OnlineShopDbContext context)
        {
            _context = context;

            Products = new GeneralRepository<Product>(context);
            Categories = new GeneralRepository<Category>(context);
            Reviews = new GeneralRepository<Review>(context);
            ProductImages = new GeneralRepository<ProductImage>(context);

            DeliveryMethods = new GeneralRepository<OrderDeliveryMethods>(_context);
            Orders = new GeneralRepository<Order>(_context);

        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
