using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkooneBlogger.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository GetArticleRepository();

        IUserRepository GetUserRepository();
        int Complete();
        Task<int> CompleteAsync();
    }
}
