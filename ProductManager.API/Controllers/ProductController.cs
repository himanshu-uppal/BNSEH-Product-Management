using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services.Interface;
using ProductManager.Common;
using ProductManager.Dto.Product;

namespace ProductManager.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService productAppService;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductAppService productAppService, ILogger<ProductController> logger)
            : base()
        {
            _logger = logger;
            this.productAppService = productAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts([FromQuery(Name = "productName")] string productName, [FromQuery(Name = "pageNumber")] int pageNumber, [FromQuery(Name = "pageSize")] int pageSize)
        {
            _logger.LogInformation("Getting all products");
            pageNumber = (pageNumber <= 0) ? Constants.DEFAULT_PAGE_NUMBER : pageNumber;
            pageSize = (pageSize <= 0) ? Constants.DEFAULT_PAGE_SIZE : pageSize;
            productName = productName != null ? productName.Trim() : null;
            _logger.LogInformation("Getting all products with criteria - Page number - {0}, Page Size - {1} and Product Name search - {2}", pageNumber, pageSize, productName);

            try
            {
               
                var products = await productAppService.GetAllProducts(productName,pageNumber, pageSize);

                _logger.LogInformation("All products - {0} ", products);
               
                return Ok(products);

                
            }
            catch(Exception error)
            {
                _logger.LogInformation("Getting All products : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
          
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAProductById(int productId)
        {
            _logger.LogInformation("GetAProductById");

           if(productId <= 0)
            {
                return BadRequest();
            }
         
            _logger.LogInformation("GetAProductById - with Product Id - {0}", productId);

            try
            {

                var product = await productAppService.GetAProductById(productId);

                if (product != null)
                {
                    _logger.LogInformation("Product with id- {0} found , product details -  ", productId, product);
                    return Ok(product);
                }

                return NotFound();        


            }
            catch (Exception error)
            {
                _logger.LogInformation("GetAProductById : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            _logger.LogInformation("DeleteProduct");

            if (productId <= 0)
            {
                return BadRequest();
            }

            _logger.LogInformation("DeleteProduct - with Product Id - {0}", productId);

            try
            {

                var product = await productAppService.DeleteProduct(productId);

                if (product != null)
                {
                    _logger.LogInformation("Product with id- {0} found and deleted , product details -  ", productId, product);
                    return Ok();
                }

                return NotFound();


            }
            catch (Exception error)
            {
                _logger.LogInformation("DeleteProduct : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPatch("{productId}/price")]
        public async Task<IActionResult> UpdateProductPrice(int productId, [FromBody] ProductDto productDto)
        {
            _logger.LogInformation("UpdateProductPrice");

            double updatedProductPrice = productDto.Price;

            if (productId <= 0 || updatedProductPrice <=0)
            {
                return BadRequest();
            }

            _logger.LogInformation("UpdateProductPrice - with Product Id - {0} and updated product price - {1}", productId, updatedProductPrice);

            try
            {

                var updateResult = await productAppService.UpdateProductPrice(productId, updatedProductPrice);

                if (updateResult.IsSuccess)
                {
                    _logger.LogInformation("Product with id- {0} found , updated product details -  ", productId, updateResult.Data);
                    return Ok();
                }

                if(updateResult.MainMessage.Code == Constants.NotFound)
                return NotFound();

                return Conflict(updateResult.MainMessage.Text);

            }
            catch (Exception error)
            {
                _logger.LogInformation("UpdateProductPrice : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}