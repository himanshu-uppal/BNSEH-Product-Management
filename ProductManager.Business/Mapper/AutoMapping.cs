using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ProductManager.DataAccess.Models;
using ProductManager.Dto.Product;

namespace ProductManager.Business.Mapper
{
   
    public class AutoMapping : Profile
    {
        public AutoMapping() : base("AutoMapping")
        {
            CreateMap<Product, ProductDto>().ReverseMap(); // means you want to map from Product to ProductDto, vice versa
        }
    }
}
