using ProductManager.DataAccess.Entity;
using ProductManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.DataAccess.Repositories.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
