using Contracts;
using Domain.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);
            if (value.IsNullOrEmpty)
            {

                return null;

            }
            return JsonSerializer.Deserialize<CustomerBasket?>(value);
        }
        public async Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket customerBasket, TimeSpan? timetoLive = null)
        {
            if(customerBasket is not null)
            {
                var jsonBasket = JsonSerializer.Serialize(customerBasket);
                var isUpdatedOrCreated = await _database.StringSetAsync(customerBasket.Id, jsonBasket, timetoLive ?? TimeSpan.FromDays(30));
                if (isUpdatedOrCreated) {
                    return await GetBasketAsync(customerBasket.Id);
                }
            }
            return null;
        }
    }
}
