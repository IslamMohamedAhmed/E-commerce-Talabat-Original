using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    
    public class BasketController(IServicesManager servicesManager) : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> Get(string id)
        {
            var basket = await servicesManager.BasketServices.GetCustomerBasket(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> Update(CustomerBasketDto customerBasketDto)
        {
            var basket = await servicesManager.BasketServices.UpdateCustomerBasket(customerBasketDto);
            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await servicesManager.BasketServices.DeleteBasket(id);
            return NoContent();
        }
    }
}
