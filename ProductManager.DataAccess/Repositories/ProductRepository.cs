using ProductManager.DataAccess.Data;
using ProductManager.DataAccess.Data.DBContext;
using ProductManager.DataAccess.Models;
using ProductManager.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        public ProductRepository(ProductManagerDbContext context) : base(context)
        {
        }
    }

}
