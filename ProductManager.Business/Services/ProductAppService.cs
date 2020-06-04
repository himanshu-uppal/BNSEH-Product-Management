using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services.Interface;
using ProductManager.Common;
using ProductManager.Common.ValueObjects;
using ProductManager.DataAccess.Models;
using ProductManager.DataAccess.Repositories.Interface;
using ProductManager.Dto.Product;
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

        private readonly ILogger<ProductAppService> _logger;
        private readonly IMapper _mapper;
        public ProductAppService(IProductRepository productRepository, ILogger<ProductAppService> logger, IMapper mapper) : base()
        {
            _logger = logger;
            this.productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<AllProductsDto> GetAllProducts(string productName,int pageNumber, int pageSize)
        {
            _logger.LogInformation("Start");
            var productsQueryable = productRepository.All();

            if (!string.IsNullOrWhiteSpace(productName))
            {
                _logger.LogInformation("Product Name : {0} search applied !!", productName);
                productsQueryable = productsQueryable.Where(p => EF.Functions.Like(p.Name, "%" + productName + "%"));
            }

            //apply pagination here
            var products = await productsQueryable.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Include(p => p.ProductCategories)
                .ToListAsync().ConfigureAwait(false);

            int productCount = productsQueryable.Count();

            AllProductsDto allProductsDto = new AllProductsDto();
            allProductsDto.Count = productCount;
            allProductsDto.Products = _mapper.Map<IEnumerable<Product>, List<ProductDto>>(products);

            _logger.LogInformation("End");

            return allProductsDto;
        }

        public async Task<ProductDto> GetAProductById(int productId)
        {
            _logger.LogInformation("Start");
            var product = await productRepository.GetByIdAsync(productId);

            ProductDto productDto = null;

            if (product != null)
            {
                productDto = _mapper.Map<Product, ProductDto>(product);                
            }

            _logger.LogInformation("End");
            return productDto;

        }

        public async Task<ProductDto> DeleteProduct(int productId)
        {
            _logger.LogInformation("Start");
            var product = await productRepository.GetByIdAsync(productId);            

            ProductDto productDto = null;

            if (product != null)
            {
                productDto = _mapper.Map<Product, ProductDto>(product);
                await productRepository.Delete(product);
                await productRepository.SaveAsyc();
            }

            _logger.LogInformation("End");
            return productDto;

        }

       public async Task<OperationResult<ProductDto>> UpdateProductByPatch(int productId, JsonPatchDocument<Product> patchDoc)
        {
            _logger.LogInformation("Start");
            Message message;

            var product = await productRepository.GetByIdAsync(productId);

            if(product == null)
            {
                message = new Message(Constants.NotFound, "Product not found");
                return new OperationResult<ProductDto>(null, false, message);
            }

            patchDoc.ApplyTo(product);

            await productRepository.UpdateAsync(product);
            await productRepository.SaveAsyc();

            ProductDto productDto = _mapper.Map<Product, ProductDto>(product);
            message = new Message(Constants.Ok, "Product updated Successfully");

            _logger.LogInformation("End");
            return new OperationResult<ProductDto>(productDto, true, message);

        }

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            _logger.LogInformation("Start");

            var product = _mapper.Map<ProductDto, Product>(productDto);
            product.CreatedOn = product.ModifiedOn = DateTimeOffset.UtcNow;
            await productRepository.CreateAsync(product);
            await productRepository.SaveAsyc();

            _logger.LogInformation("End");
            return productDto;

        }

        public async Task<OperationResult<ProductDto>> UpdateProduct(int productId, ProductDto productDto)
        {
            _logger.LogInformation("Start");
            Message message;

            var product = await productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                message = new Message(Constants.NotFound, "Product not found");
                return new OperationResult<ProductDto>(null, false, message);
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.ImageUrl = productDto.ImageUrl;
            product.ModifiedOn = DateTimeOffset.UtcNow;

            await productRepository.UpdateAsync(product);
            await productRepository.SaveAsyc();

            ProductDto productDtoCreated = _mapper.Map<Product, ProductDto>(product);
            message = new Message(Constants.Ok, "Product updated Successfully");

            _logger.LogInformation("End");
            return new OperationResult<ProductDto>(productDtoCreated, true, message);

        }
    }
        
}
