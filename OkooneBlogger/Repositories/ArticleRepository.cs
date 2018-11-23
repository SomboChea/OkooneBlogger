using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OkooneBlogger.Data;
using OkooneBlogger.Repositories.Interfaces;
using Article = OkooneBlogger.Models.Article;

namespace OkooneBlogger.Repositories
{
    public class ArticleRepository : OkooneRepository<Article>, IArticleRepository
    {
        public ArticleRepository(OkooneDbContext context) : base(context)
        {
        }

        public IEnumerable<Article> GetAllByAuthorId(int id) =>
            Context.Articles
                .Where(a => a.AuthorId == id)
                .Include(a => a.Author)
                .ToList();

        public Article GetByIdWithAuthor(int id) =>
            Context.Articles
                .Where(a => a.Id == id)
                .Include(a => a.Author)
                .FirstOrDefault();

        public IEnumerable<Article> FindArticles(Func<Article, bool> predicate) => 
            Context.Articles
                .Where(predicate)
                .ToList();

        public IEnumerable<Article> FindWithAuthor(Func<Article, bool> predicate) =>
            Context.Articles
                .Include(a => a.Author)
                .Where(predicate)
                .ToList();

        public IEnumerable<Article> GetAllWithAuthor() => 
            Context.Articles
                .Include(a => a.Author)
                .ToList();
    }
}
