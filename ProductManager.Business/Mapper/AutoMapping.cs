using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ProductManager.DataAccess.Models;
using ProductManager.Dto.Product;
using ProductManager.Dto.User;
using ProductManager.Dto.Role;
using ProductManager.Dto.Request.User;

namespace ProductManager.Business.Mapper
{
   
    public class AutoMapping : Profile
    {
        public AutoMapping() : base("AutoMapping")
        {
            CreateMap<Product, ProductDto>().ReverseMap(); // means you want to map from Product to ProductDto, vice versa
            CreateMap<UserDto, User>();
            CreateMap<RoleDto, Role>().ReverseMap();
            CreateMap<User,UserRegisterModel>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
              //  .ForMember(userDto => userDto.Role, userRole => userRole.MapFrom(user => user.Role.Name));
        }
    }
}
