using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;

namespace DTOs
{
    public class OrderReturnedDTO
    {
        public int OrderId { get; set; }
        public string Email { get; set; }

        public DateTime OrderDate { get; set; }
        public OrderAddressDTO ShippingAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public IReadOnlyList<OrderedItemDTO> OrderedItems { get; set; }

        public string Status{ get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryPrice { get; set; }


    }
}
