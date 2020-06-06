using AutoMapper;
using ProductManager.DataAccess.Models;
using ProductManager.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.MVC.Mapping
{
    public class AutoMappingMVC : Profile
    {
        public AutoMappingMVC() : base("AutoMappingMVC")
        {
           

             CreateMap<RegisterViewModel,User>().ReverseMap();
        }
    }
}
