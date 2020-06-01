﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Entity
{
    public class EntityBase : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}