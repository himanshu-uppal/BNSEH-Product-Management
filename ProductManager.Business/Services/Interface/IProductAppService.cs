using ProductManager.DataAccess.Models;
using ProductManager.Dto.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Business.Services.Interface
{
    public interface IProductAppService
    {
        Task<AllProductsDto> GetAllProducts(string productName, int pageNumber, int pageSize);

        Task<ProductDto> GetAProductById(int productId);

        Task<ProductDto> DeleteProduct(int productId);
    }
}
