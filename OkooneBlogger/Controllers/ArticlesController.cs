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

        public IActionResult Index(string q)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            IEnumerable<Article> articles;

            if (!string.IsNullOrEmpty(q))
            {
                q = q.ToLower();
                articles = _articleRepository.FindWithAuthor(a =>
                    (a.Title.ToLower().Contains(q) || a.Description.ToLower().Contains(q)));
                return View(articles);
            }

            articles = _articleRepository.GetAllWithAuthor();
            return View(articles);
        }

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description, Date")]
            Article article)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            //if (!ModelState.IsValid) return Content("Content is not valid!");

            try
            {
                article.AuthorId = int.Parse(HttpContext.Session.GetString(OkooneConstants.AUTH_ID));
                _articleRepository.AddAndSaved(article);
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

            var article = _articleRepository.GetById(id);
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Title, Description, Date")] Article article)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                article.AuthorId = int.Parse(HttpContext.Session.GetString(OkooneConstants.AUTH_ID));

                _articleRepository.UpdateAndSaved(article);

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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            try
            {
                var article = _articleRepository.GetById(id);
                _articleRepository.DeleteAndSaved(article);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public IActionResult Details(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            var article = _articleRepository.GetById(id);
            return View(article);
        }
    }
}