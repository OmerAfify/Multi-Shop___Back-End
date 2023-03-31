using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Interfaces.IServices
{
    public interface IPaymentService
    {
      public Task<ShoppingCart> CreateOrUpdatePaymentIntent(string shoppingCartId);

    }
}
