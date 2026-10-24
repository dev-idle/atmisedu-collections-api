using atMISEDU.Contract.Shared;
using atMISEDU.Protos.Common;

namespace atMISEDU.Grpc.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Convert <see cref="Result{TModel}"/> of handler to <see cref="CommonResult"/> of gRPC
        /// </summary>
        public static CommonResult ToCommonResult<TModel>(this Result<TModel> result) where TModel : class
        {
            return new CommonResult
            {
                StatusCode = result.StatusCode,
                IsSuccess = result.IsSuccess,
                MessageCode = result.MessageCode?.ToString() ?? string.Empty
            };
        }

        /// <summary>
        /// Create handlers return data as { key = id }, read the id by key name
        /// </summary>
        public static int GetCreatedId(this Result<object> result, string keyName)
        {
            return (int)(result.Data?.GetType().GetProperty(keyName)?.GetValue(result.Data) ?? 0);
        }
    }
}
