using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services.Interface;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            
            try
            {
               
                var products = await productAppService.GetAllProducts();

                return Ok(products);

                
            }
            catch(Exception error)
            {
                return Ok(error);
            }
          
        }

    }
}