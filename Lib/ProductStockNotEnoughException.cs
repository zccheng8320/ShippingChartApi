namespace Lib;

public class ProductStockNotEnoughException : Exception
{
    public ProductStockNotEnoughException() : base("產品庫存不足") { }
}