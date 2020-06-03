using ProductManager.Common.ValueObjects;
using ProductManager.DataAccess.Models;
using ProductManager.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Business.Services.Interface
{
    public interface IUserService
    {
        Task<User> ValidateUser(string username, string password);

        Task<OperationResult<UserDto>> CreateUser(User user, string password);

        Task<UserDto> GetUser(int Key);   


        IEnumerable<User> GetUsers();
     
     

    }
}
