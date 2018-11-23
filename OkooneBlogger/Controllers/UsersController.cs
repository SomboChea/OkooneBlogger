using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Helpers;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories;
using OkooneBlogger.Repositories.Interfaces;

namespace OkooneBlogger.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index(string q, string sort)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            IEnumerable<User> users = null;

            if (!string.IsNullOrEmpty(q))
            {
                q = q.ToLower();
                users = _userRepository.Find(a =>
                    (a.FullName.ToLower().Contains(q) || a.Email.ToLower().Contains(q) || a.Username.ToLower().Contains(q)));
            }
            else
            {
                users = _userRepository.GetAllWithArticles();

            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToLower().Equals("desc") || sort.ToLower().Equals("descending"))
                {
                    users = users.OrderByDescending(a => a.Id);
                }
            }

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Username, Email, Password, FullName, Id, Date")] User user)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                // if (!ModelState.IsValid) return NotFound();
                _userRepository.AddAndSaved(user);
                
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public IActionResult Edit(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            var user = _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Username, FullName, Email, Password, Id")] User user)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                _userRepository.UpdateAndSaved(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public IActionResult Details(int id)
        {
            return null;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                var user = _userRepository.GetById(id);
                _userRepository.DeleteAndSaved(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}