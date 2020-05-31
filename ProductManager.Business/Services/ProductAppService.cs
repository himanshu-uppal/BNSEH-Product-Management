using Microsoft.EntityFrameworkCore;
using ProductManager.Business.Services.Interface;
using ProductManager.DataAccess.Models;
using ProductManager.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Business.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository productRepository;
        public ProductAppService(IProductRepository productRepository) : base()
        {

            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await productRepository.All().ToListAsync();

            return products;
        }
    }
        
}
