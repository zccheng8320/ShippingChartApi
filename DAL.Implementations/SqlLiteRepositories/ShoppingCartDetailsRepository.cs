using System.Linq.Expressions;
using DAL.Abstractions;
using DAL.Implementations.DataBase;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Implementations.SqlLiteRepositories;

internal class ShoppingCartDetailsRepository : Repository<ShoppingCartDetail>, IShoppingCartDetailRepository
{
    private readonly Db _db;

    public ShoppingCartDetailsRepository(Db db) : base(db)
    {
        _db = db;
    }

    public Task InsertIfNotExistsAsync(ShoppingCartDetail shoppingCartDetail, CancellationToken ctx)
    {
        const string sql = "INSERT INTO ShoppingCartDetails(ShoppingCartId,ProductId,ProductCount)" +
                           "SELECT {0},{1},{2} " +
                           " WHERE NOT EXISTS(SELECT 1 FROM ShoppingCartDetails WHERE ShoppingCartId = {0} AND ProductId = {1});";
        var parameters = new object[] { shoppingCartDetail.ShoppingCartId, shoppingCartDetail.ProductId, 1 };
        return _db.Database.ExecuteSqlRawAsync(sql, parameters, ctx);
    }

    public Task UpdateOrInsertAsync(ShoppingCartDetail shoppingCartDetail, CancellationToken ctx)
    {
        const string sql = "insert or replace into ShoppingCartDetails (ShoppingCartId,ProductId,ProductCount) " +
                           "values ({0},{1},{2});";
        var parameters = new object[] { shoppingCartDetail.ShoppingCartId, shoppingCartDetail.ProductId, shoppingCartDetail.ProductCount };
        return _db.Database.ExecuteSqlRawAsync(sql, parameters, ctx);
    }
}