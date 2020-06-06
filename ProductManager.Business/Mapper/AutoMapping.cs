using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ProductManager.DataAccess.Models;
using ProductManager.Dto.Product;
using ProductManager.Dto.User;
using ProductManager.Dto.Role;
using ProductManager.Dto.Request.User;
using ProductManager.Dto.Category;

namespace ProductManager.Business.Mapper
{
   
    public class AutoMapping : Profile
    {
        public AutoMapping() : base("AutoMapping")
        {
            CreateMap<ProductDto, Product>().ReverseMap(); // means you want to map from Product to ProductDto, vice versa
            CreateMap<UserDto, User>();
            CreateMap<RoleDto, Role>().ReverseMap();
            CreateMap<User,UserRegisterModel>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            //  .ForMember(userDto => userDto.Role, userRole => userRole.MapFrom(user => user.Role.Name));
            CreateMap<Category, CategoryDto>();
            //CreateMap<Product, ProductDto>();
            //.AfterMap((product, productDto) =>
            // {
            //     foreach (var category in product.ProductCategories)
            //     {
            //         CategoryDto categoryDto = new CategoryDto();
            //         categoryDto.Name = category.Category.Name;
            //         productDto.Categories.Add(categoryDto);
            //     }
            // });

           // CreateMap<RegisterViewModel,User>().ReverseMap();
        }
    }
}
