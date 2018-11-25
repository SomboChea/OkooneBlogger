using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Data;
using OkooneBlogger.Repositories;
using OkooneBlogger.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Article = OkooneBlogger.Models.Article;
using User = OkooneBlogger.Models.User;

namespace OkooneBlogger.Api.Controllers
{
    [Route("api/v2")]
    [ApiController]
    public class UnitOfWorkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkController(OkooneDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        private static IEnumerable<Article> SortArticles(IEnumerable<Article> articles, string sort)
        {
            articles = sort.ToLower().Equals("desc") ? articles.OrderByDescending(a => a.Id).ToList() : articles.OrderBy(a => a.Id).ToList();
            return articles;
        }

        [HttpGet("articles")]
        public IEnumerable<Article> GetArticles(string search, string sort)
        {
            var articles = _unitOfWork.GetArticleRepository().GetAllWithAuthor();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                articles = _unitOfWork.GetArticleRepository()
                    .FindWithAuthor(article =>
                        article.Title.ToLower().Contains(search) ||
                        article.Description.ToLower().Contains(search) ||
                        article.Author.Username.ToLower().Contains(search) ||
                        article.Author.Email.ToLower().Contains(search) ||
                        article.Author.FullName.ToLower().Contains(search)
                    );
            }

            articles = SortArticles(articles, sort);
            return articles;
        }

        [HttpPost("articles")]
        public IActionResult PostArticle([Bind("Title, Description, Date")] Article article)
        {
            if (!ModelState.IsValid) return Content("Error to post data to server!");

            _unitOfWork.GetArticleRepository().Add(article);
            _unitOfWork.Complete();
            return Content("Created");
        }

        [HttpGet("users")]
        public IEnumerable<User> GetUsers()
        {
            return _unitOfWork.GetUserRepository().GetAllWithArticles();
        }
    }
}