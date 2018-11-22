using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using OkooneBlogger.Data;
using OkooneBlogger.Repositories.Interfaces;

namespace OkooneBlogger.Repositories
{
    public abstract class Repository<T> : DbContext, IRepository<T> where T : class
    {
        protected readonly OkooneDbContext Context;

        protected Repository(OkooneDbContext context) => Context = context;

        public IEnumerable<T> GetAll() => Context.Set<T>();

        public T GetById(object id) => Context.Set<T>().Find(id);

        public IEnumerable<T> Find(Func<T, bool> predicate) => Context.Set<T>().Where(predicate);

        public void Add(T entity) => Context.Add(entity);

        public void Add(T entity, out int saved)
        {
            Add(entity);
            saved = Save();
        }

        public bool AddAndSaved(T entity)
        {
            Add(entity);
            return ToBool(Save());
        }

        public void Update(T entity) => Context.Entry(entity).State = EntityState.Modified;

        public void Update(T entity, out int saved)
        {
            Update(entity);
            saved = Save();
        }

        public bool UpdateAndSaved(T entity)
        {
            Update(entity);
            return ToBool(Save());
        }

        public void Delete(T entity) => Context.Entry(entity).State = EntityState.Deleted;

        public void Delete(T entity, out int saved)
        {
            Delete(entity);
            saved = Save();
        }

        public bool DeleteAndSaved(T entity)
        {
            Delete(entity);
            return ToBool(Save());
        }

        public int Save() => Context.SaveChanges();
        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }

        public int Count() => Context.Set<T>().Count();

        public abstract void SoftDelete(T entity);
        public abstract void SoftDelete(T entity, out int saved);
        public abstract bool SoftDeleteAndSaved(T entity);

        private bool ToBool(object obj) => Convert.ToBoolean(obj);
    }
}
