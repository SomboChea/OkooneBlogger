using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories.Interfaces;

namespace OkooneBlogger.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Username, Password")] LoginViewModel login)
        {
            var user = _userRepository.Find(u => ((u.Username == login.Username) && (u.Password == login.Password)));

            return user.Any() ? Content("Ok") : Content("Fail");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Username, Email, Password, ConfirmPassword")] RegisterViewModel register)
        {
            var user = new User
            {
                Username = register.Username,
                Email = register.Email,
                Password = register.Password,
                Date = DateTime.Now
            };

            var result = false;

            if (register.Password == register.ConfirmPassword)
                result = _userRepository.AddAndSaved(user);

            return result ? Content("Ok") : Content("Fail");
        }
    }
}