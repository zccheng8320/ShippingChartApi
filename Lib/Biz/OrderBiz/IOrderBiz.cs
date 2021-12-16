using Lib.Biz.DiscountBiz.Interfaces;
using Lib.Biz.ShippingFee.Interfaces;
using Models;

namespace Lib.Biz.OrderBiz;

internal interface IOrderBiz
{
    /// <summary>
    /// 設定運費
    /// </summary>
    /// <param name="order"></param>
    void SetShippingFee(Order order);
    /// <summary>
    /// 設定折扣
    /// </summary>
    /// <param name="order"></param>
    void SetDiscount(Order order);
}

internal class OrderBiz : IOrderBiz
{
    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IShippingFee _shippingFee;

    public OrderBiz(IDiscountStrategyFactory discountStrategyFactory,IShippingFee shippingFee)
    {
        _discountStrategyFactory = discountStrategyFactory;
        _shippingFee = shippingFee;
    }

    public void SetShippingFee(Order order)
    {
        var discountStrategy = _discountStrategyFactory.Create(order);

        var discount = discountStrategy.CalculateDiscount(order);
        order.DiscountAmount = discount;
    }

    public void SetDiscount(Order order)
    {
        var shippingFee = _shippingFee.Calculate(order);
        order.ShippingFee = shippingFee;
        return;
    }
}