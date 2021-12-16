using System;
using Microsoft.EntityFrameworkCore;
using Models;
namespace DAL.Implementations.DataBase
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(table => new { table.OrderId, table.ProductId });
            modelBuilder.Entity<ShoppingCartDetail>().HasKey(table => new { table.ShoppingCartId, table.ProductId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartDetail> ShoppingCartDetails { get; set; }
    }
}