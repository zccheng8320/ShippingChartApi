using Models;

namespace Lib.Biz.ShippingFee.Interfaces;

internal interface IShippingFee
{

    public int Calculate(Order order);
}