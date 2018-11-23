using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
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

        public IEnumerable<Article> FindArticles(Func<Article, bool> predicate) => Find(predicate);

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
