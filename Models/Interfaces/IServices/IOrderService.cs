using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Models;
using OnlineShopWebAPIs.Models;

namespace Models.Interfaces.IServices
{
    public interface IOrderService
    {
        public Task<Order> CreateOrder(string buyerEmail, string shoppingCartId,
                                             int deliveryMethodId, OrderAddress orderAddress);

        public Task<Order> GetOrderByIdAsync(string buyerEmail, int orderId);
        public Task<IEnumerable<Order>> GetUserOrdersAsync(string email);
        public Task<IEnumerable<OrderDeliveryMethods>> GetDeliveryMethods();




    }

}
