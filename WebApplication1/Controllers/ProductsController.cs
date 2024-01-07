using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IServiceGeneric<Product, ProductDto> _productservice;

        public ProductsController(IServiceGeneric<Product, ProductDto> productservice)
        {
            _productservice = productservice;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return ActionResultInstance(await _productservice.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _productservice.AddAsync(productDto));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _productservice.Update(productDto, productDto.Id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id )
        {
            return ActionResultInstance(await _productservice.Remove(id));
        }
    }
}
