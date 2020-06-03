using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "Please provide the Category name")]
        [MaxLength(20)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive count")]
        public int ProductCountSold { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public Category()
        {
            this.ProductCategories = new HashSet<ProductCategory>();
        }

    }
}
