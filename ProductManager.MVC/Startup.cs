using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProductManager.Business.Extensions;
using ProductManager.Business.Mapper;
using ProductManager.Common;
using ProductManager.DataAccess.Data.DBContext;
using ProductManager.DataAccess.Extensions;
using ProductManager.MVC.Mapping;

namespace ProductManager.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();


            services.AddDbContext<ProductManagerDbContext>(options =>
        options.UseMySql(Configuration.GetConnectionString(Constants.ProductManagerDBConnection)));

             services.RegisterDataServices();
            services.RegisterBusinessServices();

            services.AddAutoMapper(typeof(Startup));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
                mc.AddProfile(new AutoMappingMVC());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            // -------------- dot net core authorization try start


            services.AddMvc();
            //// Other code


            // -------------- dot net core authorization try end

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.ADMIN_ROLE_NAME, policy => policy.RequireClaim(ClaimTypes.Role, Constants.ADMIN_ROLE_NAME));
            });

            var key = Encoding.ASCII.GetBytes(Constants.SECRET);
        services.AddAuthentication(x =>
         {
             x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
        .AddJwtBearer(x =>
        {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        //x.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateIssuerSigningKey = true,
        //    ValidateIssuer = false,
        //    IssuerSigningKey = new SymmetricSecurityKey(key),
        //    ValidateAudience = false
        //};
    });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
           
            app.UseSession();
           // app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
