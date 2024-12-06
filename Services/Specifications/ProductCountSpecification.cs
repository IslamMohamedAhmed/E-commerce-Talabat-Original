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
    internal class ProductCountSpecification : Specifications<Product>
    {
        public ProductCountSpecification(SpecificationValues values) : base(product => (!values.BrandId.HasValue || values.BrandId == product.BrandId)
        && (!values.TypeId.HasValue || values.TypeId == product.TypeId) &&
        (string.IsNullOrWhiteSpace(values.Search) || product.Name.ToLower().Contains(values.Search.ToLower().Trim())))
        {
            
        }
    }
}
