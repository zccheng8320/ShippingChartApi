using DAL.Abstractions;
using DAL.Implementations.DataBase;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace DAL.Implementations.SqlLiteRepositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Db _db;

        protected Repository(Db db)
        {
            DbSet = db.Set<T>();
            _db = db;
        }

        protected DbSet<T> DbSet { get; }


        public virtual async Task InsertAsync(T entity, CancellationToken ctx)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await DbSet.AddAsync(entity, ctx);
            await _db.SaveChangesAsync(ctx);

        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filterExpression, CancellationToken ctx)
        {
            DbSet.RemoveRange(DbSet.Where(filterExpression));
            await _db.SaveChangesAsync(ctx);
        }
    }
}