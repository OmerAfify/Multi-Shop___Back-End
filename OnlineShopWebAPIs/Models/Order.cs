using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public Order()
        {
                
        }

        public Order(IReadOnlyList<OrderedItem> orderedItems,  string email, OrderAddress shippingAddress,
            decimal subTotal, decimal total,  int orderStatusId, int deliveryMethodId)
        {
            OrderedItems = orderedItems;
            Email = email;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
            Total = total;
            OrderStatusId = orderStatusId;
            DeliveryMethodId = deliveryMethodId;

        }

        public int OrderId { get; set; }
        public string Email { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderAddress ShippingAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public IReadOnlyList<OrderedItem> OrderedItems { get; set; }        

        public int OrderStatusId{ get; set; }
        public OrderStatus OrderStatus{ get; set; }

        public int DeliveryMethodId { get; set; }
        public OrderDeliveryMethods DeliveryMethod { get; set; }

    }
}
