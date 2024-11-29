using AutoMapper;
using Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandresult = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandresult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort,int? brandid, int? typeid)
        {
            var products =await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(sort,brandid,typeid));
            var productresult = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productresult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var products = await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var productresult = mapper.Map<IEnumerable<TypeResultDto>>(products);
            return productresult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product,int>().GetAsync(new ProductWithBrandAndTypeSpecifications(id));
            var productresult = mapper.Map<ProductResultDto>(product);
            return productresult;
        }
    }
}
