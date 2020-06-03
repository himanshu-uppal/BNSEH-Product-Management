using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public double SalePrice
        {
            get { return Price + 0.1* Price; }
            set { }
        }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public Product()
        {
            this.ProductCategories = new HashSet<ProductCategory>();
        }
    }
}
