﻿using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.ErrorBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts([FromQuery] SpecificationValues values)
        {
            var products = await servicesManager.ProductServices.GetAllProductsAsync(values);
            return Ok(products);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await servicesManager.ProductServices.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var types = await servicesManager.ProductServices.GetAllTypesAsync();
            return Ok(types);
        }
        [ProducesResponseType(typeof(ErrorDetails),(int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails),(int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationErrorResponse),(int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductResultDto),(int) HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int id)
        {
            var product = await servicesManager.ProductServices.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
