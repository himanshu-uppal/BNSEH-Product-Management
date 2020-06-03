using ProductManager.Dto.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public double SalePrice { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }

        public ProductDto()
        {
            this.Categories = new HashSet<CategoryDto>();
        }

    }
}
