using Models;

namespace DAL.Abstractions;

public interface IShoppingCartDetailRepository : IRepository<ShoppingCartDetail>
{

    Task InsertIfNotExistsAsync(ShoppingCartDetail shoppingCartDetail, CancellationToken ctx);
    Task UpdateOrInsertAsync(ShoppingCartDetail shoppingCartDetail, CancellationToken ctx);
}