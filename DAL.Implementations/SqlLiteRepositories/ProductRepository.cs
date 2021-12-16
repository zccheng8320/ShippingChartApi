using DAL.Abstractions;
using DAL.Implementations.DataBase;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace DAL.Implementations.SqlLiteRepositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly Db _db;

    public ProductRepository(Db db) : base(db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> filter, CancellationToken ctx)
    {
        return await _db.Products.Where(filter).ToListAsync(ctx);
    }
}