using Contracts;
using Domain.Models;
using Shared;
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
        public ProductWithBrandAndTypeSpecifications(SpecificationValues values) : base(product => (!values.BrandId.HasValue || values.BrandId == product.BrandId)
        && (!values.TypeId.HasValue || values.TypeId == product.TypeId))
        {
            AppIncludes(product => product.productType);
            AppIncludes(product => product.productBrand);

            if (values.sort is not null) {
                switch (values.sort)
                {
                    case sortingValues.priceasc:
                        SetOrder(product => product.Price);
                        break;
                    case sortingValues.pricedesc:
                        SetOrderDescending(product => product.Price);
                        break;
                    case sortingValues.nameasc:
                        SetOrder(product => product.Name);
                        break;

                    case sortingValues.namedesc:
                        SetOrderDescending(product => product.Name);
                        break;

                }
            }
           
                SetPagination(values.PageIndex,values.PageSize);
            
        }




    }
}
