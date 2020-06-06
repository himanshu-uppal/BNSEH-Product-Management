using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.Business.Services.Interface;
using ProductManager.DataAccess.Models;
using ProductManager.MVC.Models;

namespace ProductManager.MVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        private readonly ILogger<RegisterController> _logger;

        private readonly IMapper _mapper;

        public RegisterController(IUserService userService, ILogger<RegisterController> logger, IMapper mapper) : base()
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
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = _mapper.Map<RegisterViewModel, User>(userModel);

            var result = await _userService.CreateUser(user, userModel.Password);
            if (!result.IsSuccess)
            {

                ModelState.TryAddModelError(result.MainMessage.Code, result.MainMessage.Text);


                return View(userModel);
            }

            return RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}