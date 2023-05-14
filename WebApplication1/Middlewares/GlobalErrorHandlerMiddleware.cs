namespace WebApplication1;
public class GlobalErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);

            ResponseResult result = new ResponseResult(exception);

            await HandleExceptionAsync(context, result);
        }

    }

    private async Task HandleExceptionAsync(HttpContext context, ResponseResult result)
    {
        string response = JsonSerializer.Serialize(result);


        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(response);
    }

}
