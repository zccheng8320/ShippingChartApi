using System.Linq.Expressions;
using Models;

namespace DAL.Abstractions;

public interface IProductRepository : IRepository<Product>
{
    public Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> filter,CancellationToken ctx);

}

