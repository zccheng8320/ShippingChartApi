using DAL.Abstractions;
using Lib.Biz.OrderBiz;
using Lib.ServiceResults;
using Lib.Services.Interfaces;
using Models;

namespace Lib.Services;

internal class OrderService : IOrderService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IShoppingCartDetailRepository _shoppingCartDetailRepository;
    private readonly IOrderBiz _orderBiz;

    public OrderService(
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IShoppingCartRepository shoppingCartRepository,
        IShoppingCartDetailRepository shoppingCartDetailRepository,
        IOrderBiz orderBiz)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _shoppingCartDetailRepository = shoppingCartDetailRepository;
        _orderBiz = orderBiz;
    }
    public async Task<IServiceResult<IEnumerable<Order>>> GetOrderAsync(string phoneNumber, CancellationToken ctx)
    {
        var serviceResult = new ServiceResult<IEnumerable<Order>>();

        try
        {
            serviceResult.Data = await _orderRepository.GetAllAsync(m => m.OwnerPhoneNumber == phoneNumber, ctx);
        }
        catch (Exception e)
        {
            serviceResult.SetException(e);
        }
        return serviceResult;

    }

    public async Task<IServiceResult> PlaceOrderAsync(int shoppingCartId, string phoneNumber,bool hasCoupon, CancellationToken ctx)
    {
        var serviceResult = new ServiceResult();
        try
        {
            var shoppingCart = await _shoppingCartRepository
                .GetFirstOrDefaultAsync(m => m.ShoppingCartId == shoppingCartId, true, ctx);
            if (shoppingCart == null)
            {
                serviceResult.AddError($"查無此購物車編號{shoppingCartId}");
                return serviceResult;
            }

            if (!shoppingCart.ShoppingCartDetails.Any())
            {
                serviceResult.AddError($"購物車不可為空");
                return serviceResult;
            }
            await ValidateShoppingCartAsync(shoppingCart, serviceResult, ctx);

            if (serviceResult.State != ResultState.Success)
            {
                return serviceResult;
            }
            var order = shoppingCart.ToOrder(phoneNumber, hasCoupon);
            _orderBiz.SetDiscount(order);
            _orderBiz.SetShippingFee(order);
            await _orderRepository.InsertAsync(order, ctx);
            // 訂單建立完成，嘗試清空購物車資訊(執行結果不影響)
            await TryClearShoppingCart(shoppingCartId);
        }
        catch (Exception e)
        {
            serviceResult.SetException(e);
        }
        return serviceResult;
    }

    private async Task TryClearShoppingCart(int shoppingCartId)
    {
        try
        {
            await _shoppingCartDetailRepository.DeleteAsync(m => m.ShoppingCartId == shoppingCartId, default);
        }
        catch (Exception)
        {
            //ignored
        }
    }

    /// <summary>
    /// 驗證購物車是否可以成立為訂單
    /// </summary>
    /// <param name="shoppingCart"></param>
    /// <param name="serviceResult"></param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private async Task ValidateShoppingCartAsync(ShoppingCart shoppingCart, ServiceResult serviceResult, CancellationToken ctx)
    {
        var products = await _productRepository.GetAllAsync(m => true, ctx);
        var productDict = products.ToDictionary(m => m.ProductId);

        foreach (var shoppingCartShoppingCartDetail in shoppingCart.ShoppingCartDetails)
        {
            var productId = shoppingCartShoppingCartDetail.ProductId;
            var productCountInShoppingCart = shoppingCartShoppingCartDetail.ProductCount;
            if (!productDict.ContainsKey(shoppingCartShoppingCartDetail.ProductId))
            {
                serviceResult.AddError($"ProductId:{productId}不存在於資料庫中。");
                continue;
            }
            // 以下為檢查庫存是否足夠
            var product = productDict[productId];
            if (product.PurchaseCount < product.SalesVolume + productCountInShoppingCart)
            {
                serviceResult.AddError($"ProductId :" +
                                       $"{productId}的庫存不足。");
            }
        }
    }

}

static class ShoppingCartExtensions
{
    /// <summary>
    /// 將購物車轉換成訂單資訊
    /// </summary>
    /// <param name="shoppingCart"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="hasCoupon"></param>
    /// <returns></returns>
    public static Order ToOrder(this ShoppingCart shoppingCart, string phoneNumber, bool hasCoupon)
    {
        var order = new Order
        {
            HasCoupon = hasCoupon,
            OwnerPhoneNumber = phoneNumber,
            OrderDetails = new List<OrderDetail>()
            
        };
        foreach (var shoppingCartDetail in shoppingCart.ShoppingCartDetails)
        {
            if (shoppingCartDetail.ProductCount == 0)
                continue;
            var orderDetail = new OrderDetail()
            {
                ProductId = shoppingCartDetail.ProductId,
                CheckoutPrice = shoppingCartDetail.Product.Price,
                ProductCount = shoppingCartDetail.ProductCount
            };
            order.OrderDetails.Add(orderDetail);
            order.CheckoutAmount += shoppingCartDetail.Product.Price * shoppingCartDetail.ProductCount;
        }

        return order;
    }
}