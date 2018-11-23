using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Helpers;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories.Interfaces;

namespace OkooneBlogger.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;

        public ArticlesController(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            var articles = _articleRepository.GetAllWithAuthor();
            return View(articles);
        }

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            var users = _userRepository.GetAll();
            ViewData["users"] = users;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description, AuthorId, Date")]
            Article article)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            if (!ModelState.IsValid) return NotFound();

            try
            {
                _articleRepository.AddAndSaved(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            var article = _articleRepository.GetById(id);
            return View(article);
        }

        public IActionResult Delete()
        {
            return null;
        }

        public IActionResult Details()
        {
            return null;
        }
    }
}