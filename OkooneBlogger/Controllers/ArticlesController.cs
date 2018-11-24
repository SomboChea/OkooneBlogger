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
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        private int GetAuthIdFromSession
        {
            get
            {
                int.TryParse(HttpContext.Session.GetString(OkooneConstants.AUTH_ID), out var authId);
                return authId;
            }
        }

        private IEnumerable<Article> GetArticles(string q)
        {
            return !string.IsNullOrEmpty(q) ?
                            _articleRepository.FindWithAuthor
                            (a =>
                                (a.Title.ToLower().Contains(q.ToLower()) ||
                                 a.Description.ToLower().Contains(q.ToLower()))
                            )
                            : _articleRepository.GetAllWithAuthor();
        }

        private static IEnumerable<Article> SortArticles(string sort, IEnumerable<Article> articles)
        {
            if (sort.ToLower().Equals("desc") || sort.ToLower().Equals("descending"))
                articles = articles.OrderByDescending(a => a.Title).ThenByDescending(a => a.Description);

            if (sort.ToLower().Equals("asc") || sort.ToLower().Equals("ascending"))
                articles = articles.OrderBy(a => a.Title).ThenBy(a => a.Description);
            return articles;
        }

        [HttpGet]
        public IActionResult Index(string q, string sort)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            var articles = GetArticles(q);

            if (string.IsNullOrEmpty(sort)) return View(articles.Where(a => a.AuthorId == GetAuthIdFromSession));
            {
                articles = SortArticles(sort, articles);
            }

            return View(articles.Where(a => a.AuthorId == GetAuthIdFromSession));
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description, Date")]
            Article article)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            try
            {
                article.AuthorId = GetAuthIdFromSession;
                _articleRepository.AddAndSaved(article);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            var article = _articleRepository.GetById(id);
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Title, Description, Date")] Article article)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            try
            {
                article.AuthorId = GetAuthIdFromSession;

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
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

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

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (GetAuthIdFromSession <= 0) return RedirectToAction("Login", "Authentication");

            var article = _articleRepository.GetByIdWithAuthor(id);
            return View(article);
        }
    }
}