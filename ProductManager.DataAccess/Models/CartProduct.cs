﻿using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class CartProduct : EntityBase
    {
        public virtual Cart Cart { get; set; }
        [Required(ErrorMessage = "Please provide the Product")]
        public virtual Product Product { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive count")]
        public int ProductCount { get; set; }
    }
}
