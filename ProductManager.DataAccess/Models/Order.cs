using ProductManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManager.DataAccess.Models
{
    public class Order : EntityBase
    {
        public double OrderTotalPrice { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        [Required(ErrorMessage = "Please provide the User")]
        public virtual User User { get; set; }

        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }
    }
}
