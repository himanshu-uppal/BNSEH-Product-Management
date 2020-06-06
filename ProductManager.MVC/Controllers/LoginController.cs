using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services;
using ProductManager.Business.Services.Interface;
using ProductManager.DataAccess.Models;
using ProductManager.Dto.User;
using ProductManager.MVC.ActionFilterFeature;
using ProductManager.MVC.Models;

namespace ProductManager.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        private readonly ILogger<LoginController> _logger;

        private readonly IMapper _mapper;

        public LoginController(IUserService userService, ILogger<LoginController> logger, IMapper mapper) : base()
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }           

            var user = await _userService.ValidateUser(loginModel.Username, loginModel.Password);
            if (user != null)
            {
                UserDto userDto = _mapper.Map<User, UserDto>(user);
                userDto.Token = TokenManager.GenerateToken(user);
                HttpContext.Session.SetString("userToken", userDto.Token);
                HttpContext.Session.SetInt32("userId", userDto.Id);
                HttpContext.Session.SetInt32("isLoggedIn",1);

                return RedirectToAction(nameof(HomeController.Index), "Home");
                               
            }
            ModelState.TryAddModelError("Error", "Username or password is incorrect");
            return View(loginModel);
            
        }

        [AccessTokenActionFilter]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}