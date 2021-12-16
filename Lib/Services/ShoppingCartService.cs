using DAL.Abstractions;
using Lib.ServiceResults;
using Models;

namespace Lib.Services.Interfaces;

class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartDetailRepository _shoppingCartDetailRepository;


    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, 
        IProductRepository productRepository,
        IShoppingCartDetailRepository shoppingCartDetailRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
        _shoppingCartDetailRepository = shoppingCartDetailRepository;
    }
    public async Task<IServiceResult<ShoppingCart>> GetAsync(string deviceGuid, CancellationToken ctx)
    {
        var result = new ServiceResult<ShoppingCart>();
        try
        {
            var data = await GetOrCreateShoppingCartAsync(deviceGuid, true, ctx);
            result.Data = data;
        }
        catch (Exception e)
        {
            result.SetException(e);
        }

        return result;
    }

    public async Task<IServiceResult> AddProductAsync(string deviceGuid, int productId, CancellationToken ctx)
    {
        var result = new ServiceResult();
        try
        {
            if (await IsProductIdExists(productId,ctx) !=true)
            {
                result.AddError($"ProductId:{productId}不存在於資料庫");
                return result;
            }
            var product = await _productRepository.GetAllAsync(m => m.ProductId == productId, ctx);
            if (product.FirstOrDefault() == null)
            {
                
            }

            var shoppingCart = await GetOrCreateShoppingCartAsync(deviceGuid, ctx: ctx);

            var shoppingCartDetail = new ShoppingCartDetail()
            {
                ShoppingCartId = shoppingCart.ShoppingCartId,
                ProductId = productId,
                ProductCount = 1
            };
            await _shoppingCartDetailRepository.InsertIfNotExistsAsync(shoppingCartDetail, ctx);
        }
        catch (Exception e)
        {
            result.SetException(e);
        }
        return result;
    }

   

    public async Task<IServiceResult> UpdateProductCountAsync(string deviceGuid, int productId, int count, CancellationToken ctx)
    {
        var result = new ServiceResult();
        try
        {
            if (await IsProductIdExists(productId, ctx) != true)
            {
                result.AddError($"ProductId:{productId}不存在於資料庫");
                return result;
            }
            var shoppingCart = await GetOrCreateShoppingCartAsync(deviceGuid, ctx: ctx);
            var detail = new ShoppingCartDetail()
            {
                ShoppingCartId = shoppingCart.ShoppingCartId,
                ProductId = productId,
                ProductCount = count
            };
            await _shoppingCartDetailRepository.UpdateOrInsertAsync(detail, ctx);
        }
        catch (Exception e)
        {
            result.SetException(e);
        }
        return result;


    }


    public async Task<IServiceResult> DeleteProductAsync(string deviceGuid, int productId, CancellationToken ctx)
    {
        var result = new ServiceResult();
        try
        {
            if (await IsProductIdExists(productId,ctx) != true)
            {
                result.AddError($"ProductId:{productId}不存在於資料庫");
                return result;
            }

            var shoppingCart = await GetOrCreateShoppingCartAsync(deviceGuid, ctx: ctx);
            var shoppingCartId = shoppingCart.ShoppingCartId;

            await _shoppingCartDetailRepository.DeleteAsync(m => m.ShoppingCartId == shoppingCartId && m.ProductId == productId, ctx);
        }
        catch (Exception e)
        {
            result.SetException(e);
        }
        return result;
    }
    private async Task<bool> IsProductIdExists(int productId, CancellationToken ctx)
    {
        var product = await _productRepository.GetAllAsync(m => m.ProductId == productId, ctx);
        if (product.FirstOrDefault() == null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 取得或建立購物車
    /// </summary>
    /// <param name="deviceGuid"></param>
    /// <param name="isIncludeDetails">是否要查詢購物車明細</param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<ShoppingCart> GetOrCreateShoppingCartAsync(string deviceGuid, bool isIncludeDetails = false, CancellationToken ctx = default)
    {
        // 如果購物車不存在，則先新增購物車(裡面沒產品)
        var shoppingCart = await _shoppingCartRepository
            .GetFirstOrDefaultAsync(m => m.OwnerDeviceGuid == deviceGuid, isIncludeDetails, ctx);
        if (shoppingCart == null)
        {
            await CreateShoppingCartAsync(deviceGuid, ctx);
            // 重新取得購物車
            shoppingCart = await _shoppingCartRepository
                .GetFirstOrDefaultAsync(m => m.OwnerDeviceGuid == deviceGuid, isIncludeDetails, ctx);
        }
        if (shoppingCart == null)
            throw new Exception("在建立購物車後，購物車仍然為空。");
        return shoppingCart;
    }

    /// <summary>
    /// 建立新的購物車
    /// </summary>
    /// <param name="deviceGuid"></param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    private async Task CreateShoppingCartAsync(string deviceGuid, CancellationToken ctx)
    {
        var data = new ShoppingCart()
        {
            OwnerDeviceGuid = deviceGuid
        };
        await _shoppingCartRepository.InsertAsync(data, ctx);
    }
}