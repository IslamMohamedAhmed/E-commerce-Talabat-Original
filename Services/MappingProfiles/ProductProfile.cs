using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Options;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductResultDto>().ForMember(r => r.BrandName, options => options.MapFrom(m => m.productBrand.Name)).ForMember(r=>r.PictureUrl,Options => 
            Options.MapFrom<PictureUrlResolver>()).ForMember(r => r.TypeName, options => options.MapFrom(m => m.productType.Name));
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
        }
    }
}
