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
        public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(SpecificationValues values);
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        public Task<ProductResultDto> GetProductByIdAsync(int id);
        
    }
}
