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

        private IEnumerable<Article> GetArticles(string date, bool withAuthors, bool hasDate)
        {
            if (withAuthors)
                return hasDate
                    ? _articleRepository.FindWithAuthor(a => a.Date < Convert.ToDateTime(date))
                    : _articleRepository.GetAllWithAuthor();
            {
                if (hasDate)
                    return _articleRepository.Find(a =>
                        a.Date < Convert.ToDateTime(date));

                return _articleRepository.GetAll();
            }
        }

        private static void CheckParameters(string date, string with, ref bool withAuthors, ref bool hasDate)
        {
            if (string.IsNullOrEmpty(date))
            {
                if (with.ToLower().Equals("authors")) withAuthors = true;
            }
            else
            {
                hasDate = true;
            }
        }

        [HttpGet]
        public IEnumerable<Article> Get(string date, string with = "")
        {
            var withAuthors = false;
            var hasDate = false;
            CheckParameters(date, with, ref withAuthors, ref hasDate);
            return GetArticles(date, withAuthors, hasDate);
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