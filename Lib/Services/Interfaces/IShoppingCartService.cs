using Lib.ServiceResults;
using Models;

namespace Lib.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IServiceResult<ShoppingCart>> GetAsync(string deviceGuid, CancellationToken ctx);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceGuid"></param>
        /// <param name="productId">商品編號</param>
        /// <param name="count">商品數量</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        Task<IServiceResult> AddProductAsync(string deviceGuid, int productId, CancellationToken ctx);
        Task<IServiceResult> UpdateProductCountAsync(string deviceGuid, int productId, int count, CancellationToken ctx);
        Task<IServiceResult> DeleteProductAsync(string deviceGuid, int productId, CancellationToken ctx);
    }
}
