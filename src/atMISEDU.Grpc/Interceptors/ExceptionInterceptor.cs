using System.Net;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace atMISEDU.Grpc.Interceptors
{
    /// <summary>
    /// Convert exceptions thrown by handlers to gRPC status, same role as GlobalExceptionHandler of REST api
    /// </summary>
    public class ExceptionInterceptor : Interceptor
    {
        private readonly ILogger<ExceptionInterceptor> logger;
        private readonly IWebHostEnvironment env;

        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.env = env;
        }

        /// <summary>
        /// Handle unary call, convert <see cref="CustomException"/> and unexpected exception to <see cref="RpcException"/>
        /// </summary>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (CustomException ex)
            {
                // Business error (invalid request, not found...), keep message code and details for client
                throw CreateRpcException(ex.StatusCode, ex.MessageCode, ex.Details);
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", ex.Message, DateTime.UtcNow);
                // Do not expose exception message in production
                var details = env.IsProduction() ? new List<string>() : new List<string> { ex.Message };
                throw CreateRpcException((int)HttpStatusCode.InternalServerError, MsgCode.ERR_INTERNAL_SERVER, details);
            }
        }

        /// <summary>
        /// Create <see cref="RpcException"/> with gRPC status, details as status message and codes in trailers
        /// </summary>
        private static RpcException CreateRpcException(int statusCode, MsgCode messageCode, List<string> details)
        {
            var trailers = new Metadata
            {
                { "status-code", statusCode.ToString() },
                { "message-code", messageCode.ToString() }
            };
            return new RpcException(new Status(ToGrpcStatusCode(statusCode), string.Join("; ", details)), trailers);
        }

        /// <summary>
        /// Map HTTP status code of <see cref="CustomException"/> to gRPC status code
        /// </summary>
        private static StatusCode ToGrpcStatusCode(int statusCode) => statusCode switch
        {
            (int)HttpStatusCode.BadRequest => StatusCode.InvalidArgument,
            (int)HttpStatusCode.Unauthorized => StatusCode.Unauthenticated,
            (int)HttpStatusCode.Forbidden => StatusCode.PermissionDenied,
            (int)HttpStatusCode.NotFound => StatusCode.NotFound,
            (int)HttpStatusCode.Conflict => StatusCode.AlreadyExists,
            _ => StatusCode.Internal
        };
    }
}
