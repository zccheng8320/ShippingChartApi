using Models;

namespace Lib.Biz.DiscountBiz.Interfaces;

/// <summary>
/// 折價策略
/// </summary>
internal interface IDiscountStrategy
{
    /// <summary>
    /// 取得折扣金額
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    decimal CalculateDiscount(Order order);
}