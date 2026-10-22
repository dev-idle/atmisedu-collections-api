using Microsoft.AspNetCore.Http;

namespace atMISEDU.Contract.Abstractions
{
    public interface ILogService
    {
        Task LogActionAsync(HttpContext context, int logType, object? request = null);
    }
}