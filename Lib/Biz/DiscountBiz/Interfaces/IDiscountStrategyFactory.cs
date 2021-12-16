using Models;

namespace Lib.Biz.DiscountBiz.Interfaces;

internal interface IDiscountStrategyFactory
{
    IDiscountStrategy Create(Order order);
}