using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkooneBlogger.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        IEnumerable<T> Find(Func<T, bool> predicate);

        void Add(T entity);

        void Add(T entity, out int saved);

        bool AddAndSaved(T entity);

        void Update(T entity);

        void Update(T entity, out int saved);

        bool UpdateAndSaved(T entity);

        void Delete(T entity);

        void Delete(T entity, out int saved);

        bool DeleteAndSaved(T entity);

        int Save();

        Task<int> SaveAsync();

        int Count();

        void SoftDelete(T entity);

        void SoftDelete(T entity, out int saved);

        bool SoftDeleteAndSaved(T entity);
    }
}