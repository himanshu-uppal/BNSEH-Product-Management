using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Business.Services;
using ProductManager.Business.Services.Interface;
using ProductManager.Common;
using ProductManager.DataAccess.Models;
using ProductManager.Dto.Request.User;
using ProductManager.Dto.User;

namespace ProductManager.API.Controllers
{
    [Authorize(Policy = Constants.ADMIN_ROLE_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService,IMapper mapper)
        {
            _mapper = mapper;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await userService.ValidateUser(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            UserDto userDto = _mapper.Map<User, UserDto>(user);
            userDto.Token = TokenManager.GenerateToken(user);
            return Ok(userDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userService.GetUser(id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterModel userRegisterModel)
        {

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserRegisterModel, User>(userRegisterModel);

                var createUserResult = await userService.CreateUser(user, userRegisterModel.Password);

                if (createUserResult.IsSuccess)
                {
                    return Ok();

                }
                return Conflict(createUserResult.MainMessage.Text);



            }

            return BadRequest();



        }
    }


}