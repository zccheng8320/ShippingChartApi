using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DAL.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T entity, CancellationToken ctx);
        Task DeleteAsync(Expression<Func<T, bool>> filterExpression, CancellationToken ctx);
    }
}
