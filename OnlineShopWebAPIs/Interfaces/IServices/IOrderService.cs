using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using OnlineShopWebAPIs.Models;

namespace Interfaces.IServices
{
    public interface IOrderService
    {
        public Order CreateOrder(string buyerEmail, ShoppingCart shoppingCart,
                                             int deliveryMethodId, OrderAddress orderAddress);

        public Order GetOrderById(string buyerEmail, int orderId);
        public IEnumerable<Order> GetUserOrders(string email);
        public IEnumerable<OrderDeliveryMethods> GetDeliveryMethods();




    }

}
