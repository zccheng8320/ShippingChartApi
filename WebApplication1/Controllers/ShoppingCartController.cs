using Lib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApplication1.Component;
using WebApplication1.DeviceGuid;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]", Name = "ShoppingCart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IClientDeviceGuidProvider _clientDeviceGuidProvider;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IClientDeviceGuidProvider clientDeviceGuidProvider, IShoppingCartService shoppingCartService)
        {
            _clientDeviceGuidProvider = clientDeviceGuidProvider;
            _shoppingCartService = shoppingCartService;
        }
        /// <summary>
        /// 取得購物車資訊
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShoppingCart>> Get(CancellationToken ctx)
        {
            var deviceGuid = _clientDeviceGuidProvider.Get();
            var result = await _shoppingCartService.GetAsync(deviceGuid, ctx);

            return result.ToApiResult();
        }
        /// <summary>
        /// 新增產品至購物車
        /// </summary>
        /// <param name="productId">產品id</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{productId:int}", Name = "AddProduct")]
        public async Task<ApiResult> AddProduct(int productId, CancellationToken ctx)
        {
            var deviceGuid = _clientDeviceGuidProvider.Get();
            var result = await _shoppingCartService.AddProductAsync(deviceGuid, productId, ctx);

            return result.ToApiResult();
        }
        /// <summary>
        /// 更新購物車產品的數量
        /// </summary>
        /// <param name="productId">產品編號</param>
        /// <param name="count">數量</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{productId:int}/{count:int}")]
        public async Task<ApiResult> Update(int productId, int count, CancellationToken ctx)
        {
            var deviceGuid = _clientDeviceGuidProvider.Get();
            var result = await _shoppingCartService.UpdateProductCountAsync(deviceGuid, productId, count, ctx);

            return result.ToApiResult();
        }

        /// <summary>
        /// 刪除指定商品
        /// </summary>
        /// <param name="productId">產品編號</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/{productId:int}")]
        public async Task<ApiResult> DeleteProductAsync(int productId, CancellationToken ctx)
        {
            var deviceGuid = _clientDeviceGuidProvider.Get();
            var result = await _shoppingCartService.DeleteProductAsync(deviceGuid, productId, ctx);


            return result.ToApiResult();
        }

    }
}
