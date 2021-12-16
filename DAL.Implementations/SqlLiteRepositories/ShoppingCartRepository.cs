using System.Linq.Expressions;
using System.Text;
using DAL.Abstractions;
using DAL.Implementations.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Models;

namespace DAL.Implementations.SqlLiteRepositories;


internal class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly Db _db;

    public ShoppingCartRepository(Db db) : base(db)
    {
        _db = db;
    }

    public Task<ShoppingCart?> GetFirstOrDefaultAsync(Expression<Func<ShoppingCart, bool>> expressionFilter, bool isIncludeDetails, CancellationToken ctx)
    {
        if (!isIncludeDetails)
            return _db.ShoppingCarts.AsNoTracking().FirstOrDefaultAsync(expressionFilter, ctx);

        return _db.ShoppingCarts.AsNoTracking()
            .Include(m => m.ShoppingCartDetails)
            .ThenInclude(m => m.Product)
            .FirstOrDefaultAsync(expressionFilter, ctx);
    }
}