using OkooneBlogger.Data;
using System;

namespace OkooneBlogger.Repositories
{
    public class OkooneRepository<T> : Repository<T> where T : class
    {
        public OkooneRepository(OkooneDbContext context) : base(context)
        {
        }

        public override void SoftDelete(T entity) => Delete(entity);

        public override void SoftDelete(T entity, out int saved)
        {
            SoftDelete(entity);
            saved = Save();
        }

        public override bool SoftDeleteAndSaved(T entity)
        {
            SoftDelete(entity);
            return Convert.ToBoolean(Save());
        }
    }
}