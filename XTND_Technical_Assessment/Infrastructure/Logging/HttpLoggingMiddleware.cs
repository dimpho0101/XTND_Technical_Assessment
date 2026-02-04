namespace XTND_Technical_Assessment.Infrastructure.Logging;


public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpLoggingMiddleware> _logger;

    public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        var request = context.Request;

        _logger.LogInformation(
            "HTTP Request: {Method} {Path} - RemoteIP: {RemoteIP}",
            request.Method,
            request.Path.Value,
            context.Connection.RemoteIpAddress);

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {

            await _next(context);


            var duration = DateTime.UtcNow - startTime;
            _logger.LogInformation(
                "HTTP Response: {Method} {Path} - Status: {StatusCode} - Duration: {ElapsedMilliseconds}ms",
                request.Method,
                request.Path.Value,
                context.Response.StatusCode,
                duration.TotalMilliseconds);
        }
        catch (Exception ex)
        {
            var duration = DateTime.UtcNow - startTime;
            _logger.LogError(ex,
                "HTTP Exception: {Method} {Path} - Duration: {ElapsedMilliseconds}ms",
                request.Method,
                request.Path.Value,
                duration.TotalMilliseconds);

            throw;
        }
        finally
        {

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }
}


public static class HttpLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpLoggingMiddleware>();
    }
}
