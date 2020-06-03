using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class Cart : EntityBase
    {
        public virtual ICollection<CartProduct> CartProducts { get; set; }

        public virtual User User { get; set; }

        public Cart()
        {
            this.CartProducts = new HashSet<CartProduct>();
        }

    }
}
