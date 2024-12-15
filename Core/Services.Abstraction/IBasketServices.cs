using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IBasketServices
    {
        public Task<CustomerBasketDto?> GetCustomerBasket(string id);
        public Task<CustomerBasketDto?> UpdateCustomerBasket(CustomerBasketDto customerBasket);

        public Task<bool> DeleteBasket(string id);
            
    }
}
