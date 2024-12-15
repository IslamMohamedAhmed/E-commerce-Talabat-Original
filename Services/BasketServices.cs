using AutoMapper;
using Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketServices(IBasketRepository basketRepository,IMapper mapper) : IBasketServices
    {
        public async Task<bool> DeleteBasket(string id)
        {
           return await basketRepository.DeleteAsync(id);
        }

        public async Task<CustomerBasketDto?> GetCustomerBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if(basket is null)
            {
                throw new CustomerBasketNotFoundException(id);
            }
            var basketDto = mapper.Map<CustomerBasketDto>(basket);
            return basketDto;

        }

        public async Task<CustomerBasketDto?> UpdateCustomerBasket(CustomerBasketDto customerBasket)
        {
            var basket = mapper.Map<CustomerBasket>(customerBasket);
            var UpdatedOrDeleted = await basketRepository.UpdateCustomerBasketAsync(basket);
            if(UpdatedOrDeleted is null)
            {
                throw new Exception("Can't update or add the item now!!!"); 
            }
            return mapper.Map<CustomerBasketDto>(basket);
        }
    }
}
