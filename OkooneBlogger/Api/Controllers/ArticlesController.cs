using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace OkooneBlogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public IEnumerable<Article> Get(string date, string with = "")
        {
            var withAuthors = false;
            if (string.IsNullOrEmpty(date))
            {
                if (with.ToLower().Equals("authors"))
                    withAuthors = true;
            }

            if (withAuthors)
            {
                if (!string.IsNullOrEmpty(date))
                {
                    return _articleRepository.FindWithAuthor(a =>
                        (a.Date < Convert.ToDateTime(date)));
                }

                return _articleRepository.GetAllWithAuthor();
            }

            if (!string.IsNullOrEmpty(date))
            {
                return _articleRepository.Find(a =>
                    (a.Date < Convert.ToDateTime(date)));
            }

            return _articleRepository.GetAll();
        }

        [HttpGet("find/{date}")]
        public IEnumerable<Article> InferiorWithDate(string date)
        {
            return _articleRepository.FindWithAuthor(a => (a.Date < Convert.ToDateTime(date)));
        }

        [HttpGet("{id}")]
        public Article GetArticle(int id)
        {
            return _articleRepository.GetById(id);
        }
    }
}