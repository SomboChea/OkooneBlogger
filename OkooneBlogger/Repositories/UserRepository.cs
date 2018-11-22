using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using OkooneBlogger.Data;
using OkooneBlogger.Repositories.Interfaces;
using User = OkooneBlogger.Models.User;

namespace OkooneBlogger.Repositories
{
    public class UserRepository : OkooneRepository<User>, IUserRepository
    {
        public UserRepository(OkooneDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllWithArticles() => 
            Context.Users
                .Include(a => a.Articles)
                .ToList();

        public IEnumerable<User> FindWithArticles(Func<User, bool> predicate) => 
            Context.Users.Include(a => a.Articles).Where(predicate);

    }
}
