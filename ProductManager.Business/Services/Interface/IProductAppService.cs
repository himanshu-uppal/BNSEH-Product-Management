using ProductManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Business.Services.Interface
{
    public interface IProductAppService
    {
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
