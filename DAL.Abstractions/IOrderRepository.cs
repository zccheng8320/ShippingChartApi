using Models;
using System.Linq.Expressions;

namespace DAL.Abstractions;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order,bool>> filter,CancellationToken ctx);
}