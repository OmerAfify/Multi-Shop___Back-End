using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Interfaces.IBusinessRepository;
using Models.Interfaces.IServices;
using Models.Interfaces.IUnitOfWork;
using Models.Models;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartRepository _shoppingCartRepository;
 
        public OrderService(IUnitOfWork unitOfWork, IShoppingCartRepository shoppingCartRepository)
        {
            _unitOfWork = unitOfWork;
            _shoppingCartRepository = shoppingCartRepository;
              
        }

      
        public async Task<Order> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var order = await _unitOfWork.Orders.FindAsync(o => o.Email == buyerEmail && o.OrderId == orderId,
                new List<string>(){ "DeliveryMethod", "OrderStatus", "OrderedItems"});

        return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string email)
        {
            var orders = await _unitOfWork.Orders.FindRangeAsync(o => o.Email == email,
                    new List<string>() { "DeliveryMethod", "OrderStatus", "OrderedItems" },
                    o => o.OrderByDescending(o => o.OrderDate)
                    ); 


            return orders;
        }


        public async Task<IEnumerable<OrderDeliveryMethods>> GetDeliveryMethods()
        {
            return  await _unitOfWork.DeliveryMethods.GetAllAsync(); 
        }


        public async Task<Order> CreateOrder(string buyerEmail, string shoppingCartId, int deliveryMethodId, OrderAddress orderAddress)
        {
            var orderedItemsList = new List<OrderedItem>();

            var shoppingCart = _shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId).Result;

            if (shoppingCart == null)
                return null;

            foreach (var cartItem in shoppingCart.items)
            {

                var productItem = await _unitOfWork.Products.FindAsync(i => i.productId == cartItem.productId, new List<string>() { "productImages" });

                if (productItem == null)
                    break;

                var productItemOrdered = new ProductItemOrdered(productItem.productId, productItem.productName, productItem.productImages[0].productImagePath, (decimal)productItem.salesPrice);

                var orderedItem = new OrderedItem(productItemOrdered, cartItem.quantity,cartItem.quantity * (decimal)cartItem.salesPrice);

                orderedItemsList.Add(orderedItem);
            }

            var subtotal = orderedItemsList.Sum(o => o.TotalPrice);
            var deliveryMethod = await _unitOfWork.DeliveryMethods.GetByIdAsync(deliveryMethodId);

            var order = new Order(orderedItemsList, buyerEmail, orderAddress, subtotal, subtotal + (decimal)deliveryMethod.DeliveryPrice,
            1, deliveryMethodId);


            _unitOfWork.Orders.InsertAsync(order);


            var result =await  _unitOfWork.Save();

            if (result <= 0)
                return null;
            else
            {
                return order;
            }
        }
    }

}
