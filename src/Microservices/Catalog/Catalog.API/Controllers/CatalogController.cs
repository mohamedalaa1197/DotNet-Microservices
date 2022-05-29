using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is and Error happened : Message:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string Id)
        {
            try
            {
                var product = await _productRepository.GetProductById(Id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is and Error happened : Message:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("category/{category}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            try
            {
                var product = await _productRepository.GetProductByCategory(category);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is and Error happened : Message:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            try
            {
                await _productRepository.CreateProduct(product);
                return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is and Error happened : Message:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var result = await _productRepository.UpdateProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is and Error happened : Message:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            try
            {
                var result = await _productRepository.DeleteProduct(productId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is and Error happened : Message:{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
