using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductServices
    {
        public Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort,int? brandid,int? typeid);
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        public Task<ProductResultDto> GetProductByIdAsync(int id);
        
    }
}
