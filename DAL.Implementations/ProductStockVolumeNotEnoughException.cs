namespace DAL.Implementations;

public class ProductStockVolumeNotEnoughException : Exception
{
    public ProductStockVolumeNotEnoughException(string errMsg) : base(errMsg) { }
}