using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories.Interfaces;

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
        public IEnumerable<Article> Get(string date)
        {
            if(string.IsNullOrEmpty(date))
                return _articleRepository.GetAllWithAuthor();

            return _articleRepository.FindWithAuthor(a =>
                (a.Date < Convert.ToDateTime(date)));
        }

        [HttpGet("find")]
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
