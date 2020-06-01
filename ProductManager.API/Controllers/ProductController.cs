using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services.Interface;
using ProductManager.Common;

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

            try
            {
               
                var products = await productAppService.GetAllProducts(productName,pageNumber, pageSize);

                return Ok(products);

                
            }
            catch(Exception error)
            {
                return Ok(error);
            }
          
        }

    }
}