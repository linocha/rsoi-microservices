using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Products.Domain.Models;
using Products.Domain.Services;
using Products.Resources;
using Products.Extensions;

namespace Products.Controllers
{
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetAllAsync()
        {
            var products = await _productService.ListAsync();
            // map return data 
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
        {
            // validating the request data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            //  mapping the resource to our model
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            
            // get result from model
            var result = await _productService.SaveAsync(product);

            // API returns a bad request
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            // API maps the new category (now including data such as the new Id) to our previously created ProductResource
            var productResource = _mapper.Map<Product, ProductResource>(result.Product);
            // sends it to the client
            return Ok(productResource);
        }
    }
}