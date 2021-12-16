using Lib.Biz.DiscountBiz.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Lib.Biz.DiscountBiz;

internal class DiscountStrategyFactory : IDiscountStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DiscountStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IDiscountStrategy Create(Order order)
    {
        return _serviceProvider.GetRequiredService<Test1212>();
    }
}