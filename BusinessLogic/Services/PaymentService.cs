using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Models.Interfaces.IBusinessRepository;
using Models.Interfaces.IServices;
using Models.Interfaces.IUnitOfWork;
using Models.Models;
using Stripe;

namespace BusinessLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public PaymentService(IConfiguration config, IUnitOfWork unitOfWork, IShoppingCartRepository shoppingCartRepository)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> CreateOrUpdatePaymentIntent(string shoppingCartId)
        {

            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var shippingPrice = 0m;

            var shoppingCart = _shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId).Result;

            if (shoppingCart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.DeliveryMethods.GetByIdAsync((int)shoppingCart.DeliveryMethodId);
                shippingPrice = (decimal)deliveryMethod.DeliveryPrice;
            }


            foreach(var item in shoppingCart.items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.productId);

                if(item.salesPrice != product.salesPrice)
                {
                    item.salesPrice = product.salesPrice;
                }

            }


            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(shoppingCart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {

                    Amount = (long)shoppingCart.items.Sum(i=>i.quantity * (i.salesPrice * 100)) + (long)(shippingPrice*100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };

                intent = await service.CreateAsync(options);

                shoppingCart.PaymentIntentId = intent.Id;
                shoppingCart.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)shoppingCart.items.Sum(i => i.quantity * (i.salesPrice * 100)) + (long)(shippingPrice * 100),
                };

                await service.UpdateAsync(shoppingCart.PaymentIntentId, options);

            }

            await _shoppingCartRepository.CreateOrUpdateShoppingCartAsync(shoppingCart);

            return shoppingCart;

        }

     
    }
}
