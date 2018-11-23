using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using OkooneBlogger.Helpers;
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

        private void InitSession()
        {
            HttpContext.Session.SetString(OkooneConstants.AUTH_ID, "");
            HttpContext.Session.SetString(OkooneConstants.AUTH_USERNAME, "");
        }

        private void SetSession(int authId, string authUsername)
        {
            HttpContext.Session.SetString(OkooneConstants.AUTH_ID, authId + "");
            HttpContext.Session.SetString(OkooneConstants.AUTH_USERNAME, authUsername);
        }

        private void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        private string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        public IActionResult Login()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
            {
                InitSession();
                return View();
            }

            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Username, Password")] LoginViewModel login)
        {
            var user = _userRepository.Find(u => ((u.Username == login.Username) && (u.Password == login.Password))).FirstOrDefault();
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
            {
                if (user != null && !string.IsNullOrEmpty(user.Username))
                {
                    SetSession(user.Id, user.Username);

                    return RedirectToAction("Admin", "Home");
                }

                return Content("Has Logged In");
            }

            return Content("Fail");
        }

        public IActionResult Register()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return View();

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Username, Email, Password, ConfirmPassword")] RegisterViewModel register)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return Content("Register failed, has Logged in");

            var checkUsername = _userRepository.Find(u => (u.Username == register.Username)).FirstOrDefault();

            if (checkUsername != null && !string.IsNullOrEmpty(checkUsername.Username))
                return Content("Failed, user already exist!");

;            var user = new User
            {
                Username = register.Username,
                Email = register.Email,
                Password = register.Password,
                Date = DateTime.Now
            };

            _userRepository.AddAndSaved(user);
            SetSession(user.Id, user.Username);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            InitSession();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

    }
}