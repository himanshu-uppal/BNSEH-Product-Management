using Microsoft.Extensions.DependencyInjection;
using ProductManager.Business.Services;
using ProductManager.Business.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Business.Extensions
{
    public static class BusinessServiceExtension
    {

        public static void RegisterBusinessServices(this IServiceCollection services)
        {           
            RegisterAppServices(services);
        }

        /// <summary>
        /// Registers the Application Services.
        /// </summary>
        /// <param name="service">The service collection.</param>
        private static void RegisterAppServices(this IServiceCollection service)
        {
            service.AddScoped<IProductAppService, ProductAppService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<ICryptoService, CryptoService>();

        }

    }

}
