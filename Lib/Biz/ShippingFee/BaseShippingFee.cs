using Lib.Biz.ShippingFee.Interfaces;
using Models;

namespace Lib.Biz.ShippingFee;

internal class BaseShippingFee : IShippingFee
{
    private readonly int _baseShippingFee;
    private readonly int _freeOfShippingFeeThreshold;

    public BaseShippingFee(int baseShippingFee,int freeOfShippingFeeThreshold)
    {
        _baseShippingFee = baseShippingFee;
        _freeOfShippingFeeThreshold = freeOfShippingFeeThreshold;
    }
    /// <summary>
    /// 金額大於1000 則免運
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Calculate(Order order)
    {
        if (order.CheckoutAmount >= _freeOfShippingFeeThreshold)
            return 0;
        return _baseShippingFee;
    }
}