using Lib.ServiceResults;

namespace WebApplication1.Component
{
    public class ApiResult
    {
        public string Message { get; set; }
        public string Stataus { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
    }

    internal static class IServiceResultExtensions
    {
        public static ApiResult ToApiResult(this IServiceResult serviceResult)
        {
            return new ApiResult()
            {
                Message = serviceResult.Message,
                Stataus = serviceResult.State.ToStringFaster()

            };
        }
        public static ApiResult<T> ToApiResult<T>(this IServiceResult<T> serviceResult)
        {
            return new ApiResult<T>()
            {
                Message = serviceResult.Message,
                Stataus = serviceResult.State.ToStringFaster(),
                Data = serviceResult.Data
            };
        }
    }
}
