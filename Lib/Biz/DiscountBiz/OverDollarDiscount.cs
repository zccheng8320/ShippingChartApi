using Lib.Biz.DiscountBiz.Interfaces;
using Models;

namespace Lib.Biz.DiscountBiz;

/// <summary>
/// 訂單滿N元折M元
/// </summary>
internal class OverDollarDiscount : IDiscountStrategy
{
    private readonly decimal _overAmount;
    private readonly decimal _discountOffPercent;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="overAmount">超過多少金額</param>
    /// <param name="discountOffPercent">打幾折(百分比)</param>
    public OverDollarDiscount(decimal overAmount, int discountOffPercent)
    {
        _overAmount = overAmount;
        _discountOffPercent = ((decimal)discountOffPercent) / 100;
    }

    public decimal CalculateDiscount(Order order)
    {

        if (order.CheckoutAmount >= _overAmount)
            return order.CheckoutAmount - (order.CheckoutAmount * _discountOffPercent);
        return 0;
    }

}