using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServicesManager
    {
        public IProductServices ProductServices { get; }
        public IBasketServices BasketServices { get; }
    }
}
