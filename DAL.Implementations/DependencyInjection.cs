using System;
using DAL.Abstractions;
using DAL.Implementations.DataBase;
using DAL.Implementations.SqlLiteRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace DAL.Implementations
{
    public static class DependencyInjection
    {
        public static void AddDAL(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartDetailRepository, ShoppingCartDetailsRepository>();
            services.AddDbContextFactory<Db>(optionsAction =>
            {
                // 先使用固定路徑
                optionsAction.UseSqlite($"Data Source=db.db");
            });

        }
        /// <summary>
        /// 啟動時，會自動Create or Migrate 資料庫(For Test)
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void MigrateDataBaseAndAddTestData(this IServiceProvider serviceProvider)
        {
            var factory = serviceProvider.GetRequiredService<IDbContextFactory<Db>>();
            var db = factory.CreateDbContext();
            db.Database.Migrate();
            AddDefaultTestData(db);
        }

        private static void AddDefaultTestData(Db db)
        {
            // 有資料代表有匯入預設資料了
            if (db.Products.Any())
                return;
            var product1 = new Product()
            {
                Name = "冷凍迷你可頌 ",
                PurchaseCount = 100,
                SalesVolume = 0,
                Price = 110,
                ProductNo = "AB01"
            };
            var product2 = new Product()
            {
                Name = "白熊軟性洗碗精 4kg ",
                PurchaseCount = 50,
                SalesVolume = 0,
                Price = 120,
                ProductNo = "BC01"
            };

            var product3 = new Product()
            {
                Name = "海苔沙其馬 ",
                PurchaseCount = 200,
                SalesVolume = 0,
                Price = 45,
                ProductNo = "DF01"
            };

            var product4 = new Product()
            {
                Name = "薑黃麵線 ",
                PurchaseCount = 30,
                SalesVolume = 0,
                Price = 105,
                ProductNo = "IP01"
            };

            var product5 = new Product()
            {
                Name = "花王去屑洗髮精 ",
                PurchaseCount = 150 ,
                SalesVolume = 0,
                Price = 150,
                ProductNo = "CG01"
            };

            db.Products.Add(product1);
            db.Products.Add(product2);
            db.Products.Add(product3);
            db.Products.Add(product4);
            db.Products.Add(product5);
            db.SaveChanges();
        }
    }
}