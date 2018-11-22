using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var articles = _articleRepository.GetAllWithAuthor();
            return View(articles);
        }

        public IActionResult Create()
        {
            var users = _userRepository.GetAll();
            ViewData["users"] = users;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description, AuthorId, Date")]
            Article article)
        {
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