using OkooneBlogger.Models;
using System;
using System.Collections.Generic;

namespace OkooneBlogger.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllWithArticles();

        IEnumerable<User> FindWithArticles(Func<User, bool> predicate);
    }
}