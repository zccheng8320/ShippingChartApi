using DAL.Abstractions;
using Lib.Biz.DiscountBiz;
using Lib.Biz.DiscountBiz.Interfaces;
using Lib.Biz.OrderBiz;
using Lib.Biz.ShippingFee;
using Lib.Biz.ShippingFee.Interfaces;
using Lib.Services;
using Lib.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lib
{
    public static class DependencyInjection
    {
        public static void AddLib(this IServiceCollection services)
        {
            services.AddLibServices();
            services.AddBiz();

        }
        /// <summary>
        /// 注入商業邏輯(TODO 可帶參數或設定的方式)
        /// </summary>
        /// <param name="services"></param>
        private static void AddBiz(this IServiceCollection services)
        {
            // 一張優惠卷折抵 100$
            services.AddSingleton(m => new CouponDiscount(100))
                .AddSingleton<IDiscountStrategy, CouponDiscount>(m => m.GetRequiredService<CouponDiscount>());
            // 整筆訂單滿三千打九折(可帶參數)
            services.AddSingleton(m => new OverDollarDiscount(3000, 90))
                .AddSingleton<IDiscountStrategy, OverDollarDiscount>(m => m.GetRequiredService<OverDollarDiscount>());
            // 雙12折扣策略
            services.AddSingleton<Test1212>()
                .AddSingleton<IDiscountStrategy, Test1212>(m=>m.GetRequiredService<Test1212>());

            // 運費計算邏輯(基礎運費60，大於1000則免運)
            services.AddSingleton<IShippingFee, BaseShippingFee>(m=> new BaseShippingFee(60,1000));
            // 折扣策略工廠
            services.AddScoped<IDiscountStrategyFactory, DiscountStrategyFactory>();
            // 訂單商業邏輯
            services.AddScoped<IOrderBiz, OrderBiz>();

        }
        /// <summary>
        /// 注入Lib 服務
        /// </summary>
        /// <param name="services"></param>
        private static void AddLibServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
        }

    }
}