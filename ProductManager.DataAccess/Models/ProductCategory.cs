using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class ProductCategory : EntityBase
    {
        [Required(ErrorMessage = "Please provide the Product")]
        public virtual Product Product { get; set; }
        [Required(ErrorMessage = "Please provide the Category")]
        public virtual Category Category { get; set; }

    }
}
