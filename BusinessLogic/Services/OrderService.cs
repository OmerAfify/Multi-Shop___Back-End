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

      
        public  Order GetOrderById(string buyerEmail, int orderId)
        {
            var order = _unitOfWork.Orders.Find(o => o.Email == buyerEmail && o.OrderId == orderId,
                new List<string>(){ "DeliveryMethod", "OrderStatus", "OrderedItems"});

        return order;
        }

        public IEnumerable<Order> GetUserOrders(string email)
        {
            var orders = _unitOfWork.Orders.FindRange(o => o.Email == email,
                    new List<string>() { "DeliveryMethod", "OrderStatus", "OrderedItems" },
                    o => o.OrderByDescending(o => o.OrderDate)
                    ); 


            return orders;
        }


        public  IEnumerable<OrderDeliveryMethods> GetDeliveryMethods()
        {
            return  _unitOfWork.DeliveryMethods.GetAll(); 
        }


        public Order CreateOrder(string buyerEmail, string shoppingCartId, int deliveryMethodId, OrderAddress orderAddress)
        {
            var orderedItemsList = new List<OrderedItem>();

            var shoppingCart = _shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId).Result;

            if (shoppingCart == null)
                return null;

            foreach (var cartItem in shoppingCart.items)
            {

                var productItem = _unitOfWork.Products.Find(i => i.productId == cartItem.productId, new List<string>() { "productImages" });

                if (productItem == null)
                    break;

                var productItemOrdered = new ProductItemOrdered(productItem.productId, productItem.productName, productItem.productImages[0].productImageName, (decimal)productItem.salesPrice);

                var orderedItem = new OrderedItem(productItemOrdered, cartItem.quantity,cartItem.quantity * (decimal)cartItem.salesPrice);

                orderedItemsList.Add(orderedItem);
            }

            var subtotal = orderedItemsList.Sum(o => o.TotalPrice);
            var deliveryMethod = _unitOfWork.DeliveryMethods.GetById(deliveryMethodId);

            var order = new Order(orderedItemsList, buyerEmail, orderAddress, subtotal, subtotal + (decimal)deliveryMethod.DeliveryPrice,
            1, deliveryMethodId);


            _unitOfWork.Orders.Insert(order);


            var result = _unitOfWork.Save();

            if (result <= 0)
                return null;
            else
                return order;
        }
    }

}
