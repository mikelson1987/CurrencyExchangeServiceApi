using System.Net;
using System.Text.Json;
using CurrencyExchangeService.Core.Exceptions;

namespace CurrencyExchangeApi;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (ex)
            {
                case CurrencyNotSupportedException:
                case UserNotSupportedException:
                case ArgumentOutOfRangeException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case CurrencyNotFoundException:
                case UserNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = ex?.Message });
            await response.WriteAsync(result);
        }
    }
}