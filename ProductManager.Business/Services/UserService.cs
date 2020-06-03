using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManager.Business.Services.Interface;
using ProductManager.Common;
using ProductManager.Common.ValueObjects;
using ProductManager.DataAccess.Models;
using ProductManager.DataAccess.Repositories.Interface;
using ProductManager.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Business.Services
{
    public class UserService :IUserService
    {
        // variable to hold all users fetched as Entity Repository
        private readonly IUserRepository userRepository;

        private readonly IRoleRepository roleRepository;


        //variable to refer to crypto service
        private readonly ICryptoService cryptoService;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            ICryptoService cryptoService, IMapper mapper, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;

            this.cryptoService = cryptoService;

            _mapper = mapper;

        }

        /// <summary>
        /// this function checks whether the user is a valid user or not
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>returns valid user context with principal and user details</returns>
        public async Task<User> ValidateUser(string username, string password)
        {
            //getting the user using the email given
            var user = await userRepository.Filter(u=>u.Username == username).Include(u=>u.Role).FirstOrDefaultAsync(); 

            //if user is null
            if (user == null)
            {
                //returns empty user context
                //return userContext;
                return null;
            }

            //validating password and if valid then
            if (isPasswordValid(user, password))
            {
                return user;
            }

            //return userContext;
            return null;
        }


        /// <summary>
        /// main create user which is called by every other create user method
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>        
        /// <returns>return object of Operation Result containing true/false and the user created</returns>
        public async Task<OperationResult<UserDto>> CreateUser(User user, string password)
        {
            Message message;
            //getting all users having the same username
            var existingUser =await userRepository.ContainAsync(u => u.Email == user.Email);

            var userRole = await roleRepository.Find(r => r.Name == Constants.USER_ROLE_NAME);

            //if email alraedy exists in the database
            if (existingUser)
            {
                message = new Message(Constants.Conflict, "Email already exists");
                return new OperationResult<UserDto>(null,false,message);
            }

            //creating salt for generating the password
            var passwordSalt = cryptoService.GenerateSalt();

            //creating user object using User model
            var newUser = new User()
            {               
                FirstName = user.FirstName,
                LastName = user.LastName,
                Salt = passwordSalt,
                Email = user.Email,
                Username = user.Username,
                Role = userRole,
                Password = cryptoService.EncryptPassword(password, passwordSalt),               
                CreatedOn = DateTimeOffset.UtcNow,
                ModifiedOn = DateTimeOffset.UtcNow
            };

            //adding newly created user to repository
            await userRepository.CreateAsync(newUser);

            //saving the user repository
           await userRepository.SaveAsyc();

            message = new Message(Constants.Ok, "User created");
            UserDto userDto = _mapper.Map<User, UserDto>(newUser);
            return new OperationResult<UserDto>(userDto, true, message);

           
        }





     


        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }
      

        /// <summary>
        /// to get the user using user key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>user model object </returns>
        public async Task<UserDto> GetUser(int Key)
        {
            var user = await userRepository.GetByIdAsync(Key);

            UserDto userDto = _mapper.Map<User, UserDto>(user);

            return userDto;
        }
      

 

        // Private helpers



        /// <summary>
        /// to validate the password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>true if password is valid , else false</returns>
        private bool isPasswordValid(User user, string password)
        {
            //encrypting password using salt of the user
            var userEnteredPasswordHash = cryptoService.EncryptPassword(password, user.Salt);

            //comparing hash created with given password and hash stored in user database
            return string.Equals(userEnteredPasswordHash, user.Password);
        }





        /// <summary>
        /// validating the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>true if user is valid, else false</returns>
        private bool isUserValid(User user, string password)
        {
            //calling isPasswordValid method
            if (isPasswordValid(user, password))
            {
                return true;
            }
            return false;
        }

    }
}
