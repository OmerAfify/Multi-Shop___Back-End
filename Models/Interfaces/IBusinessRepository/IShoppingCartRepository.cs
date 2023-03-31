using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Interfaces.IBusinessRepository
{
    public interface IShoppingCartRepository
    {
        public Task<ShoppingCart> GetShoppingCartByIdAsync(string id);

        public Task<ShoppingCart> CreateOrUpdateShoppingCartAsync(ShoppingCart shoppingCart);

        public Task<bool> DeleteShoppingCartAsync(string id);

    }
}