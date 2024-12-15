using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);

        public Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket customerBasket, TimeSpan? timetoLive = null);

        public Task<bool> DeleteAsync(string id);
    }
}
