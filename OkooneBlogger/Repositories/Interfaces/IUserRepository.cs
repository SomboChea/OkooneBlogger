using System;
using System.Collections.Generic;
using OkooneBlogger.Models;

namespace OkooneBlogger.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllWithArticles();
        IEnumerable<User> FindWithArticles(Func<User, bool> predicate);
    }
}
