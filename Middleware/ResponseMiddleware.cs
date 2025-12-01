using System.Text.Json;

namespace website_backend.Middleware;

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 直接调用下一个中间件，不处理响应格式
        await _next(context);
    }
}