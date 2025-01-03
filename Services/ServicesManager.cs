using AutoMapper;
using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly Lazy<IAuthenticationServices> authenticationServices;

        public ServicesManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository,UserManager<User> userManager)
        {
            productServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
            basketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));
            authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager));
        }

        public IProductServices ProductServices { get => productServices.Value; }

        public IBasketServices BasketServices => basketServices.Value;

        public IAuthenticationServices AuthenticationServices => authenticationServices.Value;
    }
}
