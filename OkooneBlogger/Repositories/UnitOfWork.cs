using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OkooneBlogger.Data;
using OkooneBlogger.Repositories.Interfaces;

namespace OkooneBlogger.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OkooneDbContext _context;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(OkooneDbContext context)
        {
            _context = context;
            _articleRepository = new ArticleRepository(context);
            _userRepository = new UserRepository(context);
        }

        public IArticleRepository GetArticleRepository() => _articleRepository;

        public IUserRepository GetUserRepository() => _userRepository;

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
