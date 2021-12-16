using Lib.ServiceResults;
using Models;

namespace Lib.Services.Interfaces;

public interface IOrderService
{
    Task<IServiceResult<IEnumerable<Order>>> GetOrderAsync(string phoneNumber, CancellationToken ctx);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="shoppingCart">購物車編號</param>
    /// <param name="phoneNumber">訂購人電話號碼</param>
    /// <param name="hasCoupon">是否使用折價卷</param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    Task<IServiceResult> PlaceOrderAsync(int shoppingCart, string phoneNumber,bool hasCoupon, CancellationToken ctx);
}