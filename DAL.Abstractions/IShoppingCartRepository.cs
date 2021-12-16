using System.Linq.Expressions;
using Models;

namespace DAL.Abstractions;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    Task<ShoppingCart?> GetFirstOrDefaultAsync(Expression<Func<ShoppingCart, bool>> expressionFilter, bool isIncludeDetails, CancellationToken ctx);

}