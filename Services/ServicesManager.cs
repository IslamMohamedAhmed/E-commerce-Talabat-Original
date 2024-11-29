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

        public ServicesManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            productServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
        }

        public IProductServices ProductServices { get => productServices.Value; }
    }
}
