using Lib.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApplication1.Component;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]", Name = "Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// 取得訂單資訊
        /// </summary>
        /// <param name="phoneNumber">訂購人電話</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<IEnumerable<Order>>> Get([FromQuery] string phoneNumber, CancellationToken ctx)
        {
            var serviceResult = await _orderService.GetOrderAsync(phoneNumber, ctx);
            return serviceResult.ToApiResult();
        }
        /// <summary>
        /// 下訂單
        /// </summary>
        /// <param name="shoppingCartId">購物車的編號</param>
        /// <param name="phoneNumber">訂購人電話號碼</param>
        /// <param name="hasCoupon">是否使用折價卷</param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{shoppingCartId:int}")]
        public async Task<ApiResult> PlaceOrder(int shoppingCartId, [FromQuery] string phoneNumber,[FromQuery]bool hasCoupon, CancellationToken ctx)
        {
            var serviceResult = await _orderService.PlaceOrderAsync(shoppingCartId, phoneNumber, hasCoupon, ctx);

            return serviceResult.ToApiResult();
        }

    }
}
