﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
    }
}