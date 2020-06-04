using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services.Interface;
using ProductManager.Common;
using ProductManager.DataAccess.Models;
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

        [Authorize(Policy=Constants.ADMIN_ROLE_NAME)]
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            _logger.LogInformation("CreateProduct");

            _logger.LogInformation("CreateProduct -  with details {0}", product);

            try
            {

                var productDto = await productAppService.CreateProduct(product);

                if (productDto != null)
                {
                    _logger.LogInformation("Product is created");
                    return Ok();
                }

               return Conflict();

            }
            catch (Exception error)
            {
                _logger.LogInformation("CreateProduct : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [Authorize(Policy = Constants.ADMIN_ROLE_NAME)]
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

        [Authorize(Policy = Constants.ADMIN_ROLE_NAME)]
        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateProductByPatch(int productId, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            _logger.LogInformation("UpdateProductByPatch");

           _logger.LogInformation("UpdateProductByPatch - product with id - {0} , with patch with details {1}", productId, patchDoc);

            try
            {

                var updateResult = await productAppService.UpdateProductByPatch(productId, patchDoc);

                if (updateResult.IsSuccess)
                {
                    _logger.LogInformation("Product with id- {0} found , updated product details -  ", productId, patchDoc);
                    return Ok();
                }

                if(updateResult.MainMessage.Code == Constants.NotFound)
                return NotFound();

                return Conflict(updateResult.MainMessage.Text);

            }
            catch (Exception error)
            {
                _logger.LogInformation("UpdateProductByPatch : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [Authorize(Policy = Constants.ADMIN_ROLE_NAME)]
        [HttpPut("{productid}")]
        public async Task<IActionResult> UpdateProduct(int productId,[FromBody] ProductDto product)
        {
            _logger.LogInformation("UpdateProduct");

            _logger.LogInformation("UpdateProduct -  with details {0}", product);

            try
            {

                var productUpdateResult = await productAppService.UpdateProduct(productId,product);

                if (productUpdateResult.IsSuccess)
                {
                    _logger.LogInformation("Product is updated");
                    return Ok(productUpdateResult.Data);
                }

                return Conflict(productUpdateResult.MainMessage.Text);

            }
            catch (Exception error)
            {
                _logger.LogInformation("UpdateProduct : Error - {0} ", error);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}