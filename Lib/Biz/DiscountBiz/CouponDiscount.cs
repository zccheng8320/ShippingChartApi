using Lib.Biz.DiscountBiz.Interfaces;
using Models;

namespace Lib.Biz.DiscountBiz;

internal class CouponDiscount : IDiscountStrategy
{
    private readonly decimal _couponDiscount;

    public CouponDiscount(decimal couponDiscount)
    {
        _couponDiscount = couponDiscount;
    }
    public decimal CalculateDiscount(Order order)
    {
        return _couponDiscount;

    }
}