using DAL.Abstractions;
using DAL.Implementations.DataBase;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace DAL.Implementations.SqlLiteRepositories;

internal class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly Db _db;

    public OrderRepository(Db db) : base(db)
    {
        _db = db;
    }

    /// <summary>
    /// 新增訂單(Order)時，需包含以下步驟
    /// 1. 啟動交易
    /// 2. 逐筆確認是否有足夠的庫存(會先預扣庫存量)
    /// 3. 確認每筆項目的庫存都足夠時，才建立訂單
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ProductStockVolumeNotEnoughException"></exception>
    public override async Task InsertAsync(Order entity, CancellationToken ctx)
    {
        const string updateSalesVolumeSql =
            "update Products set SalesVolume = SalesVolume + {0} where ProductId ={1} and PurchaseCount  >= SalesVolume + {0} ";
        await using var transaction = await _db.Database.BeginTransactionAsync(ctx);
        try
        {
            foreach (var orderDetail in entity.OrderDetails)
            {
               
                var count = orderDetail.ProductCount;
                var parameters = new object[] { count, orderDetail.ProductId };
                var affectRowsCount = await _db.Database.ExecuteSqlRawAsync(updateSalesVolumeSql, parameters, ctx);
                if (affectRowsCount == 0)
                    throw new ProductStockVolumeNotEnoughException($"ProductId : {orderDetail.ProductId}的庫存不足");
                
            }

            await base.InsertAsync(entity, ctx);
            await transaction.CommitAsync(ctx);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ctx);
            throw;
        }

    }

    public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> filter, CancellationToken ctx)
    {
        return await _db.Orders.Where(filter).Include(m => m.OrderDetails).ThenInclude(m => m.Product).ToListAsync(ctx);
    }

}