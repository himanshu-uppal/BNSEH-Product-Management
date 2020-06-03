using Microsoft.EntityFrameworkCore;
using ProductManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.DataAccess.Data.DBContext
{
    public class ProductManagerDbContext : DbContext
    {
        public ProductManagerDbContext(DbContextOptions options) : base(options)
        {

        }

        //entities
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
