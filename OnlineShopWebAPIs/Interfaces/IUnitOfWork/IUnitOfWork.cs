using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopWebAPIs.Interfaces.IGeneralRepository;
using OnlineShopWebAPIs.Models;

namespace OnlineShopWebAPIs.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGeneralRepository<Product> Products { get; } 
        IGeneralRepository<Category> Categories { get; } 
        IGeneralRepository<Review> Reviews { get; }

        public void Save();
    }
}
