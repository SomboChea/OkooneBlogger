using System;
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

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            var users = _userRepository.GetAllWithArticles();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Username, Email, Password, FullName, Id, Date")] User user)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                if (!ModelState.IsValid) return NotFound();
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

        // POST: Default/Edit/5
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
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            return null;
        }

        public IActionResult Delete(int id)
        {
            return null;
        }

        // POST: Default/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}