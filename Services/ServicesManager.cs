using AutoMapper;
using Contracts;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager : IServicesManager
    {
        private readonly Lazy<IProductServices> productServices;
        private readonly Lazy<IBasketServices> basketServices;

        public ServicesManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository)
        {
            productServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
            basketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
        }

        public IProductServices ProductServices { get => productServices.Value; }

        public IBasketServices BasketServices => basketServices.Value;
    }
}
