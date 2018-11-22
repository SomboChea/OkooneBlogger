using System;
using System.Collections.Generic;
using OkooneBlogger.Models;

namespace OkooneBlogger.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetAllWithAuthor();
        IEnumerable<Article> FindArticles(Func<Article, bool> predicate);
        IEnumerable<Article> FindWithAuthor(Func<Article, bool> predicate);
    }
}
