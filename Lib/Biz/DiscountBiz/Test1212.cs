using Lib.Biz.DiscountBiz.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Lib.Biz.DiscountBiz;

/// <summary>
/// 測試用雙12折扣
/// 先計算滿3000 在看有沒有折價卷
/// </summary>
internal class Test1212 : IDiscountStrategy
{
    private readonly IServiceProvider _serviceProvider;

    public Test1212(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public decimal CalculateDiscount(Order order)
    {
        var overDollarDiscount = _serviceProvider.GetRequiredService<OverDollarDiscount>();
        var totalDiscount = overDollarDiscount.CalculateDiscount(order);
        if (order.HasCoupon)
        {
            var couponDiscount = _serviceProvider.GetRequiredService<CouponDiscount>();
            totalDiscount += couponDiscount.CalculateDiscount(order);
        }

        return totalDiscount;
    }
}