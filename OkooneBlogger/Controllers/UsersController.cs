using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Helpers;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OkooneBlogger.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private int GetAuthIdFromSession
        {
            get
            {
                int.TryParse(HttpContext.Session.GetString(OkooneConstants.AUTH_ID), out var authId);
                return authId;
            }
        }

        private IEnumerable<User> GetUsers(string q)
        {
            return !string.IsNullOrEmpty(q)
                            ? _userRepository.Find
                            (a =>
                                a.FullName.ToLower().Contains(q.ToLower()) ||
                                a.Email.ToLower().Contains(q.ToLower()) ||
                                a.Username.ToLower().Contains(q.ToLower())
                            )
                            : _userRepository.GetAllWithArticles();
        }

        private static IEnumerable<User> SortUsers(string sort, IEnumerable<User> users)
        {
            if (!string.IsNullOrEmpty(sort) && (sort.ToLower().Equals("desc") || sort.ToLower().Equals("descending")))
            {
                users = users.OrderByDescending(a => a.Id);
            }

            return users;
        }

        [HttpGet]
        public IActionResult Index(string q, string sort)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            var users = GetUsers(q);
            users = SortUsers(sort, users);

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Username, Email, Password, FullName, Id, Date")] User user)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            try
            {
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
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            var user = _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Username, FullName, Email, Password, Id")] User user)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

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