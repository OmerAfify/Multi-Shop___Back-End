using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces.IBusinessRepository;
using Models.Interfaces.IUnitOfWork;
using Models.Models;

namespace BusinessLogic.Repository.BusinessRepository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ShoppingCart> GetShoppingCartByIdAsync(string id)
        {
            return  await _unitOfWork.ShoppingCart.FindAsync(i => i.id == id, new List<string>() { "items" });
        }

        public async Task<ShoppingCart> CreateOrUpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            var cart = await _unitOfWork.ShoppingCart.FindAsync(i => i.id == shoppingCart.id,new List<string>() { "items" });

            if(cart==null)
            {
                _unitOfWork.ShoppingCart.InsertAsync(shoppingCart);
            }
            else
            {
                cart.id = shoppingCart.id;
                cart.ClientSecret = shoppingCart.ClientSecret;
                cart.DeliveryMethodId = shoppingCart.DeliveryMethodId;
                cart.items = shoppingCart.items;
                cart.PaymentIntentId = shoppingCart.PaymentIntentId;
                cart.shippingPrice = shoppingCart.shippingPrice;


               _unitOfWork.ShoppingCart.Update(cart);

            }

            var result  = await _unitOfWork.Save();

            if (result < 1)
                return null;

            return shoppingCart;


        }

        public async Task<bool> DeleteShoppingCartAsync(string id)
        {
            var ShoppingCart = await _unitOfWork.ShoppingCart.FindAsync(i=>i.id==id);

            _unitOfWork.ShoppingCart.Delete(ShoppingCart);

            var result = await _unitOfWork.Save();

            if (result < 1)
                return false;

            return true;


        }

    
    }
}
