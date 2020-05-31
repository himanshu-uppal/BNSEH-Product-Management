using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManager.DataAccess.Data;
using ProductManager.DataAccess.Data.DBContext;
using ProductManager.DataAccess.Entity;
using ProductManager.DataAccess.Repositories;
using ProductManager.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.DataAccess.Extensions
{
    public static class DataModelExtension
    {
        public static void RegisterDataServices(this IServiceCollection services)
        {           
            services.AddScoped<DbContext, ProductManagerDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
