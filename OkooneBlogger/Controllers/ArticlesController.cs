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

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
            
        }

        public IActionResult Index(string q, string sort)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(OkooneConstants.AUTH_ID)))
                return RedirectToAction("Login", "Authentication");

            IEnumerable<Article> articles = null;

            if (!string.IsNullOrEmpty(q))
            {
                q = q.ToLower();
                articles = _articleRepository.FindWithAuthor(a =>
                    (a.Title.ToLower().Contains(q) || a.Description.ToLower().Contains(q)));
            }
            else
            {
                articles = _articleRepository.GetAllWithAuthor();
            }


            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToLower().Equals("desc") || sort.ToLower().Equals("descending"))
                {
                    articles = articles.OrderByDescending(a => a.Title).ThenByDescending(a => a.Description);
                }

                if (sort.ToLower().Equals("asc") || sort.ToLower().Equals("ascending"))
                {
                    articles = articles.OrderBy(a => a.Title).ThenBy(a => a.Description);
                }
            }
            
            return View(articles.Where(a => a.AuthorId == int.Parse(HttpContext.Session.GetString(OkooneConstants.AUTH_ID))));
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