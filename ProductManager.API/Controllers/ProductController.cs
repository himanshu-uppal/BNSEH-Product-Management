using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Business.Services.Interface;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService productAppService;

        public ProductController(IProductAppService productAppService)
            : base()
        {
            this.productAppService = productAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            
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