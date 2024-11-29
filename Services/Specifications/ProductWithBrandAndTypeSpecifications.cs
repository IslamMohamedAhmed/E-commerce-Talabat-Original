using Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id) : base(product => product.Id == id)
        {
            AppIncludes(product => product.productType);
            AppIncludes(product => product.productBrand);
        }
        public ProductWithBrandAndTypeSpecifications(string? sort, int? brandId, int? typeId) : base(product=>(!brandId.HasValue || brandId == product.BrandId) 
        && (!typeId.HasValue || product.TypeId == typeId))
        {
            AppIncludes(product => product.productType);
            AppIncludes(product => product.productBrand);

            if (!string.IsNullOrWhiteSpace(sort)) {
                switch (sort.ToLower().Trim())
                {
                    case "priceasc":
                        SetOrder(product => product.Price);
                        break;
                    case "pricedsc":
                        SetOrderDescending(product => product.Price);
                        break;
                    case "nameasc":
                        SetOrder(product => product.Name);
                        break;

                    case "namedsc":
                        SetOrderDescending(product => product.Name);
                        break;

                }
            }
        }




    }
}
