using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Dto.Product
{
    public class AllProductsDto
    {
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int Count { get; set; }
    }
}
